// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Email.Enums;
using Gardener.Email.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Dtos
{
    /// <summary>
    /// 邮件服务器配置信息
    /// </summary>
    [Display(Name = nameof(EmailLocalResource.EmailServerConfig), ResourceType = typeof(EmailLocalResource))]
    public class EmailServerConfigDto :BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Name), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(30, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Remark), ResourceType = typeof(EmailLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Host), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Host { get; set; } = null!;
        /// <summary>
        /// 端口
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.Port), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int Port { get; set; }
        /// <summary>
        /// 发件人邮箱
        /// </summary>
        [RegularExpression("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.EmailAddressValidationError))]
        [Display(Name = nameof(EmailLocalResource.FromEmail), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string FromEmail { get; set; } = null!;
        /// <summary>
        /// 账户名
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.AccountName), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string AccountName { get; set; } = null!;
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.AccountPassword), ResourceType = typeof(EmailLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string AccountPassword { get; set; } = null!;
        /// <summary>
        /// 标签
        /// </summary>
        /// <remarks>多个标签，逗号隔开</remarks>
        [Display(Name = nameof(EmailLocalResource.Tags), ResourceType = typeof(EmailLocalResource))]
        public string? Tags { get; set; }


        /// <summary>
        /// 是否启用SSL
        /// </summary>
        [Display(Name = nameof(EmailLocalResource.EnableSsl), ResourceType = typeof(EmailLocalResource))]
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 获取标签集合
        /// </summary>
        /// <returns></returns>
        public List<EmailServerTag> GetEmailServerTagEnums() 
        {
            List<EmailServerTag> tags=new List<EmailServerTag>();
            if (!string.IsNullOrEmpty(this.Tags))
            {
                foreach (string tag in this.Tags.Split(","))
                {
                    if (!string.IsNullOrEmpty(tag))
                    {
                        tags.Add(Enum.Parse<EmailServerTag>(tag));
                    }
                }
            }
            return tags;
        }
    }
}
