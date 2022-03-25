// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.EventBus
{
    /// <summary>
    /// 事件发送服务
    /// </summary>
    public class EventBusService : IEventBus
    {
        private readonly IEventPublisher eventPublisher;

        public EventBusService(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Publish<TEvent>(TEvent e, CancellationToken? cancellationToken) where TEvent : EventBase
        {
            EventSource<TEvent> eventSource = new EventSource<TEvent>(e.EventType.ToString() + e.EventGroup);
            eventSource.Body = e;
            if (cancellationToken.HasValue) 
            {
                eventSource.CancellationToken = cancellationToken.Value;
            }
            await eventPublisher.PublishAsync(eventSource);
        }
        
    }
}
