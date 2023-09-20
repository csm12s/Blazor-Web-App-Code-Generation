// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Resources;
using Gardener.Authentication.Enums;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Audit.Dtos
{
    /// <summary>
    /// 实体审计信息
    /// </summary>
    [Display(Name = nameof(AuditLocalResource.AuditEntity), ResourceType = typeof(AuditLocalResource))]
    public class AuditEntityDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 数据编号
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.DataId), ResourceType = typeof(AuditLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string DataId { get; set; } = null!;
        /// <summary>
        /// 实体名称
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Name), ResourceType = typeof(AuditLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]

        public string Name { get; set; } = null!;
        /// <summary>
        /// 实体类型名称
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.TypeName), ResourceType = typeof(AuditLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]

        public string TypeName { get; set; } = null!;
        /// <summary>
        /// 操作类型
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperationType), ResourceType = typeof(AuditLocalResource))]

        public EntityOperateType OperationType { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperaterId), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]

        public string? OperaterId { get; set; }
        /// <summary>
        /// 操作者名称
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperaterName), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]

        public string? OperaterName { get; set; }
        /// <summary>
        /// 操作者类型
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperaterType), ResourceType = typeof(AuditLocalResource))]

        public IdentityType OperaterType { get; set; }
        /// <summary>
        /// 操作ID
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.OperationId), ResourceType = typeof(AuditLocalResource))]

        public Guid OperationId { get; set; }
        /// <summary>
        /// 获取或设置 操作实体属性集合
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.AuditProperties), ResourceType = typeof(AuditLocalResource))]
        public ICollection<AuditPropertyDto>? AuditProperties { get; set; }
    }
}
