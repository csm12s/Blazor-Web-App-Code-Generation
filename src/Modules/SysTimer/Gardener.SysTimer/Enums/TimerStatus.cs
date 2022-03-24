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
    /// 任务状态
    /// </summary>    
    public enum TimerStatus
    {
        /// <summary>
        /// 运行中
        /// </summary>        
        [Description("运行中")]
        Running,
        /// <summary>
        /// 已停止或未启动
        /// </summary>        
        [Description("已停止")]
        Stopped,
        /// <summary>
        /// 单次执行失败
        /// </summary>        
        [Description("失败")]
        Failed,
        /// <summary>
        /// 任务已取消或没有该任务
        /// </summary>        
        [Description("不存在")]
        CanceledOrNone
    }
}
