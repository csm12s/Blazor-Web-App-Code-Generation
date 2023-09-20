// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// 刷新token输入
    /// </summary>
    [Display(Name = nameof(SharedLocalResource.RefreshToken), ResourceType = typeof(SharedLocalResource))]
    public class RefreshTokenInput
    {
        /// <summary>
        /// 刷新token
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.RefreshToken), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(2000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string RefreshToken { get; set; } = null!;

    }
}
