// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using Gardener.EventBus;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventBusServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptionsBuilder"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBusServices(this IServiceCollection services, Action<EventBusOptionsBuilder> configureOptionsBuilder)
        {
            //实体变化发布者
            services.AddScoped<IEntityEventPublisher, EntityEventPublisher>();
            services.AddEventBus(configureOptionsBuilder);
            return services;
        }
    }
}
