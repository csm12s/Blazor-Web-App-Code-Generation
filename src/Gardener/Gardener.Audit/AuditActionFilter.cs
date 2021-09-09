// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Audit.Domain;
using Gardener.Authorization;
using Gardener.Authorization.Domain;
using Gardener.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Audit
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditActionFilter : IAsyncActionFilter
    {
        private readonly IAuthorizationManager authorizationManager;
        private readonly AuditService auditDataManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationManager"></param>
        /// <param name="auditDataManager"></param>
        public AuditActionFilter(IAuthorizationManager authorizationManager,
            AuditService auditDataManager)
        {
            this.authorizationManager = authorizationManager;
            this.auditDataManager = auditDataManager;
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

            if (context.HttpContext.User.Identity.IsAuthenticated == false) { await next(); return; }


            Function function=null;
            User user = null;
            if (authorizationManager != null)
            {
                function = await authorizationManager.GetContenxtFunction();
                //资源未启用审计
                if (function != null && !function.EnableAudit) { await next(); return; }
                user = authorizationManager.GetUser();
            }
            HttpContext httpContext = context.HttpContext;
            StringValues ua = string.Empty;
            httpContext.Request.Headers.TryGetValue("User-Agent", out ua);
            HttpMethodType method = (HttpMethodType)Enum.Parse(typeof(HttpMethodType),httpContext.Request.Method.ToUpper());
            string path = httpContext.Request.Path.HasValue? httpContext.Request.Path.Value:null;
            string parameters = string.Empty;


            if (method.Equals(HttpMethodType.GET) || method.Equals(HttpMethodType.DELETE))
            {
                if(httpContext.Request.QueryString.HasValue) parameters = httpContext.Request.QueryString.Value;
            }
            else if (method.Equals(HttpMethodType.POST) || method.Equals(HttpMethodType.PUT) || method.Equals(HttpMethodType.PATCH))
            {
                parameters = await ReadBodyAsync(httpContext.Request);
                
            }
            
            AuditOperation auditOperation = new AuditOperation()
            {
                CreatedTime = DateTimeOffset.Now,
                Id = Guid.NewGuid(),
                Ip = httpContext.GetRemoteIpAddressToIPv4(),
                Path= path,
                Method= method,
                Parameters= parameters,
                UserAgent = ua.ToString(),
                OperaterId = user!=null?user.Id.ToString():null,
                OperaterName = user != null ? (user.NickName ?? user.UserName):null,
                ResourceId = function!=null? function.Id:Guid.Empty,
                ResourceName = function!=null? function.Service+":"+function.Summary:null
            };
            await auditDataManager.SaveAuditOperation(auditOperation);
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
            request.EnableBuffering();
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
