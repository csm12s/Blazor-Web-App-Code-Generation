// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Core.Enums
{
    /// <summary>
    /// 排序规则
    /// </summary>
    public enum RuleSort
    {
        /// <summary>
        /// 必排
        /// </summary>
        Must = 0,
        /// <summary>
        /// 禁止
        /// </summary>
        Prohibit = 1,
        /// <summary>
        /// 连排
        /// </summary>
        Continuity = 2
    }
}
