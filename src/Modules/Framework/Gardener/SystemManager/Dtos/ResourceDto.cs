// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Enums;
using Gardener.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 资源
    /// </summary>
    [Description("资源信息")]
    public class ResourceDto: BaseDto<Guid>, ITreeNode<ResourceDto>
    {
        /// <summary>
        /// 名称
        /// Locale Key
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Name")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Key")]
        public string Key { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        [MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Path")]
        public string? Path { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("Icon")]
        public string? Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DefaultValue(0)]
        [DisplayName("Order")]
        public int Order { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<ResourceDto>? Children { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Required, DefaultValue(ResourceType.Menu)]
        [DisplayName("Type")]
        public ResourceType Type { get; set; }

        /// <summary>
        /// 是否支持多租户
        /// </summary>
        [DisplayName("SupportMultiTenant")]
        public bool SupportMultiTenant { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        /// <remarks>
        /// 菜单类型：控制在界面中是否展示该菜单
        /// </remarks>
        [DisplayName("Hide")]
        public bool Hide { get; set; }
    }
}
