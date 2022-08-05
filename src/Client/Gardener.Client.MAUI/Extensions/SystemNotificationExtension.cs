// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Core;

namespace Gardener.Client.MAUI.Extensions
{
    public static class SystemNotificationExtension
    {
        /// <summary>
        /// SignalRClientManager
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSignalRClientManager(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<ISignalRClientBuilder, SignalRClientBuilder>();
            builder.Services.AddScoped<ISignalRClientManager, SignalRClientManager>();
        }
    }
}
