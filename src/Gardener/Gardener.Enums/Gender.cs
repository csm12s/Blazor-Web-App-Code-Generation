// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------
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
        [Description("男")]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female
    }
}