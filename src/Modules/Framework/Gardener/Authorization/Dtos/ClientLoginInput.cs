// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// 客户端登录输入
    /// </summary>
    [Display(Name = nameof(SharedLocalResource.ClientLoginInput), ResourceType = typeof(SharedLocalResource))]
    public class ClientLoginInput
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.ClientId), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid ClientId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Timespan), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public long Timespan { get; set; }

        /// <summary>
        /// 加密的值
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.EncryptionValue), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(64, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string EncryptionValue { get; set; } = null!;
    }
}
