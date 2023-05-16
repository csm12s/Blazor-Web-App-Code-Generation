// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Gardener.Base.Dto
{
    /// <summary>
    /// 租户资源
    /// </summary>
    public class SystemTenantResourceDto : TenantBaseDtoNoKey
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Required]
        [DisplayName("租户编号")]
        public new Guid TenantId { get; set; }

        /// <summary>
        /// 资源编号
        /// </summary>
        [Required]
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }
    }
}
