using Fur;
using Fur.Authorization;
using Fur.DataEncryption;
using Gardener.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;

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
            var isValid = context.ValidateJwtBearer(httpContext, out JsonWebToken token);
            if (!isValid) return false;

            // 检查权限
            return CheckAuthorzie(httpContext);
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static bool CheckAuthorzie(DefaultHttpContext httpContext)
        {
            // 获取权限特性
            var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
            if (securityDefineAttribute == null) return true;

            ControllerActionDescriptor controllerActionDescriptor= httpContext.GetMetadata<ControllerActionDescriptor>();
            string method = controllerActionDescriptor.DisplayName;
            string parameters = string.Join('-', controllerActionDescriptor.Parameters.Select(x => x.ParameterType.FullName + "_" + x.Name));
            if (parameters is not null  and not "")
            {
                method += "-" + parameters;
            }
            return App.GetService<IAuthorizationManager>().CheckSecurity(MD5Encryption.Encrypt(method));
        }
    }
}