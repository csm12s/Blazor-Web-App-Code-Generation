// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Enums;
using Gardener.UserCenter.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 用户数据转换实体
    /// </summary>
    [Display(Name = nameof(UserCenterResource.User), ResourceType = typeof(UserCenterResource))]
    public class UserDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = nameof(UserCenterResource.UserName), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        [Order(1)]
        public string UserName { get; set; } = null!;
        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = nameof(UserCenterResource.NickName), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        [Order(2)]
        public string? NickName { get; set; }
        /// <summary>
        /// 密码加密后的
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Password), ResourceType = typeof(UserCenterResource))]
        [MaxLength(32, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        public string? Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Avatar), ResourceType = typeof(UserCenterResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Email), ResourceType = typeof(UserCenterResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Email { get; set; }
        /// <summary>
        /// 邮箱是否确认
        /// </summary>
        [Display(Name = nameof(UserCenterResource.EmailConfirmed), ResourceType = typeof(UserCenterResource))]
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = nameof(UserCenterResource.PhoneNumber), ResourceType = typeof(UserCenterResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// 手机是否确认
        /// </summary>
        [Display(Name = nameof(UserCenterResource.PhoneNumberConfirmed), ResourceType = typeof(UserCenterResource))]
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Gender), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [Order(3)]
        public Gender Gender { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Roles), ResourceType = typeof(UserCenterResource))]
        public ICollection<RoleDto>? Roles { get; set; }
        /// <summary>
        /// 用户扩展信息
        /// </summary>
        [Display(Name = nameof(UserCenterResource.UserExtension), ResourceType = typeof(UserCenterResource))]
        public UserExtensionDto? UserExtension { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [Display(Name = nameof(UserCenterResource.DeptId), ResourceType = typeof(UserCenterResource))]
        public int? DeptId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Dept), ResourceType = typeof(UserCenterResource))]
        public DeptDto? Dept { get; set; }
        /// <summary>
        /// 岗位编号
        /// </summary>
        [Display(Name = nameof(UserCenterResource.PositionId), ResourceType = typeof(UserCenterResource))]
        public int? PositionId { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Position), ResourceType = typeof(UserCenterResource))]
        public PositionDto? Position;
        /// <summary>
        /// 从名字获取头像
        /// </summary>
        /// <returns></returns>
        public string GetAvatarFromName()
        {
            return (this.NickName ?? this.UserName).Substring(0,1);
        }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <remarks>
        /// 判断规则 <see cref="Gardener.Authorization.Core.IAuthorizationService.IsSuperAdministrator"/>
        /// </remarks>
        [DisabledSearchField]
        [Display(Name = nameof(UserCenterResource.IsSuperAdministrator), ResourceType = typeof(UserCenterResource))]
        public bool? IsSuperAdministrator { get;set; }
    }
}
