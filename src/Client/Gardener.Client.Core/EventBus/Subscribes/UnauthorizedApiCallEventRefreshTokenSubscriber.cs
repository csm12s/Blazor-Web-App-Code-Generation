// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Client.Core.EventBus.Subscribes
{
    /// <summary>
    /// 身份验证失败时刷新token
    /// </summary>
    [ScopedService]
    public class UnauthorizedApiCallEventRefreshTokenSubscriber : EventSubscriberBase<UnauthorizedApiCallEvent>
    {
        private object lockObj = new object();
        private int state = 0;
        private readonly IAuthenticationStateManager authenticationStateManager;

        public UnauthorizedApiCallEventRefreshTokenSubscriber(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public override async Task CallBack(UnauthorizedApiCallEvent e)
        {
            if (e.HttpStatusCode.Equals(HttpStatusCode.Unauthorized))
            {

                lock (lockObj)
                {
                    while (state==1)
                    {
                        if (state == 0)
                        {
                            state = 1;
                        }
                        else
                        if (state == 2)
                        {
                            state = 0;
                        }
                        Thread.Sleep(10);
                    }
                }
                if (state == 1)
                {
                    await authenticationStateManager.RefreshToken(true);
                    state = 2;
                }

            }
        }
    }
}
