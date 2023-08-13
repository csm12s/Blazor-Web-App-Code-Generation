// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Gardener.Attributes;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 字典信息
    /// </summary>
    [Description("Code")]
    public class CodeDto : BaseDto<int>
    {
        /// <summary>
        /// 字段类型编号
        /// </summary>
        [DisplayName("CodeTypeId")]
        [Required(ErrorMessage ="不能为空")]
        public int CodeTypeId { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        [DisplayName("CodeValue")]
        [Required(ErrorMessage = "不能为空"), MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        public string CodeValue { get; set; } = null!;
        /// <summary>
        /// 字典名称
        /// </summary>
        [DisplayName("CodeName")]
        [Required(ErrorMessage = "不能为空"), MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        public string CodeName { get; set; } = null!;
        /// <summary>
        /// 本地化字典名称
        /// </summary>
        [DisplayName("LocalCodeName")]
        [DisabledSearchField]
        public string LocalCodeName { get; set; } = null!;
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("Order")]
        [Required(ErrorMessage = "不能为空")]
        public int Order { get; set; }
        /// <summary>
        /// 扩展参数
        /// </summary>
        [DisplayName("ExtendParams")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        public string? ExtendParams { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [DisplayName("Color")]
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        public string? Color { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public CodeTypeDto CodeType { get; set; } = null!;
    }
}
