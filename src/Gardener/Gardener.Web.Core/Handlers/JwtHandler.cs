using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
        public override bool Pipeline(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            // 检查权限
            return CheckAuthorzie(context);
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static bool CheckAuthorzie(AuthorizationHandlerContext httpContext)
        {
            // 获取权限特性
            //var securityDefineAttribute = httpContext.GetMetadata<ResourceDefineAttribute>();
            //if (securityDefineAttribute == null) return true;

            //ControllerActionDescriptor controllerActionDescriptor= httpContext.GetMetadata<ControllerActionDescriptor>();
            //string method = controllerActionDescriptor.DisplayName;
            //string parameters = string.Join('-', controllerActionDescriptor.Parameters.Select(x => x.ParameterType.FullName + "_" + x.Name));
            //if (parameters is not null  and not "")
            //{
            //    method += "-" + parameters;
            //}
            //return App.GetService<IAuthorizationManager>().CheckSecurity(MD5Encryption.Encrypt(method));
            return true;
        }

    }
}