// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace Gardener.NotificationSystem.Options
{
    /// <summary>
    /// SignalR配置
    /// </summary>
    public class SignalROptions: IConfigurableOptions
    {
        /// <summary>
        /// 系统通知配置
        /// </summary>
        public SystemNotificationHubOptions? SystemNotificationHub { get; set; }
    }
}
