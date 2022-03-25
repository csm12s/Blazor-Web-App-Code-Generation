// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using System.Net;
using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [TransientService]
    public class UnauthorizedApiCallEventSubscriber : IEventSubscriber<UnauthorizedApiCallEvent>
    {

        private readonly IAuthenticationStateManager authenticationStateManager;

        public UnauthorizedApiCallEventSubscriber(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public async Task CallBack(UnauthorizedApiCallEvent e)
        {
            if (e.HttpStatusCode.Equals(HttpStatusCode.Unauthorized)) 
            {
                await authenticationStateManager.RefreshToken();
            }
        }
    }
}
