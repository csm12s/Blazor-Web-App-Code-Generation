// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientPageResourceAuthorizationHandler : AuthorizationHandler<ClientPageAuthorizationRequirement>
    {

        private readonly IAuthenticationStateManager authenticationStateManager;

        public ClientPageResourceAuthorizationHandler(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientPageAuthorizationRequirement requirement)
        {
            var resourceKey =context.Resource;

            //var userResources = authenticationStateManager.GetCurrentUserResources();

            //if (userResources.Any(x => x.Key.Equals(resourceKey)))
            //{
            //    Console.WriteLine($"{resourceKey} 验证通过");
            //    //如果当前用户有资源访问权限，则返回成功
            //    context.Succeed(requirement);
            //}
            //else
            //{ 
            //    Console.WriteLine($"{resourceKey} 验证未通过");
            //    context.Fail();
            //}
            return Task.CompletedTask;
        }
    }
}
