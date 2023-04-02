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
    public enum SysTimeExceptionCode
    {
        /// <summary>
        /// 任务调度不存在
        /// </summary>
        TASK_NOT_EXIST,
        /// <summary>
        /// 已存在同名任务调度
        /// </summary>
        TASK_ALLREADY_EXIST,
        /// <summary>
        /// 本地任务未找到
        /// </summary>
        LOCAL_JOB_NOT_FIND,
        /// <summary>
        /// 本地任务创建失败
        /// </summary>
        LOCAL_JOB_CREATE_FAIL,
        /// <summary>
        /// 请求地址是空
        /// </summary>
        REQUEST_URL_IS_NULL_OR_EMPTY,
        /// <summary>
        /// 任务执行间隔是null
        /// </summary>
        JOB_INTERVAL_IS_NULL
    }
}
