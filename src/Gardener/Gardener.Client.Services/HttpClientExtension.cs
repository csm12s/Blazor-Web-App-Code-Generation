// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

//namespace Gardener.Client.Services
//{
//    public static class HttpClientException
//    {
//        #region 标准的Http请求扩展
//        public static async Task<TRsp> GetAsync<TRsp>(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
//        {
//            var httpResponse = await client.GetAsync(requestUri, cancellationToken);
//            httpResponse.EnsureSuccessStatusCode();
//            TRsp rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
//            return rsp;
//        }
//        public static async Task<TRsp> PostAsync<TReq, TRsp>(this HttpClient client, string requestUri, TReq value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
//        {
//            var httpResponse = await client.PostAsJsonAsync<TReq>(requestUri, value, options, cancellationToken);
//            httpResponse.EnsureSuccessStatusCode();
//            TRsp rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
//            return rsp;
//        }
//        public static async Task PostAsync<TReq>(this HttpClient client, string requestUri, TReq value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
//        {
//            var httpResponse = await client.PostAsJsonAsync<TReq>(requestUri, value, options, cancellationToken);
//            httpResponse.EnsureSuccessStatusCode();
//        }
//        public static async Task<TRsp> PutAsync<TReq, TRsp>(this HttpClient client, string requestUri, TReq value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
//        {
//            var httpResponse = await client.PutAsJsonAsync<TReq>(requestUri, value, options, cancellationToken);
//            httpResponse.EnsureSuccessStatusCode();
//            TRsp rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
//            return rsp;
//        }
//        public static async Task PutAsync<TReq>(this HttpClient client, string requestUri, TReq value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
//        {
//            var httpResponse=await client.PutAsJsonAsync<TReq>(requestUri, value, options, cancellationToken);
//            httpResponse.EnsureSuccessStatusCode();
//        }
//        public static async Task<TRsp> DeleteAsync<TRsp>(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
//        {
//            var httpResponse = await client.DeleteAsync(requestUri, cancellationToken);
//            httpResponse.EnsureSuccessStatusCode();
//            TRsp rsp = await httpResponse.Content.ReadFromJsonAsync<TRsp>();
//            return rsp;
//        }
//        public static async Task DeleteAsync(this HttpClient client, string requestUri, CancellationToken cancellationToken = default)
//        {
//            var httpResponse = await client.DeleteAsync(requestUri, cancellationToken);
//            httpResponse.EnsureSuccessStatusCode();
//        }


//        #endregion
//    }
//}
