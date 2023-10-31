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
    /// 操作审计信息
    /// </summary>
    [Display(Name = nameof(AuditLocalResource.AuditOperation), ResourceType = typeof(AuditLocalResource))]
    public class AuditOperationDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 资源名
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.ResourceName), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ResourceName { get; set; } = null!;
        /// <summary>
        /// 资源编号
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.ResourceId), ResourceType = typeof(AuditLocalResource))]
        public Guid? ResourceId { get; set; }
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
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public IdentityType OperaterType { get; set; }
        /// <summary>
        /// 访问IP
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Ip), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Ip { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.UserAgent), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? UserAgent { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Path), ResourceType = typeof(AuditLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Path { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Method), ResourceType = typeof(AuditLocalResource))]
        public HttpMethod Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.Parameters), ResourceType = typeof(AuditLocalResource))]
        public string? Parameters { get; set; }
        /// <summary>
        /// 关联数据审计
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.AuditEntities), ResourceType = typeof(AuditLocalResource))]
        public ICollection<AuditEntityDto>? AuditEntities { get; set; }
    }
}
