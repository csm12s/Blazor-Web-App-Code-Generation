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
        Task DeleteAsync(string controller, string action = "", IDictionary<string, object> queryString = null);
        Task<TResponse> GetFromJsonAsync<TResponse>(string controller, string action = "", IDictionary<string, object> queryString = null);
        Task<TResponse> GetFromJsonAsyncAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> PostFromJsonAsyncAsync<TRequest, TResponse>(string url, TRequest request = default(TRequest));
        Task<TResponse> PostFromJsonAsyncAsync<TRequest, TResponse>(string controller, string action="", TRequest request=default(TRequest));
        Task PutFromJsonAsyncAsync<TRequest>(string url, TRequest request = default);
        Task<TResponse> PutFromJsonAsyncAsync<TRequest, TResponse>(string url, TRequest request = default(TRequest));
        Task<TResponse> PutFromJsonAsyncAsync<TRequest, TResponse>(string controller, string action = "", TRequest request = default(TRequest));
    }
}