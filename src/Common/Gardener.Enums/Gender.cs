// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("Male")]
        [TagColor(ClientAntPresetColor.Blue)]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [Description("Female")]
        [TagColor(ClientAntPresetColor.Magenta)]
        Female,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("Other")]
        [TagColor(ClientAntPresetColor.Volcano)]
        Other
    }
}