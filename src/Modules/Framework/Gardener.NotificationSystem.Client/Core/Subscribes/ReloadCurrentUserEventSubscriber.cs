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
    /// 重载用户后，连接系统通知
    /// </summary>
    [TransientService]
    public class ReloadCurrentUserEventSubscriber : IEventSubscriber<ReloadCurrentUserEvent>
    {
        private readonly SystemNotificationSignalRHandler handler;

        public ReloadCurrentUserEventSubscriber(SystemNotificationSignalRHandler handler)
        {
            this.handler = handler;
        }

        public async Task CallBack(ReloadCurrentUserEvent e)
        {
          await handler.Start();
        }
    }
}
