// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using Gardener.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class ApiCaller : IApiCaller
    {
        private readonly HttpClient httpClient;
        private readonly IClientLogger log;
        public ApiCaller(HttpClient httpClient, IClientLogger log)
        {
            this.httpClient = httpClient;
            this.log = log;
        }
        async Task<TResponse> ResponseHandle<TResponse>(Func<Task<HttpResponseMessage>> func)
        {
            try
            {
                HttpResponseMessage httpResponse = await func.Invoke();
                if (HttpStatusCode.OK.Equals(httpResponse.StatusCode))
                {
                    var result = await httpResponse.Content.ReadFromJsonAsync<ApiResult<TResponse>>();
                    if (!result.Succeeded)
                    {
                        log.Error(result.Errors?.ToString(), result.StatusCode);
                        return default(TResponse);
                    }
                    return result.Data;
                }
                //请求失败 
                log.Error("请求失败", (int)httpResponse.StatusCode);
                return default(TResponse);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, -999, ex);
                return default(TResponse);
            }
        }
        async Task ResponseHandle(Func<Task<HttpResponseMessage>> func)
        {
            try
            {
                HttpResponseMessage httpResponse = await func.Invoke();
                if (!HttpStatusCode.OK.Equals(httpResponse.StatusCode))
                {
                    //请求失败
                    log.Error("请求失败", (int)httpResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, -999, ex);
            }
        }
        private string GetUrl(string url, IDictionary<string, object> queryString = null)
        {
            if (queryString != null && queryString.Count > 0)
            {
                url = QueryHelpers.AddQueryString(url, queryString.ToDictionary(p => p.Key, p => p.Value==null?"": p.Value.ToString()));
            }
            return url;
        }
        #region post
        public async Task PostAsync<TRequest>(string url, TRequest request = default(TRequest))
        {
            await ResponseHandle(() =>
            {
                return httpClient.PostAsJsonAsync<TRequest>(url, request);
            });
        }
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request = default(TRequest))
        {
            return await ResponseHandle<TResponse>(() =>
           {
               return httpClient.PostAsJsonAsync(url, request);
           });

        }
        #endregion
        #region get
        public async Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, object> queryString = null)
        {
            return await ResponseHandle<TResponse>(() =>
             {
                 url = GetUrl(url, queryString);
                 return httpClient.GetAsync(url); ;
             });

        }
        #endregion
        #region delete
        public async Task DeleteAsync(string url, IDictionary<string, object> queryString = null)
        {
            await ResponseHandle(() =>
            {
                return httpClient.DeleteAsync(GetUrl(url, queryString));
            });
        }
        public async Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null)
        {
            return await ResponseHandle<TResponse>(() =>
            {
                return httpClient.DeleteAsync(GetUrl(url, queryString));
            });
        }
        #endregion
        #region put
        public async Task PutAsync<TRequest>(string url, TRequest request = default)
        {
            await ResponseHandle(() =>
            {
                return httpClient.PutAsJsonAsync(url, request);
            });
        }
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request = default)
        {
            return await ResponseHandle<TResponse>(() =>
           {
               return httpClient.PutAsJsonAsync(url, request);
           });
        }


        #endregion
    }
}
