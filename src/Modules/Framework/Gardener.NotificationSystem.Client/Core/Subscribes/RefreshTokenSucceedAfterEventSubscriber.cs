// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.NotificationSystem.Client.Subscribes
{
    /// <summary>
    /// 刷新token后，修改通知系统的token
    /// </summary>
    [TransientService]
    public class RefreshTokenSucceedAfterEventSubscriber : IEventSubscriber<RefreshTokenSucceedAfterEvent>
    {
        private readonly SystemNotificationSignalRHandler handler;

        public RefreshTokenSucceedAfterEventSubscriber(SystemNotificationSignalRHandler handler)
        {
            this.handler = handler;
        }

        public Task CallBack(RefreshTokenSucceedAfterEvent e)
        {
            
            return Task.CompletedTask;
        }
    }
}
