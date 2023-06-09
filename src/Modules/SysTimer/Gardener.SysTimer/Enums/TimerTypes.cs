// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.SysTimer.Enums
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum TimerTypes
    {
        /// <summary>
        /// 间隔方式
        /// </summary>        
        [Description("Interval")]
        Interval,
        /// <summary>
        /// Cron 表达式
        /// </summary>        
        [Description("Cron")]
        Cron
    }
}
