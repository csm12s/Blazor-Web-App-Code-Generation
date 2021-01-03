// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Core.Entites;
using Gardener.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Core.Audit
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditActionFilter : IAsyncActionFilter
    {
        private readonly IAuthorizationManager authorizationManager;
        private readonly IAuditDataManager auditDataManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationManager"></param>
        /// <param name="auditDataManager"></param>
        public AuditActionFilter(IAuthorizationManager authorizationManager, IAuditDataManager auditDataManager)
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
            if (context.HttpContext.User.Identity.IsAuthenticated == false) { await next(); return; }
            User user = authorizationManager.GetUser();
            if (user == null) { await next(); return; }

            Resource resource = await authorizationManager.GetContenxtResource();
            //资源未启用审计
            if(resource!=null && !resource.EnableAudit) { await next(); return; }

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
                OperaterId = user.Id.ToString(),
                OperaterName = user.NickName ?? user.UserName,
                ResourceId = resource!=null? resource.Id:Guid.Empty,
                ResourceName = resource!=null? resource.Name:null
            };
            await auditDataManager.SaveAuditOperation(auditOperation);
            await next();
        }
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
    }
}
