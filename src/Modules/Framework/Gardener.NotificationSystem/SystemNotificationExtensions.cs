// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Core;
using Gardener.NotificationSystem.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gardener.NotificationSystem
{
    /// <summary>
    /// 系统通知服务扩展
    /// </summary>
    public static class SystemNotificationExtensions
    {

        /// <summary>
        /// 添加系统通知服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSystemNotify(this IServiceCollection services)
        {
            //添加配置信息
            services.AddConfigurableOptions<SignalROptions>();
            // 添加即时通讯
            services.AddSignalR();
            services.TryAddSingleton<IUserIdProvider, JwtUserIdProvider>();
            services.TryAddSingleton<ISystemNotificationService, SystemNotificationService>();

            return services;
        }
    }
}
