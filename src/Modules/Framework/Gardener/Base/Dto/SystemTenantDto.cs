// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using Gardener.Base.Resources;

namespace Gardener.Base
{
    /// <summary>
    /// 租户
    /// </summary>
    [Display(Name = nameof(SharedLocalResource.SystemTenant), ResourceType = typeof(SharedLocalResource))]
    public class SystemTenantDto : BaseDto<Guid>, ITenant
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Name), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Email), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(256, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Tel), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Tel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Remark), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }
    }
}
