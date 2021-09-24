// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class EventBusSimpleImpl : IEventBus
    {
        private IServiceProvider _serviceProvider;
        public EventBusSimpleImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Publish<TEvent>(TEvent e) where TEvent : EventBase
        {
            var handlers= _serviceProvider.GetServices<IEventHandler<TEvent>>();
            if (handlers != null) 
            {
                foreach (var handler in handlers)
                {
                    await handler.Handler(e);
                }
            }
        }
    }
}
