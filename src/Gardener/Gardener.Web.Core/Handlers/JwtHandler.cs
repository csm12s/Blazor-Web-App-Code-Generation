// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Gardener.Enums;
using System;
using Gardener.Core;
using System.Threading.Tasks;

namespace Gardener.Web.Core
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
            //拦截所有验证需求
            //本验证不成立 不触发 PolicyPipeline

            var authorizationManager = httpContext.RequestServices.GetService<IAuthorizationManager>();

            if (await authorizationManager.IsSuperAdministrator())
                return true;

            // 获取权限特性
            var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
            if (securityDefineAttribute != null) return await authorizationManager.CheckSecurity(securityDefineAttribute.ResourceId);

            //没有特性的可以通过路由+请求方法查找


            HttpMethodType method = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), httpContext.Request.Method);
            string path = ((Microsoft.AspNetCore.Routing.RouteEndpoint)httpContext.GetEndpoint()).RoutePattern.RawText;
            return await authorizationManager.CheckSecurity(method, path);
        }
        public override async Task<bool> PolicyPipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext, IAuthorizationRequirement requirement)
        {
            //所有验证需求都会过来但我们只关心自己关注的，其它的有其它处理器处理
            if (requirement is AppAuthorizeRequirement) return true;

            return false;
        }

    }
}