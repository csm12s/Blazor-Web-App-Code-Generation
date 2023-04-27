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
    /// 用户信息刷新后，连接signalRClient
    /// </summary>
    [TransientService]
    public class ReloadCurrentUserEventSubscriber : EventSubscriberBase<ReloadCurrentUserEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public ReloadCurrentUserEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public override Task CallBack(ReloadCurrentUserEvent e)
        {
            //无需等待
            signalRClientManager.ConnectionAndStartAll();
            return Task.CompletedTask;
        }
    }
}
