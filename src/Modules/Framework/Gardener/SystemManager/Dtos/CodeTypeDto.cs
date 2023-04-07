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
    [Description("字典类型")]
    public class CodeTypeDto : BaseDto<int>
    {
        /// <summary>
        /// 字典类型名称
        /// </summary>
        [DisplayName("字典类型名称")]
        [Required, MaxLength(50)]
        public string CodeTypeName { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("字典类型名称")]
        public string? Remark { get; set; }
    }
}
