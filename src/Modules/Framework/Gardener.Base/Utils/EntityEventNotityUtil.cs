// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Enums;
using Gardener.EventBus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Base
{
    /// <summary>
    /// 实体事件通知静态类
    /// </summary>
    public static class EntityEventNotityUtil
    {
        private static IEventBus? _eventBus = null;
        private static readonly object _senderLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEventBus GetEventBus()
        {
            
            if (_eventBus != null)
            {
                return _eventBus;
            }
            lock (_senderLock)
            {
                if (_eventBus == null)
                {
                    _eventBus = App.GetService<IEventBus>();
                }
            }

            return _eventBus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="operateType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static async Task NotifyAsync<TEntity,TData>(EntityOperateType operateType,TData data)
        {
            EventInfo<TData> eventBase = new EventInfo<TData>(EventType.EntityOperate, data);
            eventBase.EventGroup = typeof(TEntity).Name + operateType.ToString();
            await GetEventBus().Publish(eventBase);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="operateType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static async Task NotifyAsync<TEntity>(EntityOperateType operateType, TEntity data)
        {
            EventInfo<TEntity> eventBase = new EventInfo<TEntity>(EventType.EntityOperate, data);
            eventBase.EventGroup = typeof(TEntity).Name + operateType.ToString();
            await GetEventBus().Publish(eventBase);
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
            await NotifyAsync<TEntity, TKey>(EntityOperateType.Delete, key);
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
            await NotifyAsync<TEntity, IEnumerable<TKey>>(EntityOperateType.Deletes, keys);
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
            await NotifyAsync<TEntity, TKey>(EntityOperateType.FakeDelete, key);
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
            await NotifyAsync<TEntity, IEnumerable<TKey>>(EntityOperateType.FakeDeletes, keys);
        }
        /// <summary>
        /// 通知插入
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyInsertAsync<TEntity>(TEntity entity)
        {
            await NotifyAsync<TEntity>(EntityOperateType.Insert, entity);
        }
        /// <summary>
        /// 通知更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyUpdateAsync<TEntity>(TEntity entity)
        {
            await NotifyAsync<TEntity>(EntityOperateType.Update, entity);
        }
        /// <summary>
        /// 通知锁定
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task NotifyLockAsync<TEntity>(TEntity entity)
        {
            await NotifyAsync<TEntity>(EntityOperateType.Lock, entity);
        }
    }
}
