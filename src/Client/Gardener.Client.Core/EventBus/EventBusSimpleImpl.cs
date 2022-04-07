// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        public EventBusSimpleImpl(IServiceProvider serviceProvider, IClientLogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Publish<TEvent>(TEvent e, CancellationToken? cancellationToken) where TEvent : EventBase
        {
            _logger.Info($"eventBus publish event {e.GetType().FullName}  {System.Text.Json.JsonSerializer.Serialize(e)}");
            var handlers = _serviceProvider.GetServices<IEventSubscriber<TEvent>>();
            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    try
                    {
                        if(cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested) 
                        {
                            break;
                        }
                        if (!handler.Ignore(e)) 
                        {
                           await handler.CallBack(e);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"eventBus event handler error {e.GetType().FullName}  {System.Text.Json.JsonSerializer.Serialize(e)}", ex: ex);
                    }
                }
            }
        }
    }
}
