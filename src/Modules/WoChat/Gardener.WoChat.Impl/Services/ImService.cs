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
using Gardener.WoChat.Domains;
using Gardener.WoChat.Dtos;
using Gardener.WoChat.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gardener.WoChat.Services
{
    /// <summary>
    /// Im聊天服务
    /// </summary>
    [ApiDescriptionSettings("NotificationSystem")]
    public class ImService : IImService, IDynamicApiController
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IRepository<ImSession> imSessionRepository;
        private readonly IRepository<ImSessionMessage> imSessionMessageRepository;
        private readonly IRepository<ImUserSession> imUserSessionRepository;
        /// <summary>
        /// Im聊天服务
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="imSessionRepository"></param>
        /// <param name="imSessionMessageRepository"></param>
        /// <param name="imUserSessionRepository"></param>
        public ImService(IAuthorizationService authorizationService, IRepository<ImSession> imSessionRepository, IRepository<ImSessionMessage> imSessionMessageRepository, IRepository<ImUserSession> imUserSessionRepository)
        {
            this.authorizationService = authorizationService;
            this.imSessionRepository = imSessionRepository;
            this.imSessionMessageRepository = imSessionMessageRepository;
            this.imUserSessionRepository = imUserSessionRepository;
        }

        /// <summary>
        /// 添加会话
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddMyImSession(ImSessionAddInput input)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return false;
            }
            ImSession imSession = input.Adapt<ImSession>();
            imSession.Id = Guid.NewGuid();
            imSession.CreateBy = identity.Id;
            imSession.CreateIdentityType = identity.IdentityType;
            imSession.CreatedTime = DateTimeOffset.Now;

            await imSessionRepository.InsertAsync(imSession);

            foreach (var userId in input.UserIds)
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
            return true;
        }

        /// <summary>
        /// 获取会话列表
        /// </summary>
        /// <returns></returns>
        public Task<List<ImSessionDto>> GetImSessions()
        {
            return imUserSessionRepository.AsQueryable(false).Select(x => x.Adapt<ImSessionDto>()).ToListAsync();
        }
        /// <summary>
        /// 获取我的会话列表
        /// </summary>
        /// <returns></returns>
        public Task<List<ImSessionDto>> GetMyImSessions()
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return Task.FromResult<List<ImSessionDto>>(new()); ;
            }
            int userId = int.Parse(identity.Id);
            return imUserSessionRepository
                .AsQueryable(false)
                .Where(x => x.UserId.Equals(userId) && x.IsActive == true)
                .Select(x => x.Adapt<ImSessionDto>())
                .OrderByDescending(x => x.LastMessageTime)
                .ToListAsync();
        }
        /// <summary>
        /// 获取会话消息列表
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <param name="maxDateTime"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<ImSessionMessageDto>> GetMySessionMessages(Guid imSessionId, DateTimeOffset? maxDateTime, int pageSize = 100)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return new List<ImSessionMessageDto>(0);
            }
            int userId = int.Parse(identity.Id);
            if (await imUserSessionRepository
                .AsQueryable(false)
                .Where(x => x.UserId.Equals(userId)).AnyAsync())
            {
                return new List<ImSessionMessageDto>(0);
            }
            var query = imSessionMessageRepository.AsQueryable(false)
               .Where(x => x.ImSessionId.Equals(imSessionId));
            if (maxDateTime != null)
            {
                query = query.Where(x => x.CreatedTime > maxDateTime);
            }
            return await query
                .Select(x => x.Adapt<ImSessionMessageDto>())
                .OrderBy(x => x.CreatedTime)
                .Take(pageSize)
                .ToListAsync();
        }
        /// <summary>
        /// 退出会话
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 私聊直接删除，群里直接退出，自己创建的话直接解散
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
            sessionMessage.CreateIdentityType=identity.IdentityType;
            await imSessionMessageRepository.InsertAsync(sessionMessage);
            //发送
            return true;
        }
    }
}
