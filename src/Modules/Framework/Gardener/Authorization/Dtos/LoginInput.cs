// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base.Resources;
using Gardener.Enums;
using Gardener.VerifyCode.Dtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
    [Display(Name = nameof(SharedLocalResource.LoginInput), ResourceType = typeof(SharedLocalResource))]
    public class LoginInput: ImageVerifyCodeCheckInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.UserName), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Password), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string Password { get; set; } = null!;

        /// <summary>
        /// 登录类型
        /// </summary>
        /// 
        [Display(Name = nameof(SharedLocalResource.LoginType), ResourceType = typeof(SharedLocalResource))]
        public LoginType LoginType { get; set; } = LoginType.AccountPassword;
        /// <summary>
        /// 登录客户端类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.LoginClientType), ResourceType = typeof(SharedLocalResource))]
        public LoginClientType LoginClientType { get; set; } = LoginClientType.Browser;

    }
}