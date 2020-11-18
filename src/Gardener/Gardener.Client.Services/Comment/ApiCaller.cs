// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class ApiCaller : IApiCaller
    {
        private HttpClient httpClient;
        private ILogger log;

        public ApiCaller(HttpClient httpClient, ILogger log)
        {
            this.httpClient = httpClient;
            this.log = log;
        }

        private string GetUrl(string url, IDictionary<string, object> queryString = null)
        {
            if (queryString != null && queryString.Count > 0)
            {
                url = QueryHelpers.AddQueryString(url, queryString.ToDictionary(p => p.Key, p => p.Value?.ToString()));
            }
            return url;
        }
        #region post
        public async Task PostFromJsonAsyncAsync<TRequest>(string url, TRequest request = default(TRequest))
        {
            try
            {
                url = GetUrl(url);
                await httpClient.PostFromJsonAsync<TRequest>(url, request);
            }
            catch (Exception ex)
            {
                await log.Error($"服务异常:{ex.Message}", ex);
            }
        }
        public async Task PostFromJsonAsyncAsync<TRequest>(string controller, string action = "", TRequest request = default(TRequest))
        {
            var url = $"{controller}/{action}";
            await PostFromJsonAsyncAsync<TRequest>(url, request);
        }
        public async Task<TResponse> PostFromJsonAsyncAsync<TRequest, TResponse>(string url, TRequest request = default(TRequest))
        {
            try
            {
                url = GetUrl(url);
                var result = await httpClient.PostFromJsonAsync<TRequest, ApiResult<TResponse>>(url, request);
                if (result.Successed)
                {
                    return result.Data;
                }
                else
                {
                    await log.Error($"服务异常:{result.Errors}({result.StatusCode})");
                }
                return default(TResponse);
            }
            catch (Exception ex)
            {
                await log.Error($"服务异常:{ex.Message}", ex);
                return default(TResponse);
            }
        }
        public async Task<TResponse> PostFromJsonAsyncAsync<TRequest, TResponse>(string controller, string action = "", TRequest request = default(TRequest))
        {
            var url = $"{controller}/{action}";
            return await PostFromJsonAsyncAsync<TRequest, TResponse>(url, request);
        }
        #endregion

        #region get
        public async Task<TResponse> GetFromJsonAsyncAsync<TResponse>(string url, IDictionary<string, object> queryString = null)
        {
            try
            {
                url = GetUrl(url, queryString);

                var result = await httpClient.GetFromJsonAsync<ApiResult<TResponse>>(url);
                if (result.Successed)
                {
                    return result.Data;
                }
                else
                {
                    await log.Error($"服务异常:{result.Errors}({result.StatusCode})");
                }
                return default(TResponse);
            }
            catch (Exception ex)
            {
                await log.Error($"服务异常:{ex.Message}", ex);
                return default(TResponse);
            }
        }
        public async Task<TResponse> GetFromJsonAsync<TResponse>(string controller, string action, IDictionary<string, object> queryString = null)
        {
            var url = $"{controller}/{action}";
            return await GetFromJsonAsyncAsync<TResponse>(url, queryString);
        }
        #endregion
        #region delete
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string url, IDictionary<string, object> queryString = null)
        {
            try
            {
              HttpResponseMessage httpResponse=  await httpClient.DeleteAsync(GetUrl(url, queryString));
                if (!httpResponse.IsSuccessStatusCode)
                {
                    await log.Error($"服务异常:httpcode({httpResponse.StatusCode})");
                }
            }
            catch (Exception ex)
            {
                await log.Error($"服务异常:{ex.Message}", ex);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string controller, string action, IDictionary<string, object> queryString = null)
        {
            var url = $"{controller}/{action}";
            await DeleteAsync(url, queryString);
        }
        #endregion
        #region put
        public async Task PutFromJsonAsyncAsync<TRequest>(string url, TRequest request = default)
        {
            try
            {
                url = GetUrl(url);
                await httpClient.PutFromJsonAsync<TRequest>(url, request);
            }
            catch (Exception ex)
            {
                await log.Error($"服务异常:{ex.Message}", ex);
            }
        }
        public async Task PutFromJsonAsyncAsync<TRequest>(string controller, string action = "", TRequest request = default)
        {
            var url = $"{controller}/{action}";
            await PutFromJsonAsyncAsync<TRequest>(url, request);
        }
        public async Task<TResponse> PutFromJsonAsyncAsync<TRequest, TResponse>(string url, TRequest request = default)
        {
            try
            {
                url = GetUrl(url);
                var result = await httpClient.PutFromJsonAsync<TRequest, ApiResult<TResponse>>(url, request);
                if (result.Successed)
                {
                    return result.Data;
                }
                else
                {
                    await log.Error($"服务异常:{result.Errors}({result.StatusCode})");
                }
                return default(TResponse);
            }
            catch (Exception ex)
            {
                await log.Error($"服务异常:{ex.Message}", ex);
                return default(TResponse);
            }
        }
        public async Task<TResponse> PutFromJsonAsyncAsync<TRequest, TResponse>(string controller, string action = "", TRequest request = default)
        {
            var url = $"{controller}/{action}";
            return await PutFromJsonAsyncAsync<TRequest, TResponse>(url, request);
        }
        #endregion
    }
}
