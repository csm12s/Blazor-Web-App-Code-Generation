// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// JWT 授权自定义处理程序
    /// </summary>
    public class JwtHandler : AppAuthorizeHandler
    {
        /// <summary>
        /// 请求管道
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            var authorizationManager = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            return await authorizationManager.ChecktContenxtApiEndpoint();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        public override Task<bool> PolicyPipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext, IAuthorizationRequirement requirement)
        {
            return Task.FromResult(true);
        }

    }
}