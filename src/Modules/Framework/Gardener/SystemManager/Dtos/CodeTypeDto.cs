// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Gardener.Base;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Description("CodeType")]
    public class CodeTypeDto : BaseDto<int>
    {
        /// <summary>
        /// 字典类型名称
        /// </summary>
        [DisplayName("CodeTypeName")]
        [Required(ErrorMessage = "不能为空"), MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        public string CodeTypeName { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("Remark")]
        [MaxLength(200, ErrorMessage = "最大长度不能大于{1}")]
        public string? Remark { get; set; }
    }
}
