// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.Authorization.Core;
using Gardener.Authorization.Dtos;
using Gardener.Base.Entity.Domains;
using Gardener.EntityFramwork.EFAudit;
using Gardener.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Audit.Core
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditActionFilter : IAsyncActionFilter
    {
        private readonly IAuthorizationService authorizationManager;
        private readonly IOrmAuditService auditService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationManager"></param>
        /// <param name="auditService"></param>
        public AuditActionFilter(IAuthorizationService authorizationManager,
            IOrmAuditService auditService)
        {
            this.authorizationManager = authorizationManager;
            this.auditService = auditService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IgnoreAuditAttribute ignoreAudit = context.HttpContext.GetMetadata<IgnoreAuditAttribute>();
            if (ignoreAudit != null) { await next(); return; }

            if (context.HttpContext.User.Identity == null || context.HttpContext.User.Identity.IsAuthenticated == false) { await next(); return; }


            ApiEndpoint? api = null;
            Identity? identity = null;
            if (authorizationManager != null)
            {
                api = await authorizationManager.GetApiEndpoint();
                //资源未启用审计
                if (api != null && !api.EnableAudit) { await next(); return; }
                identity = authorizationManager.GetIdentity();
            }
            HttpContext httpContext = context.HttpContext;
            StringValues ua = string.Empty;
            httpContext.Request.Headers.TryGetValue("User-Agent", out ua);
            HttpMethod method = (HttpMethod)Enum.Parse(typeof(HttpMethod), httpContext.Request.Method.ToUpper());
            string? path = httpContext.Request.Path.HasValue ? httpContext.Request.Path.Value : null;
            string parameters = string.Empty;


            if (method.Equals(HttpMethod.GET) || method.Equals(HttpMethod.DELETE))
            {
                if (httpContext.Request.QueryString.Value != null)
                {
                    parameters = httpContext.Request.QueryString.Value;
                }
            }
            else if (method.Equals(HttpMethod.POST) || method.Equals(HttpMethod.PUT) || method.Equals(HttpMethod.PATCH))
            {
                parameters = await ReadBodyAsync(httpContext.Request);
            }

            Guid? resourceId = api != null ? api.Id : null;
            string? resourceName = (api != null ? $"{api.Service}:{api.Summary}-{api.Description}" : null);

            AuditOperation auditOperation = new AuditOperation()
            {
                CreatedTime = DateTimeOffset.Now,
                Id = Guid.NewGuid(),
                Ip = httpContext.GetRemoteIpAddressToIPv4(),
                Path = path,
                Method = method,
                Parameters = parameters,
                UserAgent = ua.ToString(),
                OperaterId = identity != null ? identity.Id.ToString() : null,
                OperaterName = identity != null ? (identity.NickName ?? identity.Name) : null,
                OperaterType = identity != null ? identity.IdentityType : IdentityType.Unknown,
                ResourceId = resourceId,
                ResourceName = resourceName,
                CreateBy = identity?.Id,
                CreateIdentityType = identity?.IdentityType,
                TenantId = identity?.TenantId,
            };
            await auditService.SaveAuditOperation(auditOperation);
            await next();
        }

        #region private
        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Encoding GetRequestEncoding(HttpRequest request)
        {
            var requestContentType = request.ContentType;
            var requestMediaType = requestContentType == null ? default(MediaType) : new MediaType(requestContentType);
            var requestEncoding = requestMediaType.Encoding;
            if (requestEncoding == null)
            {
                requestEncoding = Encoding.UTF8;
            }
            return requestEncoding;
        }
        /// <summary>
        /// 读取body
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> ReadBodyAsync(HttpRequest request)
        {
            string result = string.Empty;
            request.Body.Position = 0;
            var stream = request.Body;
            long? length = request.ContentLength;
            if (length != null && length > 0)
            {
                StreamReader streamReader = new StreamReader(stream, GetRequestEncoding(request));
                result = await streamReader.ReadToEndAsync();
            }
            request.Body.Position = 0;
            return result;
        }
        #endregion

    }
}
