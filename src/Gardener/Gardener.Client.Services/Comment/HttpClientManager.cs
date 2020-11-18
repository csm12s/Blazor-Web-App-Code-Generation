// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System.Net.Http;
using System.Net.Http.Headers;

namespace Gardener.Client.Services
{
    public  class HttpClientManager
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
            //httpClient.Authenticator = new JwtAuthenticator(token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
