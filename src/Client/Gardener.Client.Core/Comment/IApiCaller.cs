// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    public interface IApiCaller
    {
        Task DeleteAsync(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> GetAsync<TResponse>(string url);
        Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, object> queryString);
        Task<TResponse> GetAsync<TResponse>(string url, List<KeyValuePair<string, object>> queryString);
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request = default);
        Task PostAsync<TRequest>(string url, TRequest request = default);
        Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request = default);
        Task PutAsync<TRequest>(string url, TRequest request = default);
    }
}