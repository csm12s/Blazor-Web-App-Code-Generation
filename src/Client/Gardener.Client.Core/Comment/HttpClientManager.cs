// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Constants;
using Gardener.Client.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Gardener.Client.Core
{
    [ScopedService]
    public class HttpClientManager
    {
        private readonly HttpClient httpClient;

        public HttpClientManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void SetClientAuthorization(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GardenerAuthenticationSchemes.User, token);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string>? GetAuthorizationHeaders()
        {
            if (httpClient.DefaultRequestHeaders.Authorization == null)
            {
                return null;
            }
            return new KeyValuePair<string, string>(nameof(httpClient.DefaultRequestHeaders.Authorization), httpClient.DefaultRequestHeaders.Authorization.Scheme + " " + httpClient.DefaultRequestHeaders.Authorization.Parameter ?? string.Empty);
        }
    }
}
