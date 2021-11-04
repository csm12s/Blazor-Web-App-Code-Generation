// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.EventBus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Base
{
    /// <summary>
    /// 实体事件通知
    /// </summary>
    public static class EntityEventNotityUtil
    {
        private static IEntityEventPublisher _publisher = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEntityEventPublisher GetPublisher()
        {
            if (_publisher != null)
            {
                return _publisher;
            }
            _publisher = App.GetService<IEntityEventPublisher>();

            return _publisher;
        }
        /// <summary>
        /// 通知删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task NotifyDeleteAsync<TEntity, TKey>(TKey key)
        {
            await GetPublisher().NotifyDeleteAsync<TEntity, TKey>(key);
        }
        /// <summary>
        /// 通知批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static async Task NotifyDeletesAsync<TEntity, TKey>(IEnumerable<TKey> keys)
        {
            await GetPublisher().NotifyDeletesAsync<TEntity, TKey>(keys);
        }
        /// <summary>
        /// 通知逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task NotifyFakeDeleteAsync<TEntity, TKey>(TKey key)
        {
            await GetPublisher().NotifyFakeDeleteAsync<TEntity, TKey>(key);
        }
        /// <summary>
        /// 通知批量逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static async Task NotifyFakeDeletesAsync<TEntity, TKey>(IEnumerable<TKey> keys)
        {
            await GetPublisher().NotifyFakeDeletesAsync<TEntity, TKey>(keys);
        }
        /// <summary>
        /// 通知插入
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyInsertAsync<TEntity>(TEntity entity)
        {
            await GetPublisher().NotifyInsertAsync<TEntity>(entity);
        }
        /// <summary>
        /// 通知更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyUpdateAsync<TEntity>(TEntity entity)
        {
            await GetPublisher().NotifyUpdateAsync<TEntity>(entity);
        }
        /// <summary>
        /// 通知锁定
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyLockAsync<TEntity>(TEntity entity)
        {
            await GetPublisher().NotifyLockAsync<TEntity>(entity);
        }
    }
}
