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
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(ErrorMessage = "不能为空"), MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("唯一键")]
        public string Key { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        [MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("地址")]
        public string Path { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [Required(ErrorMessage = "不能为空"), DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<ResourceDto> Children { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Required, DefaultValue(ResourceType.Menu)]
        [DisplayName("类型")]
        public ResourceType Type { get; set; }
    }
}
