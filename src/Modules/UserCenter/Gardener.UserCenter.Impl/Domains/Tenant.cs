// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 租户
    /// </summary>
    [Description("租户")]
    public class Tenant : GardenerEntityBaseNoKey
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("租户编号")]
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        [DisplayName("租户名称")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [EmailAddress, MaxLength(256)]
        [DisplayName("电子邮箱")]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Phone, MaxLength(32)]
        [DisplayName("手机号码")]
        public string? PhoneNumber { get; set; }

    }
}
