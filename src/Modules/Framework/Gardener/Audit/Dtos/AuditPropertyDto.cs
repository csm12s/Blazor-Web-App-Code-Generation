// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Resources;
using Gardener.Base;
using Gardener.Base.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Audit.Dtos
{
    /// <summary>
    /// 属性审计信息
    /// </summary>
    [Display(Name = nameof(AuditLocalResource.AuditProperty), ResourceType = typeof(AuditLocalResource))]
    public class AuditPropertyDto : TenantBaseDto<Guid>
    {

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.DisplayName), ResourceType = typeof(AuditLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// 字段名称
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.FieldName), ResourceType = typeof(AuditLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string FieldName { get; set; } = null!;

        /// <summary>
        /// 旧值
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OriginalValue), ResourceType = typeof(AuditLocalResource))]
        public string? OriginalValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.NewValue), ResourceType = typeof(AuditLocalResource))]
        public string? NewValue { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.DataType), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DataType { get; set; } = null!;

        /// <summary>
        /// 实体审计编号  
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.AuditEntityId), ResourceType = typeof(AuditLocalResource))]
        public Guid AuditEntityId { get; set; }
    }
}
