// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// 租户
    /// </summary>
    public class TenantDto : BaseDto<Guid>, ITenant
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [DisplayName("Name")]
        [Required(ErrorMessage = "不能为空"), MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(256, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Email")]
        public string? Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(32, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Tel")]
        public string? Tel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("Remark")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        public string? Remark { get; set; }
    }
}
