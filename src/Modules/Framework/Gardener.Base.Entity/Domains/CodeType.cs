// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Description("CodeType")]
    public class CodeType : GardenerEntityBase
    {
        /// <summary>
        /// 字典类型名称
        /// </summary>
        [DisplayName("CodeTypeName")]
        [Required, MaxLength(50)]
        public string CodeTypeName { get; set; } = null!;
        /// <summary>
        /// 字典类型值
        /// </summary>
        [DisplayName("CodeTypeValue")]
        [Required, MaxLength(50)]
        public string CodeTypeValue { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("Remark")]
        [MaxLength(200)]
        public string? Remark { get; set; }
        /// <summary>
        /// 字典集合
        /// </summary>
        public ICollection<Code> Codes { get; set; }= new List<Code>();
    }
}
