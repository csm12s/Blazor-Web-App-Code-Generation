// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientUIResourceAuthorizationHandler : AuthorizationHandler<ClientUIAuthorizationRequirement>
    {

        private readonly IAuthenticationStateManager authenticationStateManager;

        public ClientUIResourceAuthorizationHandler(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientUIAuthorizationRequirement requirement)
        {
            if (context.Resource is ClientResource resource)
            {
                bool state = resource.AndCondition ? true : false;

                foreach (string key in resource.Keys)
                {
                    var isAuth = await authenticationStateManager.CheckCurrentUserHaveBtnResourceKey(key);
                    if (resource.AndCondition)
                    {
                        if (!isAuth)
                        {
                            state = false;
                        }
                    }
                    else
                    {
                        if (isAuth)
                        {
                            state = true;
                        }
                    }
                }

                if (state)
                {
                    //如果当前用户有资源访问权限，则返回成功
                    context.Succeed(requirement);
                }
                else 
                {
                    context.Fail();
                }
            }
        }
    }
}
