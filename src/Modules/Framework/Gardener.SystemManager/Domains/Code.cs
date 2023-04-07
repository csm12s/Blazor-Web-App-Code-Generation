// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.SystemManager.Domains
{
    /// <summary>
    /// 字典信息
    /// </summary>
    [Description("字典信息")]
    public class Code : GardenerEntityBase
    {
        /// <summary>
        /// 字段类型编号
        /// </summary>
        [DisplayName("字段类型编号")]
        [Required, MaxLength(50)]
        public int CodeTypeId { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        [DisplayName("字典名称")]
        [Required, MaxLength(50)]
        public string CodeName { get; set; } = null!;
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Required]
        public int Order { get; set; }
        /// <summary>
        /// 扩展参数
        /// </summary>
        [DisplayName("扩展参数")]
        [MaxLength(500)]
        public string? ExtendParams { get;set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [DisplayName("颜色")]
        [MaxLength(50)]
        public string? Color { get; set; }
    }
}
