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
using Gardener.WoChat.Domains;
using Gardener.WoChat.Dtos;
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
        public async Task<bool> AddImSession(ImSessionAddInput input)
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
                ImUserSession imUserSession = new ImUserSession();
                imUserSession.UserId = userId;
                imUserSession.ImSessionId = imSession.Id;
                imUserSession.Id = Guid.NewGuid();
                imUserSession.CreateBy = identity.Id;
                imUserSession.CreateIdentityType = identity.IdentityType;
                imUserSession.CreatedTime = DateTimeOffset.Now;
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
            return imUserSessionRepository.AsQueryable(false).Select(x=>x.Adapt<ImSessionDto>()).ToListAsync();
        }
        /// <summary>
        /// 获取我的会话列表
        /// </summary>
        /// <returns></returns>
        public Task<List<ImSessionDto>> GetMyImSessions()
        {
            return imUserSessionRepository.AsQueryable(false).Select(x => x.Adapt<ImSessionDto>()).ToListAsync();
        }
        /// <summary>
        /// 获取会话消息列表
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <param name="maxDateTime"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<ImSessionMessageDto>> GetSessionMessages(Guid imSessionId, DateTimeOffset? maxDateTime, int? pageSize = 100)
        {
            Identity? identity = authorizationService.GetIdentity();
            if (identity == null || !IdentityType.User.Equals(identity.IdentityType))
            {
                return Task.FromResult(new List<ImSessionMessageDto>(0));
            }
            return imSessionMessageRepository.AsQueryable(false).Select(x => x.Adapt<ImSessionMessageDto>()).ToListAsync();
        }

        public Task<bool> RemoveImSession(Guid imSessionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendMessage(ImSessionMessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
