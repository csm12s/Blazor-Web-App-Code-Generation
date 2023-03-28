// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Common;
using Gardener.Enums;
using Gardener.EventBus;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// api 调用器
    /// </summary>
    [ScopedService]
    public class ApiCaller : IApiCaller
    {
        private readonly HttpClient httpClient;
        private readonly IClientLogger log;
        private readonly IEventBus eventBus;
        private readonly IClientLocalizer localizer;
        /// <summary>
        /// api 调用器
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="log"></param>
        /// <param name="eventBus"></param>
        /// <param name="localizer"></param>
        public ApiCaller(HttpClient httpClient, IClientLogger log, IEventBus eventBus, IClientLocalizer localizer)
        {
            this.httpClient = httpClient;
            this.log = log;
            this.eventBus = eventBus;
            this.localizer = localizer;
        }
        /// <summary>
        /// 调用包装
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="func"></param>
        /// <param name="retry">重试次数</param>
        /// <returns></returns>
        async Task<TResponse> ResponseHandle<TResponse>(Func<Task<HttpResponseMessage>> func,int retry=0)
        {
            try
            {
                HttpResponseMessage httpResponse = await func.Invoke();
                if (HttpStatusCode.OK.Equals(httpResponse.StatusCode))
                {
                    var result = await httpResponse.Content.ReadFromJsonAsync<ApiResult<TResponse>>();
                    if (result==null || !result.Succeeded)
                    {
                        #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                        log.Error(result?.Errors?.ToString()??string.Empty, result?.StatusCode);
                        #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                              //时间戳过期
                        if (result!=null && result.StatusCode == 500 && ExceptionCode.REFRESHTOKEN_NO_EXIST_OR_EXPIRE.ToString().Equals(result.ErrorCode?.ToString()))
                        {
                            await eventBus.Publish(new RefreshTokenErrorEvent());
                        }

                        return default(TResponse);
                    }
                    return result.Data;
                }
                //身份验证失败
                if (httpResponse.StatusCode.Equals(HttpStatusCode.Unauthorized) || httpResponse.StatusCode.Equals(HttpStatusCode.Forbidden))
                {
                    await eventBus.Publish(new UnauthorizedApiCallEvent() { HttpStatusCode = httpResponse.StatusCode });
                }
                if (httpResponse.StatusCode.Equals(HttpStatusCode.Unauthorized) && retry==0)
                {
                  return await ResponseHandle<TResponse>(func, ++retry);
                }
                //请求失败 
                #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                log.Error(localizer[SharedLocalResource.ResuqesFail], (int)httpResponse.StatusCode);
                #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                return default(TResponse);
            }
            catch (Exception ex)
            {
                #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                log.Error($"{localizer[SharedLocalResource.ResuqesException]}[{ex.Message}]", -999, ex);
                #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                return default(TResponse);
            }
        }
        /// <summary>
        /// 调用包装
        /// </summary>
        /// <param name="func"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        async Task ResponseHandle(Func<Task<HttpResponseMessage>> func,int retry=0)
        {
            try
            {
                HttpResponseMessage httpResponse = await func.Invoke();
                if (!HttpStatusCode.OK.Equals(httpResponse.StatusCode))
                {
                    //身份验证失败
                    if (httpResponse.StatusCode.Equals(HttpStatusCode.Unauthorized) || httpResponse.StatusCode.Equals(HttpStatusCode.Forbidden))
                    {
                        await eventBus.Publish(new UnauthorizedApiCallEvent() { HttpStatusCode=httpResponse.StatusCode});
                    }
                    if (httpResponse.StatusCode.Equals(HttpStatusCode.Unauthorized) && retry == 0)
                    {
                        await ResponseHandle(func, ++retry);
                        return;
                    }
                    //请求失败 
                    #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    log.Error(localizer[SharedLocalResource.ResuqesFail], (int)httpResponse.StatusCode);
                    #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                }
                else 
                {
                    var result = await httpResponse.Content.ReadFromJsonAsync<ApiResult<Object>>();
                    if (result==null ||!result.Succeeded)
                    {
                        #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                        log.Error(result?.Errors?.ToString()??string.Empty, result?.StatusCode);
                        #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                        //时间戳过期
                        if (result!=null && result.StatusCode == 500 && ExceptionCode.REFRESHTOKEN_NO_EXIST_OR_EXPIRE.ToString().Equals(result.ErrorCode?.ToString()))
                        {
                            await eventBus.Publish(new RefreshTokenErrorEvent());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                log.Error($"{localizer[SharedLocalResource.ResuqesException]}[{ex.Message}]", -999, ex);
                #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            }
        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private string GetUrl(string url, IDictionary<string, object?>? queryString = null)
        {
            if (queryString != null && queryString.Count > 0)
            {
                url = QueryHelpers.AddQueryString(url, queryString.ToDictionary(p => p.Key, p => p.Value==null?"": p.Value.ToString()));
            }
            return url;
        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private string GetUrl(string url, List<KeyValuePair<string, object?>> queryString)
        {
            if (queryString != null && queryString.Count() > 0)
            {
                foreach (KeyValuePair<string, object?> item in queryString)
                {
                    url = QueryHelpers.AddQueryString(url, item.Key, item.Value == null ? "" : item.Value.ToString());
                }
            }
            return url;
        }
        #region post
        /// <summary>
        /// post
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task PostAsync<TRequest>(string url, TRequest? request = default(TRequest))
        {
            return ResponseHandle(() =>
            {
                if (request == null)
                {
                    //todo:request 等于null时 给空 是否会有问题
                    return httpClient.PostAsJsonAsync(url,string.Empty);
                }
                return httpClient.PostAsJsonAsync(url, request);
            });
        }
        /// <summary>
        /// post
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest? request = default(TRequest))
        {
            return ResponseHandle<TResponse>(() =>
           {
               return httpClient.PostAsJsonAsync(url, request);
           });

        }
        /// <summary>
        /// post
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> PostWithoutBodyAsync<TResponse>(string url, IDictionary<string, object>? queryString = null)
        {
            return ResponseHandle<TResponse>(() =>
            {
                return httpClient.PostAsJsonAsync(url, queryString);
            });
        }
        #endregion
        #region get
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<TResponse> GetAsync<TResponse>(string url)
        {
            return ResponseHandle<TResponse>(() =>
            {
                return httpClient.GetAsync(url); ;
            });

        }
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, object?> queryString)
        {
            return ResponseHandle<TResponse>(() =>
             {
                 url = GetUrl(url, queryString);
                 return httpClient.GetAsync(url); ;
             });

        }
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> GetAsync<TResponse>(string url, List<KeyValuePair<string, object?>> queryString)
        {
            return ResponseHandle<TResponse>(() =>
            {
                url = GetUrl(url, queryString);
                return httpClient.GetAsync(url); ;
            });

        }
        #endregion
        #region delete
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task DeleteAsync(string url, IDictionary<string, object?>? queryString = null)
        {
           return ResponseHandle(() =>
            {
                return httpClient.DeleteAsync(GetUrl(url, queryString));
            });
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object?>? queryString = null)
        {
            return ResponseHandle<TResponse>(() =>
            {
                return httpClient.DeleteAsync(GetUrl(url, queryString));
            });
        }
        #endregion
        #region put
        /// <summary>
        /// put
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task PutAsync<TRequest>(string url, TRequest? request = default)
        {
            return ResponseHandle(() =>
            {
                return httpClient.PutAsJsonAsync(url, request);
            });
        }
        /// <summary>
        /// put
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest? request = default)
        {
            return ResponseHandle<TResponse>(() =>
           {
               return httpClient.PutAsJsonAsync(url, request);
           });
        }

        #endregion
    }
}
