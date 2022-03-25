// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gardener.NotificationSystem
{
    /// <summary>
    /// 
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
            // 添加即时通讯
            services.AddSignalR();
            services.TryAddSingleton<IUserIdProvider, JwtUserIdProvider>();
            services.TryAddScoped<ISystemNotificationService, SystemNotificationService>();

            return services;
        }
    }
}
