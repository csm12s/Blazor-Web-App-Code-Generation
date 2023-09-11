// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessageResourceType =typeof(ValidateErrorMessagesResource),ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [MinLength(1, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMinValidationError))]
        [Display(Name= nameof(SharedLocalResource.Name), ResourceType=typeof(SharedLocalResource))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 角色描述
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        [Display(Name = nameof(SharedLocalResource.Remark), ResourceType = typeof(SharedLocalResource))]
        public string Remark { get; set; }=null!;

        /// <summary>
        /// 是否是超级管理员
        /// 超级管理员拥有所有权限
        /// </summary>
        [Display(Name = nameof(UserCenterResource.IsSuperAdministrator), ResourceType = typeof(UserCenterResource))]
        public bool IsSuperAdministrator { get; set; }

        /// <summary>
        /// 是否是默认角色
        /// </summary>
        /// <remarks>
        /// 添加用户时默认添加该角色
        /// </remarks>
        [Display(Name = nameof(UserCenterResource.IsDefault), ResourceType = typeof(UserCenterResource))]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<ResourceDto>? Resources { get; set; }
    }
}