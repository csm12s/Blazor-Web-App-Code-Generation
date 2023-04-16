// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
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

namespace Gardener.WoChat.Services
{
    /// <summary>
    /// Im聊天服务
    /// </summary>
    [ApiDescriptionSettings("NotificationSystem")]
    public class WoChatImService : IWoChatImService, IDynamicApiController
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
            if (input.SessionType.Equals(ImSessionType.Personal))
            {
                userIds.Add(currentUserId);
                //判断私聊是否存在
                string signature = GetSignature(userIds);
                var session = await imSessionRepository.Where(x => x.SessionType.Equals(ImSessionType.Personal) && x.UsersSignature.Equals(signature)).FirstOrDefaultAsync();
                if (session!=null)
                {
                    //已存在
                    return session.Id;
                }
            }

            ImSession imSession = input.Adapt<ImSession>();
            imSession.Id = Guid.NewGuid();
            imSession.UsersSignature = GetSignature(userIds);
            imSession.CreateBy = identity.Id;
            imSession.CreateIdentityType = identity.IdentityType;
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
                    CreateBy = identity.Id,
                    CreateIdentityType = identity.IdentityType,
                    CreatedTime = DateTimeOffset.Now
                };
                await imUserSessionRepository.InsertAsync(imUserSession);
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
            IEnumerable<int> userIds = imUsersessionDtos.Select(x => (int)(x.UserId)).Distinct();

            var task1 = imSessionRepository.AsQueryable(false)
                .Where(x => sessionIds.Contains(x.Id))
                .Select(x => x.Adapt<ImSessionDto>())
                .ToListAsync();

            var task2 = userService.GetUsers(userIds);

            IEnumerable<ImSessionDto> sessions = await task1;
            IEnumerable<UserDto> users = await task2;
            if (sessions.Any())
            {
                //填充用戶信息
                foreach (ImSessionDto session in sessions)
                {
                    session.Users = users.Where(x => x.Id == userId);
                }
            }
            return sessions;
        }
        /// <summary>
        /// 获取会话消息列表
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <param name="maxDateTime"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ImSessionMessageDto>> GetMySessionMessages([FromQuery] Guid imSessionId, [FromQuery] DateTimeOffset? maxDateTime, [FromQuery] int pageSize = 100)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return new ImSessionMessageDto[0];
            }
            int userId = int.Parse(identity.Id);
            if (await imUserSessionRepository
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
            IEnumerable<ImSessionMessageDto> messages = await query
                .Select(x => x.Adapt<ImSessionMessageDto>())
                .OrderBy(x => x.CreatedTime)
                .Take(pageSize)
                .ToListAsync();
            if (messages.Any())
            {
                //填充用戶信息
                var users = await userService.GetUsers(messages.Select(x => x.UserId));
                foreach (ImSessionMessageDto message in messages)
                {
                    message.User = users.Where(x => x.Id == message.UserId).FirstOrDefault();
                }
            }
            return messages;
        }
        /// <summary>
        /// 退出会话
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 私聊直接删除，群聊直接退出，自己创建的话直接解散
        /// </remarks>
        public async Task<bool> QuitMyImSession(Guid imSessionId)
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
            if (session.SessionType.Equals(ImSessionType.Personal))
            {
                List<Task> tasks = new()
                {
                    //私聊直接删除
                    imSessionRepository.DeleteAsync(imSessionId)
                };
                userSessions.ForEach(x =>
                {
                    tasks.Add(imUserSessionRepository.DeleteAsync(x.Id));
                });
                await Task.WhenAll(tasks);
            }
            else if (session.SessionType.Equals(ImSessionType.Group))
            {
                //群聊直接退出，自己创建的话直接解散
            }
            return true;
        }
        /// <summary>
        /// 移除会话
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 仅仅隐藏会话
        /// </remarks>
        public async Task<bool> RemoveMyImSession(Guid imSessionId)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return false;
            }
            int userId = int.Parse(identity.Id);
            var userSession = await imUserSessionRepository.AsQueryable(false).Where(x => x.IsActive.Equals(true) && x.UserId.Equals(userId)).FirstOrDefaultAsync();
            if (userSession == null)
            {
                return false;
            }

            userSession.IsActive = false;
            userSession.UpdatedTime = DateTime.UtcNow;
            userSession.UpdateBy = identity.Id;
            userSession.UpdateIdentityType = identity.IdentityType;
            await imUserSessionRepository.UpdateIncludeAsync(userSession,
                new string[] {
                    nameof(ImUserSession.IsActive),
                    nameof(ImUserSession.UpdateBy),
                    nameof(ImUserSession.UpdatedTime),
                    nameof(ImUserSession.UpdateIdentityType)
                });
            return true;
        }
        /// <summary>
        /// 发送消息到会话
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> SendMessage(ImSessionMessageDto message)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return false;
            }
            int userId = int.Parse(identity.Id);

            ImSessionMessage sessionMessage = message.Adapt<ImSessionMessage>();
            sessionMessage.UserId = userId;
            sessionMessage.CreatedTime = DateTimeOffset.Now;
            sessionMessage.CreateBy = identity.Id;
            sessionMessage.CreateIdentityType = identity.IdentityType;
            await imSessionMessageRepository.InsertAsync(sessionMessage);
            //发送
            var user = await userService.Get(userId);
            message.User = user;
            await systemNotificationService.SendToGroup(WoChatUtil.GetImGroupName(message.ImSessionId), new WoChatImMessageNotificationData()
            {
                Identity = identity,
                ImMessage = message,
            });
            return true;
        }
    }
}
