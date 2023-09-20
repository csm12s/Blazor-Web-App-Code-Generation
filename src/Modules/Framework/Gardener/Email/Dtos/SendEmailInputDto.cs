// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Email.Enums;
using Gardener.Email.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Dtos
{
    /// <summary>
    /// 发送邮件输入信息
    /// </summary>
    [Display(Name = nameof(EmailLocalResource.SendEmailInput), ResourceType = typeof(EmailLocalResource))]
    public class SendEmailInputDto
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.TemplateId), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid TemplateId { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Data), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public object Data { get; set; } = null!;
        /// <summary>
        /// 接收方邮箱地址
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.ToEmail), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [RegularExpression("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.EmailAddressValidationError))]
        public string ToEmail { get; set; } = null!;
        /// <summary>
        /// 邮件服务器标签
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.ServerTag), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public EmailServerTag ServerTag { get; set; } = EmailServerTag.Base;
        /// <summary>
        /// 邮件服务器配置编号
        /// </summary>
        /// <remarks>
        /// 优先使用，为空时使用ServerTag
        /// </remarks>
        [Display(Name = nameof(EmailLocalResource.EmailServerConfigId), ResourceType = typeof(EmailLocalResource))]
        public Guid EmailServerConfigId { get; set; }
    }
}
