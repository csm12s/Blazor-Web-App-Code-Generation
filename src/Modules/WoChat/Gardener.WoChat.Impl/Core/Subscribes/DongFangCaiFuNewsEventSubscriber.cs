// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.EventBus;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.EasyJob.Dtos;
using Gardener.EventBus;
using Gardener.WoChat.Domains;
using Gardener.WoChat.Enums;
using Gardener.WoChat.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.WoChat.Impl.Core.Subscribes
{
    /// <summary>
    /// 订阅东方财富资讯
    /// </summary>
    public class DongFangCaiFuNewsEventSubscriber : IEventSubscriber, ISingleton
    {

        private readonly IServiceScopeFactory _scopeFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopeFactory"></param>
        public DongFangCaiFuNewsEventSubscriber(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 订阅东方财富资讯
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.SystemNotify) + nameof(DongFangCaiFuNewsEvent))]
        public async Task News(EventHandlerExecutingContext context)
        {
            using var serviceScope = _scopeFactory.CreateScope();
            IEventSource eventSource = context.Source;
            DongFangCaiFuNewsEvent news = (DongFangCaiFuNewsEvent)eventSource.Payload;
            int userId = 9;
            IRepository<ImUserSession> imUserSessionRepository = serviceScope.ServiceProvider.GetRequiredService<IRepository<ImUserSession>>();
            List<Guid> imSessionIds = imUserSessionRepository.AsQueryable(false).Where(x => x.UserId.Equals(userId)).Select(x => x.ImSessionId).Distinct().ToList();
            var imService = serviceScope.ServiceProvider.GetRequiredService<IWoChatImService>();
            List<Task> tasks = new List<Task>();
            foreach (var im in imSessionIds)
            {
                tasks.Add(imService.SendMessage(new Dtos.ImSessionMessageDto()
                {
                    ImSessionId = im,
                    UserId = userId,
                    MessageType=ImMessageType.Text,
                    Message= news.Content

                }, new Identity()
                {
                    Id = userId.ToString(),
                    IdentityType = IdentityType.User
                }));
            }
            await Task.WhenAll(tasks);
        }
    }
}
