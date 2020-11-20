// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Dtos
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
        [Required, MaxLength(64)]
        public string ResourceId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 权限名称简写
        /// </summary>
        [Required, MaxLength(100)]
        public string SortName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        [MaxLength(200)]
        public string Path { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [Required, DefaultValue(0)]
        public int Order { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public ResourceDto Parent { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<ResourceDto> Childrens { get; set; }
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
    }
}
