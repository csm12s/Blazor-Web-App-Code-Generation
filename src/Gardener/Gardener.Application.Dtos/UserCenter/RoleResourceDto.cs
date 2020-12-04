// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
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
        public Guid ResourceId { get; set; }
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
