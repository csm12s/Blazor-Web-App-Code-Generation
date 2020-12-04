// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage ="不能为空")]
        [MaxLength(100,ErrorMessage = "最大长度不能大于{0}")]
        public string Name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{0}")]
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// 是否是超级管理员
        /// 超级管理员拥有所有权限
        /// </summary>
        public bool IsSuperAdministrator { get; set; }
        /// <summary>
        /// 是否是默认权限
        /// 注册用户时默认设置
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<ResourceDto> Resources { get; set; }
    }
}