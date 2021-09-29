// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;

namespace Gardener.EventBus
{
    /// <summary>
    /// 实体变化事件
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EntityChangeEvent<TEntity,TKey>: EventBaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="data"></param>
        public EntityChangeEvent(EntityOperationType operation, TEntity data) :base()
        {
            Operation = operation;
            Data = data;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        public EntityChangeEvent(EntityOperationType operation) : base()
        {
            Operation = operation;
        }
        /// <summary>
        /// 
        /// </summary>
        public EntityChangeEvent() : base()
        {
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public EntityOperationType Operation { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public TEntity Data { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public TKey key { get; set; }
        /// <summary>
        /// 是否是逻辑删除
        /// </summary>
        public bool IsFakeDelete { get; set; } = false;
        /// <summary>
        /// 获取Key
        /// </summary>
        /// <param name="keySelecter"></param>
        /// <returns></returns>
        public TKey GetKey(Func<TEntity,TKey> keySelecter)
        {
            bool keyIsNull = false;
            if (key is Guid)
            {
                if (Guid.Empty.Equals(key))
                {
                    keyIsNull = true;
                }
            }
            else if (key == null)
            {
                keyIsNull = true;
            }

            if (!keyIsNull) 
            {

                return key;
            }

            return Data == null ? default(TKey) : keySelecter(Data);
        }
    }
}
