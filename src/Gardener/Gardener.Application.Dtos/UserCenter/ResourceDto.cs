// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 资源
    /// </summary>
    public class ResourceDto
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 权限唯一名（每一个接口）
        /// </summary>
        [Required(ErrorMessage ="不能为空"), MaxLength(64,ErrorMessage = "最大长度不能大于{0}")]
        public string ResourceId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{0}")]
        public string Name { get; set; }
        /// <summary>
        /// 权限key
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{0}")]
        public string Key { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "最大长度不能大于{0}")]
        public string Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        [MaxLength(200, ErrorMessage = "最大长度不能大于{0}")]
        public string Path { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50, ErrorMessage = "最大长度不能大于{0}")]
        public string Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DefaultValue(0)]
        public int Order { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        //public ResourceDto Parent { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<ResourceDto> Children { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        [Required, DefaultValue(ResourceType.API)]
        public ResourceType Type { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<RoleDto> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleResourceDto> RoleResources { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
