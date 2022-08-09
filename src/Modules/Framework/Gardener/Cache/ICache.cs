// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Cache
{
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        T Get<T>(string key, Func<T> func);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        T Get<T>(string key, Func<T> func, TimeSpan absoluteExpiration);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> func);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan absoluteExpiration);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetStringAsync(string key);
        /// <summary>
        /// 根据项的键刷新缓存中的项，重置其滑动过期超时（如果有）
        /// </summary>
        /// <param name="key"></param>
        void Refresh(string key);
        /// <summary>
        /// 异步 根据项的键刷新缓存中的项，重置其滑动过期超时（如果有）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RefreshAsync(string key);
        /// <summary>
        /// 根据缓存项的字符串键删除缓存项
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        /// 异步 根据缓存项的字符串键删除缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set<T>(string key, T value);
        /// <summary>
        /// 添加到缓存中，指定绝对过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        void Set<T>(string key, T value, TimeSpan absoluteExpiration);
        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value);
        /// <summary>
        /// 添加到缓存中，指定绝对过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration);
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key);
    }
}
