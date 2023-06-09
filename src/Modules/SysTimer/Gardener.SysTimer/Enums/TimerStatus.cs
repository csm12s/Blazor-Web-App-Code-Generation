// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

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
        [Description("Running")]
        Running,
        /// <summary>
        /// 已停止
        /// </summary>        
        [Description("Stopped")]
        Stopped,
        /// <summary>
        /// 单次执行失败
        /// </summary>        
        [Description("Failed")]
        Failed,
        /// <summary>
        /// 任务已取消或没有该任务
        /// </summary>        
        [Description("CanceledOrNone")]
        CanceledOrNone
    }
}
