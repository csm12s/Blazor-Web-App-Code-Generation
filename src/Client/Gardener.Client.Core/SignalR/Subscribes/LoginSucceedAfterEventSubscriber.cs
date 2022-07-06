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
    /// 登录成功后，连接系统通知
    /// </summary>
    [TransientService]
    public class LoginSucceedAfterEventSubscriber : EventSubscriberBase<LoginSucceedAfterEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public LoginSucceedAfterEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public override Task CallBack(LoginSucceedAfterEvent e)
        {
            return signalRClientManager.ConnectionAndStartAll();
        }
    }
}
