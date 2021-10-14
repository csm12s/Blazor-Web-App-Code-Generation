// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authorization.Dtos;
using Gardener.Cache;
using Gardener.Enums;
using Gardener.UserCenter.Impl.Domains;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 接口查询服务
    /// </summary>
    public class ApiEndpointQueryService : IApiEndpointQueryService
    {
        private readonly string cacheKeyPre = $"{nameof(ApiEndpoint)}:";
        private readonly ICache cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        public ApiEndpointQueryService(ICache cache)
        {
            this.cache = cache;
        }
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetApiEndpointCacheKey(string key)
        { 
            return cacheKeyPre + key; 
        }
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string GetApiEndpointCacheKey(string path, HttpMethod method)
        { 
            return cacheKeyPre + path + "_" + method;
        }
        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint> Query(string key)
        {
            key = GetApiEndpointCacheKey(key);

            Func<Task<ApiEndpoint>> func = async () => {

                Function function = await Db.GetRepository<Function>().AsQueryable(false).Where(x => x.Key.Equals(key) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
                return function?.Adapt<ApiEndpoint>();
            };
            return await cache.GetAsync<ApiEndpoint>(key, func);
        }

        /// <summary>
        /// 根据path,method获取功能点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint> Query(string path, HttpMethod method)
        {
            string key = GetApiEndpointCacheKey(path,method);
            Func<Task<ApiEndpoint>> func = async () => {
                Function function = await Db.GetRepository<Function>().AsQueryable(false).Where(x => x.Method.Equals(method) && x.Path.Equals(path) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
                return function?.Adapt<ApiEndpoint>();
            };
            return await cache.GetAsync<ApiEndpoint>(key, func);
        }
    }
}
