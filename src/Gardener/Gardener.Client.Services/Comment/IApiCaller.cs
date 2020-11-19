// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IApiCaller
    {
        Task DeleteAsync(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> GetFromJsonAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> PostFromJsonAsync<TRequest, TResponse>(string url, TRequest request = default(TRequest));
        Task PutFromJsonAsync<TRequest>(string url, TRequest request = default);
        Task<TResponse> PutFromJsonAsync<TRequest, TResponse>(string url, TRequest request = default(TRequest));
    }
}