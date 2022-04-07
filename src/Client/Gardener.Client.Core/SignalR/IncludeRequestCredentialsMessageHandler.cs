// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class IncludeRequestCredentialsMessageHandler : DelegatingHandler
    {
        private readonly Action<HttpRequestMessage> _httpRequestConfigure;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerHandler"></param>
        public IncludeRequestCredentialsMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerHandler"></param>
        /// <param name="httpRequestConfigure"></param>
        public IncludeRequestCredentialsMessageHandler(HttpMessageHandler innerHandler,Action<HttpRequestMessage> httpRequestConfigure) : base(innerHandler)
        {
            _httpRequestConfigure  = httpRequestConfigure;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpRequestConfigure != null) 
            {
                _httpRequestConfigure.Invoke(request);
            }

            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            request.SetBrowserRequestMode(BrowserRequestMode.Cors);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
