// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base;
using Gardener.Base.Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Authentication.Dtos
{
    /// <summary>
    /// 用户Token信息
    /// </summary>
    [Display(Name = nameof(SharedLocalResource.LoginToken), ResourceType = typeof(SharedLocalResource))]
    public class LoginTokenDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 身份编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IdentityId), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string IdentityId { get; set; } = null!;

        /// <summary>
        /// 身份唯一名称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IdentityName), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string IdentityName { get; set; } = null!;

        /// <summary>
        /// 身份昵称
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IdentityNickName), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? IdentityNickName { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IdentityType), ResourceType = typeof(SharedLocalResource))]
        public IdentityType IdentityType { get; set; }

        /// <summary>
        /// 获取或设置 登录Id
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.LoginId), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string LoginId { get; set; } = null!;

        /// <summary>
        /// 登录的客户端类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.LoginClientType), ResourceType = typeof(SharedLocalResource))]
        public LoginClientType LoginClientType { get; set; }

        /// <summary>
        /// 获取或设置 标识值
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Value), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(2000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Value { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.EndTime), ResourceType = typeof(SharedLocalResource))]
        public DateTimeOffset EndTime { get; set; } = default!;

        /// <summary>
        /// 访问IP
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Ip), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Ip { get; set; }

        /// <summary>
        /// 已退出登录
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.LoggedOut), ResourceType = typeof(SharedLocalResource))]
        public bool LoggedOut { get; set; }
    }
}
