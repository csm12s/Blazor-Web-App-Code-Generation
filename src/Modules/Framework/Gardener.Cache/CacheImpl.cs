// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Cache
{
    public class CacheImpl : ICache
    {
        private readonly IDistributedCache _cache;

        public CacheImpl(IDistributedCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            return _cache.GetString(key);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            string value = _cache.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="absoluteExpirationSeconds"></param>
        /// <returns></returns>
        public T Get<T>(string key, Func<T> func)
        {
            return Get<T>(key, func, new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTimeOffset.MaxValue));
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        public T Get<T>(string key, Func<T> func, TimeSpan absoluteExpiration)
        {
            return Get<T>(key, func, new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(absoluteExpiration));
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public T Get<T>(string key, Func<T> func, DistributedCacheEntryOptions options)
        {
            T value = Get<T>(key);
            if (value == null)
            {
                T newValue =func();
                if (newValue != null)
                {
                    Set(key, newValue, options);
                }
                return newValue;
            }
            return value;
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            string value = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="absoluteExpirationSeconds"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> func)
        {
            return await GetAsync<T>(key, func, new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTimeOffset.MaxValue));
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan absoluteExpiration)
        {
            return await GetAsync<T>(key, func, new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(absoluteExpiration));
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> func, DistributedCacheEntryOptions options)
        {
            T value = await GetAsync<T>(key);
            if (value == null)
            {
                T newValue =await func();
                if (newValue != null)
                {
                    await SetAsync(key, newValue, options);
                }
                return newValue;
            }
            return value;
        }
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value)
        {
            Set(key, value, new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTimeOffset.MaxValue));
        }
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        public void Set<T>(string key, T value, TimeSpan absoluteExpiration)
        {
            Set(key, value, new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(absoluteExpiration));
        }
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options">绝对过期时间</param>
        public void Set<T>(string key, T value, DistributedCacheEntryOptions options)
        {
            byte[] data = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value);
            _cache.Set(key, data, options);
        }
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task SetAsync<T>(string key, T value)
        {
            await SetAsync(key, value, new DistributedCacheEntryOptions()
               .SetAbsoluteExpiration(DateTimeOffset.MaxValue));
        }
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        public async Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration)
        {
            await SetAsync(key, value, new DistributedCacheEntryOptions()
               .SetAbsoluteExpiration(absoluteExpiration));
        }
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options">绝对过期时间</param>
        public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
        {
            byte[] data = value is string s? Encoding.UTF8.GetBytes(s): System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value);
            await _cache.SetAsync(key, data, options);
        }
        /// <summary>
        /// 根据缓存项的字符串键删除缓存项
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }
        /// <summary>
        /// 根据缓存项的字符串键删除缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
        /// <summary>
        /// 根据项的键刷新缓存中的项，重置其滑动过期超时（如果有）
        /// </summary>
        /// <param name="key"></param>
        public void Refresh(string key)
        {
            _cache.Refresh(key);
        }
        /// <summary>
        /// 根据项的键刷新缓存中的项，重置其滑动过期超时（如果有）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RefreshAsync(string key)
        {
            await _cache.RefreshAsync(key);
        }
    }
}
