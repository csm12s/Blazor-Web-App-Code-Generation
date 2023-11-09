// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.ComponentModel.DataAnnotations;
using Gardener.Attributes;
using Gardener.SystemManager.Resources;
using Gardener.Base.Resources;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 字典信息
    /// </summary>
    [Display(Name = nameof(SystemManagerResource.Code), ResourceType = typeof(SystemManagerResource))]
    public class CodeDto : BaseDto<int>
    {
        /// <summary>
        /// 字段类型编号
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CodeTypeId), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int CodeTypeId { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CodeValue), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeValue { get; set; } = null!;
        /// <summary>
        /// 字典名称
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CodeName), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string CodeName { get; set; } = null!;
        /// <summary>
        /// 本地化字典名称
        /// </summary>
        [DisabledSearchField]
        [Display(Name = nameof(SystemManagerResource.LocalCodeName), ResourceType = typeof(SystemManagerResource))]
        [NotMapped]
        public string LocalCodeName { get; set; } = null!;
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Order), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int Order { get; set; }
        /// <summary>
        /// 扩展参数
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.ExtendParams), ResourceType = typeof(SystemManagerResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ExtendParams { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Color), ResourceType = typeof(SystemManagerResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Color { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CodeType), ResourceType = typeof(SystemManagerResource))]
        [NotMapped]
        public CodeTypeDto CodeType { get; set; } = null!;
    }
}
