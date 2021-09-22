// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
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
            if (await authenticationStateManager.CheckCurrentUserHaveBtnResourceKey(context.Resource))
            {
                //如果当前用户有资源访问权限，则返回成功
                context.Succeed(requirement);
            }
        }
    }
}
