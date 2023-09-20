// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// Http Job 参数
    /// </summary>
    public sealed class HttpJobProperties
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        /// <param name="requestUri"></param>
        public HttpJobProperties(string requestUri)
        {
            RequestUri = requestUri;
        }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUri { get; set; }


        /// <summary>
        /// 请求方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

        /// <summary>
        /// 请求报文体
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// 请求客户端名称
        /// </summary>
        public string ClientName { get; set; } = "HttpJob";

        /// <summary>
        /// 确保请求成功，否则抛异常
        /// </summary>
        public bool EnsureSuccessStatusCode { get; set; } = true;
    }
}
