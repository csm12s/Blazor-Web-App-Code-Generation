// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// api 调用器
    /// </summary>
    public interface IApiCaller
    {
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task DeleteAsync(string url, IDictionary<string, object> queryString = null);
        /// <summary>
        /// delete
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string url);
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, object> queryString);
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string url, List<KeyValuePair<string, object>> queryString);
        /// <summary>
        /// post
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request = default);
        /// <summary>
        /// post
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task PostAsync<TRequest>(string url, TRequest request = default);
        /// <summary>
        /// post
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<TResponse> PostWithoutBodyAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        /// <summary>
        /// put
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request = default);
        /// <summary>
        /// put
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task PutAsync<TRequest>(string url, TRequest request = default);
    }
}