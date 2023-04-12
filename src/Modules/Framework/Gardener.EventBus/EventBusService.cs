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
        /// 发布消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task PublishAsync(EventBase e, CancellationToken? cancellationToken = null)
        {
            EventSource<EventBase> eventSource = new EventSource<EventBase>(e.EventType.ToString() + e.EventGroup);
            eventSource.Body = e;
            if (cancellationToken.HasValue)
            {
                eventSource.CancellationToken = cancellationToken.Value;
            }
            return eventPublisher.PublishAsync(eventSource);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public void Publish(EventBase e, CancellationToken? cancellationToken = null)
        {
            PublishAsync(e, cancellationToken);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="callBack"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Subscriber Subscribe<TEvent>(Func<TEvent, Task> callBack) where TEvent : EventBase
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 解绑
        /// </summary>
        /// <param name="subscriber"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UnSubscribe(Subscriber subscriber)
        {
            throw new NotImplementedException();
        }
    }
}
