// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.EventBus.Events;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Subscribes
{
    /// <summary>
    /// 登出后端口系统通知
    /// </summary>
    [TransientService]
    public class LogoutSucceedAfterEventSubscriber : EventSubscriberBase<LogoutSucceedAfterEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public LogoutSucceedAfterEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public override Task CallBack(LogoutSucceedAfterEvent e)
        {
            return signalRClientManager.StopAll();
        }
    }
}
