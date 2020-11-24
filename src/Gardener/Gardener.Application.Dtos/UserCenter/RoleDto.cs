using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Dtos
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
    }
}