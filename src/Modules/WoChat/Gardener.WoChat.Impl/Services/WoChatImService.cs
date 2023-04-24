// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.Authorization.Core;
using Gardener.Common;
using Gardener.NotificationSystem.Core;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.WoChat.Domains;
using Gardener.WoChat.Dtos;
using Gardener.WoChat.Dtos.Notification;
using Gardener.WoChat.Enums;
using Gardener.WoChat.Impl.Core;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Gardener.WoChat.Services
{
    /// <summary>
    /// Im聊天服务
    /// </summary>
    [ApiDescriptionSettings("NotificationSystem")]
    public class WoChatImService : IWoChatImService, IDynamicApiController, IScoped
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IRepository<ImSession> imSessionRepository;
        private readonly IRepository<ImSessionMessage> imSessionMessageRepository;
        private readonly IRepository<ImUserSession> imUserSessionRepository;
        private readonly ISystemNotificationService systemNotificationService;
        private readonly IUserService userService;
        /// <summary>
        /// Im聊天服务
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="imSessionRepository"></param>
        /// <param name="imSessionMessageRepository"></param>
        /// <param name="imUserSessionRepository"></param>
        /// <param name="systemNotificationService"></param>
        /// <param name="userService"></param>
        public WoChatImService(IAuthorizationService authorizationService, IRepository<ImSession> imSessionRepository, IRepository<ImSessionMessage> imSessionMessageRepository, IRepository<ImUserSession> imUserSessionRepository, ISystemNotificationService systemNotificationService, IUserService userService)
        {
            this.authorizationService = authorizationService;
            this.imSessionRepository = imSessionRepository;
            this.imSessionMessageRepository = imSessionMessageRepository;
            this.imUserSessionRepository = imUserSessionRepository;
            this.systemNotificationService = systemNotificationService;
            this.userService = userService;
        }
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        private string GetSignature(IEnumerable<int> userIds)
        {
            string userIdStrs = string.Join(",", userIds.OrderBy(x => x));
            return MD5Encryption.Encrypt(userIdStrs);
        }
        /// <summary>
        /// 添加会话
        /// </summary>
        /// <param name="input"></param>
        /// <returns>会话编号</returns>
        public async Task<Guid?> AddMyImSession(ImSessionAddInput input)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return null;
            }
            int currentUserId = int.Parse(identity.Id);
            List<int> userIds = input.UserIds.ToList();
            if (userIds.Count == 0)
            {
                return null;
            }
            if (userIds.Count == 1)
            {
                //只有一个只能是私聊
                input.SessionType = ImSessionType.Personal;
            }

            userIds.Add(currentUserId);
            string signature = GetSignature(userIds);
            if (input.SessionType.Equals(ImSessionType.Personal))
            {
                //判断私聊是否存在
                var session = await imSessionRepository.Where(x => x.SessionType.Equals(ImSessionType.Personal) && x.UsersSignature.Equals(signature)).FirstOrDefaultAsync();
                if (session != null)
                {
                    var userSession = await imUserSessionRepository.Where(x => x.UserId.Equals(currentUserId) && x.ImSessionId.Equals(session.Id)).FirstOrDefaultAsync();
                    if (userSession != null)
                    {
                        //激活我的会话
                        userSession.IsActive = true;
                        userSession.SetUpdatedIdentity(identity);
                        userSession.UpdatedTime = DateTimeOffset.Now;
                        await imUserSessionRepository.UpdateIncludeAsync(userSession, new[] { nameof(ImUserSession.IsActive), nameof(ImUserSession.UpdateIdentityType), nameof(ImUserSession.UpdateBy), nameof(ImUserSession.UpdatedTime) });
                    }
                    else
                    {
                        //会话缺失
                        ImUserSession imUserSession = new()
                        {
                            Id = Guid.NewGuid(),
                            IsActive = true,
                            UserId = currentUserId,
                            ImSessionId = session.Id,
                            CreatedTime = DateTimeOffset.Now
                        };
                        imUserSession.SetCreatedIdentity(identity);
                        await imUserSessionRepository.InsertAsync(imUserSession);

                    }
                    session.LastMessageTime = DateTimeOffset.Now;
                    session.SetUpdatedIdentity(identity);
                    session.UpdatedTime = DateTimeOffset.Now;
                    await imSessionRepository.UpdateIncludeAsync(session, new[] { nameof(ImSession.LastMessageTime), nameof(ImSession.UpdateIdentityType), nameof(ImSession.UpdateBy), nameof(ImSession.UpdatedTime) });
                    //已存在
                    return session.Id;
                }
            }

            ImSession imSession = input.Adapt<ImSession>();
            imSession.Id = Guid.NewGuid();
            imSession.UsersSignature = signature;
            imSession.SetUpdatedIdentity(identity);
            imSession.CreatedTime = DateTimeOffset.Now;

            await imSessionRepository.InsertAsync(imSession);

            foreach (var userId in userIds)
            {
                ImUserSession imUserSession = new()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    UserId = userId,
                    ImSessionId = imSession.Id,
                    CreatedTime = DateTimeOffset.Now
                };
                imUserSession.SetCreatedIdentity(identity);
                if (userId.Equals(currentUserId))
                {
                    //激活自己的会话
                    imUserSession.IsActive = true;
                }
                await imUserSessionRepository.InsertAsync(imUserSession);

                await systemNotificationService.UserGroupAdd(WoChatUtil.GetImGroupName(imSession.Id), new Identity()
                {
                    Id = userId.ToString(),
                    IdentityType = identity.IdentityType
                });
            }
            return imSession.Id;
        }
        /// <summary>
        /// 获取会话列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ImSessionDto>> GetImGroupSessions()
        {
            List<ImSessionDto> imSessions = await imSessionRepository.AsQueryable(false).Where(x => x.SessionType.Equals(ImSessionType.Group)).Select(x => x.Adapt<ImSessionDto>()).ToListAsync();
            if (!imSessions.Any())
            {
                return new ImSessionDto[0];
            }
            List<ImUserSessionDto> imUserSessions = await imUserSessionRepository.AsQueryable(false).Where(x => imSessions.Select(u => u.Id).Contains(x.ImSessionId)).Select(x => x.Adapt<ImUserSessionDto>()).ToListAsync();
            if (!imUserSessions.Any())
            {
                return new ImSessionDto[0];
            }
            List<UserDto> users = await userService.GetUsers(imUserSessions.Select(x => x.UserId).Distinct());
            imSessions.ForEach(x =>
            {
                IEnumerable<int> userIds = imUserSessions.Where(u => u.ImSessionId.Equals(x.Id)).Select(u => u.UserId);
                x.Users = users.Where(r => userIds.Contains(r.Id)).Select(x => x);
            });
            return imSessions;
        }
        /// <summary>
        /// 获取我的会话列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ImSessionDto>> GetMyImSessions()
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return new ImSessionDto[0];
            }
            int userId = int.Parse(identity.Id);
            //我的会话
            IEnumerable<ImUserSessionDto> imUsersessionDtos = await imUserSessionRepository
                .AsQueryable(false)
                .Where(x => x.UserId.Equals(userId) && x.IsActive == true)
                .Select(x => x.Adapt<ImUserSessionDto>())
                .ToListAsync();

            IEnumerable<Guid> sessionIds = imUsersessionDtos.Select(x => x.ImSessionId).Distinct();
            if (!sessionIds.Any())
            {
                return new ImSessionDto[0];
            }
            var task1 = imSessionRepository.AsQueryable(false)
                .Where(x => sessionIds.Contains(x.Id))
                .Select(x => x.Adapt<ImSessionDto>())
                .ToListAsync();
            var task2 = imUserSessionRepository.AsQueryable(false)
                .Where(x => sessionIds.Contains(x.ImSessionId))
                .ToListAsync();

            IEnumerable<ImSessionDto> sessions = await task1;
            IEnumerable<ImUserSession> userSessions = await task2;

            IEnumerable<UserDto> users = await userService.GetUsers(userSessions.Select(x => x.UserId).Distinct());
            //填充用戶信息
            foreach (ImSessionDto session in sessions)
            {
                IEnumerable<int> userIds = userSessions.Where(x => x.ImSessionId.Equals(session.Id)).Select(x => x.UserId).Distinct();
                session.Users = users.Where(x => userIds.Any(u => u.Equals(x.Id)));
                session.SessionName = GetShowSessionName(session, userId);
                session.CurrentUserCanSendMessage = session.DisableSendMessage.Equals(true) && !userId.ToString().Equals(session?.CreateBy);
            }
            return sessions.OrderByDescending(x => x.LastMessageTime);
        }
        /// <summary>
        /// 获取会话消息列表
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <param name="maxDateTime"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ImSessionMessageDto>> GetMySessionMessages([FromQuery] Guid imSessionId, [FromQuery] DateTimeOffset? maxDateTime = null, [FromQuery] int pageSize = 100)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return new ImSessionMessageDto[0];
            }
            int userId = int.Parse(identity.Id);
            //不在会话中，不能查询
            if (!await imUserSessionRepository
                .AsQueryable(false)
                .Where(x => x.UserId.Equals(userId)).AnyAsync())
            {
                return new ImSessionMessageDto[0];
            }
            var query = imSessionMessageRepository.AsQueryable(false)
               .Where(x => x.ImSessionId.Equals(imSessionId));
            if (maxDateTime != null)
            {
                query = query.Where(x => x.CreatedTime > maxDateTime);
            }
            IEnumerable<ImSessionMessage> messages = await query
                .OrderBy(x => x.CreatedTime)
                .Take(pageSize)
                .ToListAsync();
            if (!messages.Any())
            {
                return new ImSessionMessageDto[0];
            }
            //填充用戶信息
            var users = await userService.GetUsers(messages.Select(x => x.UserId));

            List<ImSessionMessageDto> messageDtos = new List<ImSessionMessageDto>();
            foreach (ImSessionMessage message in messages)
            {
                var userDto = message.Adapt<ImSessionMessageDto>();
                userDto.User = users.Where(x => x.Id == message.UserId).FirstOrDefault();
                messageDtos.Add(userDto);
            }
            return messageDtos;
        }

        /// <summary>
        /// 退出会话
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 私聊隐藏，群聊自己创建的话直接解散，不是就退出
        /// </remarks>
        public async Task<bool> QuitMyImSession([FromBody] Guid imSessionId)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return false;
            }
            int userId = int.Parse(identity.Id);
            var task1 = imSessionRepository.FindAsync(imSessionId);
            var task2 = imUserSessionRepository.AsQueryable(false).Where(x => x.ImSessionId.Equals(imSessionId)).ToListAsync();
            var session = await task1;
            var userSessions = await task2;
            if (session == null || userSessions == null || !userSessions.Any(x => x.UserId.Equals(userId)))
            {
                return false;
            }
            List<Task> tasks = new List<Task>();
            if (session.SessionType.Equals(ImSessionType.Personal))
            {
                //隐藏
                ImUserSession userSession = userSessions.Where(x => x.UserId.Equals(userId)).First();
                userSession.SetUpdatedIdentity(identity);
                userSession.UpdatedTime = DateTimeOffset.Now;
                userSession.IsActive = false;
                tasks.Add(imUserSessionRepository.UpdateIncludeAsync(userSession, new[] { nameof(ImUserSession.IsActive), nameof(ImUserSession.UpdateIdentityType), nameof(ImUserSession.UpdateBy), nameof(ImUserSession.UpdatedTime) }));
                //更新会话已有人隐藏了
                session.AllUserIsActive = false;
                session.SetUpdatedIdentity(identity);
                session.UpdatedTime = DateTimeOffset.Now;
                tasks.Add(imSessionRepository.UpdateIncludeAsync(session, new[] { nameof(ImSession.AllUserIsActive), nameof(ImSession.UpdateIdentityType), nameof(ImSession.UpdateBy), nameof(ImSession.UpdatedTime) }));
            }
            else if (session.SessionType.Equals(ImSessionType.Group))
            {
                if (userId.ToString().Equals(session.CreateBy))
                {
                    //解散
                    tasks.Add(imSessionRepository.DeleteAsync(imSessionId));
                    userSessions.ForEach(x =>
                    {
                        tasks.Add(imUserSessionRepository.DeleteAsync(x.Id));
                        tasks.Add(systemNotificationService.UserGroupRemove(WoChatUtil.GetImGroupName(imSessionId), new Identity()
                        {
                            Id = x.UserId.ToString(),
                            IdentityType = IdentityType.User
                        }));
                    });

                }
                else
                {
                    //退出
                    ImUserSession userSession = userSessions.Where(x => x.UserId.Equals(userId)).First();
                    tasks.Add(imUserSessionRepository.DeleteAsync(userSession.Id));
                    tasks.Add(systemNotificationService.UserGroupRemove(WoChatUtil.GetImGroupName(imSessionId), identity));
                    tasks.Add(systemNotificationService.SendToGroup(WoChatUtil.GetImGroupName(imSessionId), new WoChatImSystemMessageNotificationData()
                    {
                        Identity = identity,
                        ImSessionId = imSessionId,
                        MessageType = ImSystemMessageType.UserQuit
                    }));
                }
            }
            await Task.WhenAll(tasks);
            return true;
        }
        /// <summary>
        /// 发送消息到会话
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<bool> SendMessage(ImSessionMessageDto message)
        {
            return SendMessage(message, authorizationService.GetIdentity());
        }
        /// <summary>
        /// 发送消息到会话
        /// </summary>
        /// <param name="message"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<bool> SendMessage(ImSessionMessageDto message, Identity? identity)
        {
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return false;
            }
            int userId = int.Parse(identity.Id);
            var imSession = await imSessionRepository.FindAsync(message.ImSessionId);
            if (imSession.DisableSendMessage.Equals(true) && !userId.ToString().Equals(imSession.CreateBy))
            {
                //禁言中
                throw Oops.Oh(ExceptionCode.SessionDisableSendMessage);
            }
            ImSessionMessage sessionMessage = message.Adapt<ImSessionMessage>();
            sessionMessage.UserId = userId;
            sessionMessage.CreatedTime = DateTimeOffset.Now;
            sessionMessage.CreateBy = identity.Id;
            sessionMessage.CreateIdentityType = identity.IdentityType;
            List<Task> tasks = new List<Task>();
            //入库
            tasks.Add(imSessionMessageRepository.InsertNowAsync(sessionMessage));
            //查找会话
            var user = await userService.Get(userId);
            message = sessionMessage.Adapt<ImSessionMessageDto>();
            message.User = user;
            if (imSession != null)
            {
                if (!imSession.AllUserIsActive)
                {
                    //查看那些用户未激活
                    List<ImUserSession> noActiveUserSessions = await imUserSessionRepository.AsQueryable(false).Where(x => x.IsActive == false && x.ImSessionId.Equals(imSession.Id)).ToListAsync();
                    noActiveUserSessions.ForEach(x =>
                    {
                        //激活
                        x.IsActive = true;
                        x.SetUpdatedIdentity(identity);
                        x.UpdatedTime = DateTimeOffset.Now;
                        tasks.Add(imUserSessionRepository.UpdateIncludeNowAsync(x, new[] { nameof(ImUserSession.IsActive), nameof(ImUserSession.UpdateIdentityType), nameof(ImUserSession.UpdateBy), nameof(ImUserSession.UpdatedTime) }));
                    });
                    imSession.AllUserIsActive = true;
                }
                //更新会话时间
                imSession.SetUpdatedIdentity(identity);
                imSession.LastMessageTime = DateTimeOffset.Now;
                tasks.Add(imSessionRepository.UpdateIncludeNowAsync(imSession, new[] { nameof(ImSession.AllUserIsActive), nameof(ImSession.LastMessageTime), nameof(ImSession.UpdateIdentityType), nameof(ImSession.UpdateBy), nameof(ImSession.UpdatedTime) }));
            }
            //发送
            tasks.Add(systemNotificationService.SendToGroup(WoChatUtil.GetImGroupName(message.ImSessionId), new WoChatImUserMessageNotificationData()
            {
                Identity = identity,
                ImMessage = message,
            }));

            await Task.WhenAll(tasks);
            return true;
        }
        /// <summary>
        /// 关闭会话消息发送权限
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        public async Task<bool> DisableSessionSendMessage([FromBody] Guid imSessionId)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return false;
            }
            int userId = int.Parse(identity.Id);

            var session = await imSessionRepository.FindAsync(imSessionId);
            if (session == null)
            {
                return false;
            }

            if (!userId.ToString().Equals(identity.Id))
            {
                return false;
            }

            session.DisableSendMessage = true;
            session.SetUpdatedIdentity(identity);
            session.UpdatedTime = DateTimeOffset.Now;
            await imSessionRepository.UpdateIncludeAsync(session, new[] { nameof(ImSession.DisableSendMessage), nameof(ImSession.UpdateIdentityType), nameof(ImSession.UpdateBy), nameof(ImSession.UpdatedTime) });
            await systemNotificationService.SendToGroup(WoChatUtil.GetImGroupName(imSessionId), new WoChatImSystemMessageNotificationData()
            {
                Identity = identity,
                ImSessionId = imSessionId,
                MessageType = ImSystemMessageType.DisableSessionSendMessage
            });
            return true;

        }
        /// <summary>
        /// 获取展示的会话名称
        /// </summary>
        /// <param name="session"></param>
        /// <param name="myUserId">自己的用户编号-<see cref="ImSessionType.Personal"/>有效</param>
        /// <returns></returns>
        private string GetShowSessionName(ImSessionDto session, int? myUserId = null)
        {
            if (session.SessionType.Equals(ImSessionType.Personal))
            {
                UserDto userTemp = session.Users.Where(x => x.Id != myUserId).First();
                return userTemp.NickName ?? userTemp.UserName;
            }
            else if (session.SessionType.Equals(ImSessionType.Group))
            {
                if (!string.IsNullOrEmpty(session.SessionName))
                {
                    return session.SessionName;
                }
                return string.Join("、", session.Users.Take(3).Select(x => x.NickName ?? x.UserName)) + "...";
            }
            return string.Empty;
        }
        /// <summary>
        /// 开启会话消息发送权限
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> EnableSessionSendMessage([FromBody] Guid imSessionId)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return false;
            }
            int userId = int.Parse(identity.Id);

            var session = await imSessionRepository.FindAsync(imSessionId);
            if (session == null)
            {
                return false;
            }

            if (!userId.ToString().Equals(identity.Id))
            {
                return false;
            }

            session.DisableSendMessage = false;
            session.SetUpdatedIdentity(identity);
            session.UpdatedTime = DateTimeOffset.Now;
            await imSessionRepository.UpdateIncludeAsync(session, new[] { nameof(ImSession.DisableSendMessage), nameof(ImSession.UpdateIdentityType), nameof(ImSession.UpdateBy), nameof(ImSession.UpdatedTime) });
            await systemNotificationService.SendToGroup(WoChatUtil.GetImGroupName(imSessionId), new WoChatImSystemMessageNotificationData()
            {
                Identity = identity,
                ImSessionId = imSessionId,
                MessageType = ImSystemMessageType.EnableSessionSendMessage
            });
            return true;
        }
        /// <summary>
        /// 根据会话编号获取会话
        /// </summary>
        /// <param name="imSessionId">会话编号</param>
        /// <returns></returns>
        public async Task<ImSessionDto?> GetImSession(Guid imSessionId)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return null;
            }
            int userId = int.Parse(identity.Id);
            //我的会话
            bool exitis = await imUserSessionRepository
                .AsQueryable(false)
                .Where(x => x.UserId.Equals(userId) && x.ImSessionId.Equals(imSessionId))
                .AnyAsync();

            if (!exitis)
            {
                return null;
            }
            var task1 = imSessionRepository.AsQueryable(false)
                .Where(x => x.Id.Equals(imSessionId))
                .Select(x => x.Adapt<ImSessionDto>())
                .FirstOrDefaultAsync();
            var task2 = imUserSessionRepository.AsQueryable(false)
                .Where(x => x.ImSessionId.Equals(imSessionId))
                .Select(x => x.UserId)
                .ToListAsync();

            ImSessionDto? session = await task1;
            if (session == null)
            {
                return null;
            }
            IEnumerable<int> userIds = await task2;

            IEnumerable<UserDto> users = await userService.GetUsers(userIds.Distinct());
            //填充用戶信息
            session.Users = users;
            session.SessionName = GetShowSessionName(session, userId);
            session.CurrentUserCanSendMessage = session.DisableSendMessage.Equals(true) && !userId.ToString().Equals(session?.CreateBy);
            return session;
        }
    }
}
