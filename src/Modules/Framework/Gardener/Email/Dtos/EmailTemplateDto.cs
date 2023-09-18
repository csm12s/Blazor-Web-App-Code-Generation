// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Email.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Dtos
{
    /// <summary>
    /// 邮件模板信息
    /// </summary>
    [Display(Name = nameof(EmailLocalResource.EmailTemplate), ResourceType = typeof(EmailLocalResource))]
    public class EmailTemplateDto : BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Name), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(30, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 发件人
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.FromName), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string FromName { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Remark), ResourceType = typeof(EmailLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }
        /// <summary>
        /// 主题模板
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.SubjectTemplate), ResourceType = typeof(EmailLocalResource))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? SubjectTemplate { get; set; }
        /// <summary>
        /// 内容模板
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.ContentTemplate), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ContentTemplate { get; set; } = null!;
        /// <summary>
        /// 例子
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Example), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Example { get; set; }

        /// <summary>
        /// 是否是HTML内容
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.IsHtml), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool IsHtml { get; set; }
    }
}
