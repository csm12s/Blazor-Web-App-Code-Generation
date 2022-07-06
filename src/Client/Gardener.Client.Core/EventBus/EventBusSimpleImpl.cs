// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus
{
    /// <summary>
    /// 事件通知
    /// </summary>
    [ScopedService]
    public class EventBusSimpleImpl : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IClientLogger _logger;
        private readonly Dictionary<String, List<Subscriber>> _subscribers = new Dictionary<String, List<Subscriber>>();
        public EventBusSimpleImpl(IServiceProvider serviceProvider, IClientLogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Publish(EventBase e, CancellationToken? cancellationToken)
        {
            string typeName = e.GetType().FullName;
            _logger.Info($"eventBus publish event {e.GetType().FullName}  {System.Text.Json.JsonSerializer.Serialize(e)}");
            List<Task> tasks = new List<Task>();
            //静态注册订阅者
            var staticSubscribers = _serviceProvider.GetServices<IEventSubscriber>();
            if (staticSubscribers != null)
            {
                foreach (var handler in staticSubscribers)
                {
                    try
                    {
                        if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                        {
                            break;
                        }
                        tasks.Add(handler.CallBack(e));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"eventBus event handler error {e.GetType().FullName}  {System.Text.Json.JsonSerializer.Serialize(e)}", ex: ex);
                    }
                }

            }
            //动态注册订阅者
            if (_subscribers.TryGetValue(typeName, out List<Subscriber> subscribers))
            {
                //循环订阅者
                foreach (var subscriber in subscribers)
                {
                    try
                    {
                        if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                        {
                            break;
                        }
                        tasks.Add(subscriber.CallBack(e));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"eventBus subscriber error {e.GetType().FullName}  {System.Text.Json.JsonSerializer.Serialize(e)}", ex: ex);
                    }
                }

            }
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public Subscriber Subscribe<TEvent>(Func<TEvent, Task> callBack) where TEvent : EventBase
        {
            Subscriber subscriber = new Subscriber(typeof(TEvent), e => callBack((TEvent)e));
            lock (_subscribers)
            {
                string typeName = typeof(TEvent).FullName;
                
                if (!_subscribers.TryGetValue(typeName, out List<Subscriber> subscribers))
                {
                    subscribers = new List<Subscriber>();
                    _subscribers.Add(typeName, subscribers);
                }
                subscribers.Add(subscriber);
            }
            return subscriber;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="subscriber"></param>
        public void UnSubscribe(Subscriber subscriber)
        {
            if (_subscribers.TryGetValue(subscriber.EventType.FullName, out List<Subscriber> subscribers))
            {
                subscribers.RemoveAll(x => x.Id.Equals(subscriber.Id));
            }
        }
        
    }
}
