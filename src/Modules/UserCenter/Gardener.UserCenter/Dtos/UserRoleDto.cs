// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.UserCenter.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    [Display(Name = nameof(UserCenterResource.UserRole), ResourceType = typeof(UserCenterResource))]
    public class UserRoleDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Display(Name = nameof(UserCenterResource.UserId), ResourceType = typeof(UserCenterResource))]
        public int UserId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        [Display(Name = nameof(UserCenterResource.User), ResourceType = typeof(UserCenterResource))]
        public UserDto? User { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = nameof(UserCenterResource.RoleId), ResourceType = typeof(UserCenterResource))]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Role), ResourceType = typeof(UserCenterResource))]
        public RoleDto? Role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = nameof(UserCenterResource.CreatedTime), ResourceType = typeof(UserCenterResource))]
        public DateTimeOffset CreatedTime { get; set; }
    }
}
