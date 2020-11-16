// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class UserRoleDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserDto User { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public RoleDto Role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
    }
}
