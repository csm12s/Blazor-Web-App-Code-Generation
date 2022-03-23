// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.SysTimer.Enums
{
    /// <summary>
    /// 异常状态码
    /// 详细提示配置到:exceptionmessagesettings.json/ErrorCodeMessageSettings
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 任务调度不存在
        /// </summary>
        [Description("任务调度不存在")]
        TASK_NOT_EXIST,
        /// <summary>
        /// 已存在同名任务调度
        /// </summary>
        [Description("已存在同名任务调度")]
        TASK_ALLREADY_EXIST,
    }
}
