// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using System;
using System.ComponentModel;

namespace Gardener.Base
{
    public abstract class TenantBaseDto<TKey, TKey_TenantId> : BaseDto<TKey>
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        public virtual TKey_TenantId TenantId { get; set; }
    }
    /// <summary>
    /// dto基础类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseDto<TKey>: BaseDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("Id")]
        public TKey Id { get; set; }
       
    }

    /// <summary>
    /// dto基础类
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("IsLocked")]
        public bool IsLocked { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        [DisplayName("IsDeleted")]
        [DisabledSearchField]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("CreatedTime")]
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("UpdatedTime")]
        public DateTimeOffset? UpdatedTime { get; set; }
    }
}
