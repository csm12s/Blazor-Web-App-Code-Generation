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
    /// 重载用户后，连接系统通知
    /// </summary>
    [TransientService]
    public class ReloadCurrentUserEventSubscriber : IEventSubscriber<ReloadCurrentUserEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public ReloadCurrentUserEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public Task CallBack(ReloadCurrentUserEvent e)
        {
            return signalRClientManager.ConnectionAndStartAll();
        }
    }
}
