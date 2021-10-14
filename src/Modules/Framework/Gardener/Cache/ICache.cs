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
        T Get<T>(string key);
        T Get<T>(string key, Func<T> func);
        T Get<T>(string key, Func<T> func, TimeSpan absoluteExpiration);
        Task<T> GetAsync<T>(string key);
        Task<T> GetAsync<T>(string key, Func<Task<T>> func);
        Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan absoluteExpiration);
        string GetString(string key);
        Task<string> GetStringAsync(string key);
        void Refresh(string key);
        Task RefreshAsync(string key);
        void Remove(string key);
        Task RemoveAsync(string key);
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, TimeSpan absoluteExpiration);
        Task SetAsync<T>(string key, T value);
        Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration);
    }
}
