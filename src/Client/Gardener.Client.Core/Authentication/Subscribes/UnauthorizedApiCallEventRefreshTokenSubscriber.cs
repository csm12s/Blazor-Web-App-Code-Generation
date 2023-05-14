// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using System.Net;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Authentication.Subscribes
{
    /// <summary>
    /// 身份验证失败时刷新token
    /// </summary>
    [ScopedService]
    public class UnauthorizedApiCallEventRefreshTokenSubscriber : EventSubscriberBase<UnauthorizedApiCallEvent>
    {
        private object lockObj = new object();
        private Task? refreshTask = null;
        private readonly IAuthenticationStateManager authenticationStateManager;

        public UnauthorizedApiCallEventRefreshTokenSubscriber(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public override Task CallBack(UnauthorizedApiCallEvent e)
        {
            if (e.HttpStatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                if (refreshTask == null || refreshTask.IsCompleted)
                {
                    lock (lockObj)
                    {
                        if (refreshTask == null || refreshTask.IsCompleted)
                        {
                            refreshTask = authenticationStateManager.RefreshToken(true);
                        }
                    }
                }
                return refreshTask;
            }
            return Task.CompletedTask;
        }
    }
}
