// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IApiCaller
    {
        Task DeleteAsync(string url, IDictionary<string, object> queryString = null);
        Task<ApiResult<TResponse>> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        Task<ApiResult<TResponse>> GetAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest request = default);
        Task PostAsync<TRequest>(string url, TRequest request = default);
        Task<ApiResult<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest request = default);
        Task PutAsync<TRequest>(string url, TRequest request = default);
    }
}