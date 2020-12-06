// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client
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

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientUIAuthorizationRequirement requirement)
        {
            var resourceKey =(string)context.Resource;

            var userResources = authenticationStateManager.GetCurrentUserResources();

            if (userResources.Any(x => x.Key.Equals(resourceKey)))
            {
                //如果当前用户有资源访问权限，则返回成功
                context.Succeed(requirement);
            }
            else
            { 
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
