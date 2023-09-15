// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Gardener.Base;
using Gardener.SystemManager.Resources;
using Gardener.Base.Resources;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Display(Name = nameof(SystemManagerResource.CodeType), ResourceType = typeof(SystemManagerResource))]
    public class CodeTypeDto : BaseDto<int>
    {
        /// <summary>
        /// 字典类型名称
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CodeTypeName), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeTypeName { get; set; } = null!;
        /// <summary>
        /// 字典类型值
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CodeTypeValue), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeTypeValue { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Remark), ResourceType = typeof(SystemManagerResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }
    }
}
