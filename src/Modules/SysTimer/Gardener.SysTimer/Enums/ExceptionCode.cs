// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.SysTimer.Enums
{
    /// <summary>
    /// 异常状态码
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 任务调度不存在
        /// </summary>
        TASK_NOT_EXIST,
        /// <summary>
        /// 已存在同名任务调度
        /// </summary>
        TASK_ALLREADY_EXIST,
    }
}
