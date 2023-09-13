// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.UserCenter.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    [Display(Name = nameof(UserCenterResource.Client), ResourceType = typeof(UserCenterResource))]
    public class ClientDto:BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Name), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(30, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Remark), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Remark { get; set; } = null!;

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Contacts), ResourceType = typeof(UserCenterResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Tel), ResourceType = typeof(UserCenterResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Tel { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        [Display(Name = nameof(UserCenterResource.SecretKey), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(64, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? SecretKey { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Email), ResourceType = typeof(UserCenterResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Email { get; set; }
    }
}
