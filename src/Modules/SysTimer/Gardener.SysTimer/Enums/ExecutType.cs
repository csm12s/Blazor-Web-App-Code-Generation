// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Enums
{
    /// <summary>
    /// 执行类型
    /// </summary>
    public enum ExecutType
    {
        /// <summary>
        /// 并行执行（不会等到上一个任务完成）
        /// </summary>
        [Description("并行")]
        Parallel = 0,
        /// <summary>
        /// 串行执行（等到上一个任务完成后执行）
        /// </summary>
        [Description("串行")]
        Scceeding = 1
    }
}
