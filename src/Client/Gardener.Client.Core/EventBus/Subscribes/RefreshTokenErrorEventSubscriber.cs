// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus.Subscribes
{
    /// <summary>
    /// 刷新token失败事件处理器
    /// </summary>
    [TransientService]
    public class RefreshTokenErrorEventSubscriber : EventSubscriberBase<RefreshTokenErrorEvent>
    {
        private readonly IAuthenticationStateManager authenticationStateManager;

        public RefreshTokenErrorEventSubscriber(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public override async Task CallBack(RefreshTokenErrorEvent e)
        {
            await authenticationStateManager.CleanUserInfo();
        }
    }
}
