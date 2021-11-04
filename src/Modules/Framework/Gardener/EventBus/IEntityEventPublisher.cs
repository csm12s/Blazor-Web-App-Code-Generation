// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.EventBus
{
    public interface IEntityEventPublisher
    {
        /// <summary>
        /// 通知删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task NotifyDeleteAsync<TEntity, TKey>(TKey key);
        /// <summary>
        /// 通知批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task NotifyDeletesAsync<TEntity, TKey>(IEnumerable<TKey> keys);
        /// <summary>
        /// 通知逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task NotifyFakeDeleteAsync<TEntity, TKey>(TKey key);
        /// <summary>
        /// 通知批量逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task NotifyFakeDeletesAsync<TEntity, TKey>(IEnumerable<TKey> keys);
        /// <summary>
        /// 通知插入
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task NotifyInsertAsync<TEntity>(TEntity entity);
        /// <summary>
        /// 通知锁定
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task NotifyLockAsync<TEntity>(TEntity entity);
        /// <summary>
        /// 通知更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task NotifyUpdateAsync<TEntity>(TEntity entity);
        /// <summary>
        /// 通知
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="action"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task PublishAsync<TEntity>(string action, object data);
        /// <summary>
        /// 通知
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="action"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync<TEntity>(string action, object data, CancellationToken cancellationToken);
    }
}
