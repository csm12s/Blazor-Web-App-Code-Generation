// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// 租户编号
    /// </summary>
    public interface IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <remarks>
        /// <para>默认：租户编号不为null或空的是租户</para>
        /// <para>租户在查询时限制租户编号</para>
        /// <para>租户在新增更新时处理租户编号</para>
        /// <para>
        /// 处理详情在
        /// <code>Gardener.EntityFramwork.DbContexts.GardenerMultiTenantDbContext</code>
        /// </para>
        /// </remarks>
        /// <returns></returns>
        public bool IsTenant => TenantId != null && !TenantId.Equals(Guid.Empty);
}
    }
