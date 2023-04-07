// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Domains
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Description("字典类型")]
    public class CodeType : GardenerEntityBase
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
