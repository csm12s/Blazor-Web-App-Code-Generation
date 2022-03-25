// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Gardener.NotificationSystem.Client
{
    /// <summary>
    /// 系统通知
    /// </summary>
    public static class SystemNotificationExtension
    {
        /// <summary>
        /// 系统通知
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSystemNotifyNotification(this WebAssemblyHostBuilder builder) 
        {
            builder.Services.AddScoped<SystemNotificationSignalRHandler, SystemNotificationSignalRHandler>();
        }

    }
}
