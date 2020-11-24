// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Dtos
{
    /// <summary>
    /// 角色资源关系
    /// </summary>
    public class RoleResourceDto
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public RoleDto Role { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        public int ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public ResourceDto Resource { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
    }
}
