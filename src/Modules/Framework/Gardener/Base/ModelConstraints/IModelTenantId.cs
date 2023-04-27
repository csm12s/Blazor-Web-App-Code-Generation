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
    }
}
