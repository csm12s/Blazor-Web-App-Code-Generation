// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.EventBus.Events;

namespace Gardener.NotificationSystem.Client.Subscribes
{
    /// <summary>
    /// 登出后端口系统通知
    /// </summary>
    [TransientService]
    public class LogoutSucceedAfterEventSubscriber : IEventSubscriber<LogoutSucceedAfterEvent>
    {
        private readonly SystemNotificationSignalRHandler handler;

        public LogoutSucceedAfterEventSubscriber(SystemNotificationSignalRHandler handler)
        {
            this.handler = handler;
        }

        public async Task CallBack(LogoutSucceedAfterEvent e)
        {
           await handler.Close();
        }
    }
}
