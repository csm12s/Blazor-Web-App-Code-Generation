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
        public async Task PostFromJsonAsync<TRequest>(string url, TRequest request = default(TRequest))
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
        public async Task<TResponse> PostFromJsonAsync<TRequest, TResponse>(string url, TRequest request = default(TRequest))
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
        #endregion

        #region get
        public async Task<TResponse> GetFromJsonAsync<TResponse>(string url, IDictionary<string, object> queryString = null)
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
        #endregion
        #region delete
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
        public async Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null)
        {
            try
            {
               var result = await httpClient.DeleteFromJsonAsync<ApiResult<TResponse>>(GetUrl(url, queryString));
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
        #endregion
        #region put
        public async Task PutFromJsonAsync<TRequest>(string url, TRequest request = default)
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
        public async Task<TResponse> PutFromJsonAsync<TRequest, TResponse>(string url, TRequest request = default)
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
        #endregion
    }
}
