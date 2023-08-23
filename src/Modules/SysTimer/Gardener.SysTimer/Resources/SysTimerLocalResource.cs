// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;

namespace Gardener.SysTimer.Resources
{
    /// <summary>
    /// 定时任务本地化资源
    /// </summary>
    public class SysTimerLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 任务名
        /// </summary>
        public const string JobName = nameof(JobName);
        /// <summary>
        /// 执行类型
        /// </summary>
        public const string ExecuteType = nameof(ExecuteType);
        /// <summary>
        /// 执行内容
        /// </summary>
        public const string ExecuteContent = nameof(ExecuteContent);
        /// <summary>
        /// 调度
        /// </summary>
        public const string Scheduling = nameof(Scheduling);
        /// <summary>
        /// 定时状态
        /// </summary>
        public const string TimerStatus = nameof(TimerStatus);
        /// <summary>
        /// 已执行
        /// </summary>
        public const string Executed = nameof(Executed);
        /// <summary>
        /// 执行失败
        /// </summary>
        public const string ExecutFailed = nameof(ExecutFailed);
        /// <summary>
        /// 本地方法
        /// </summary>
        public const string LocalMethod = nameof(LocalMethod);
        /// <summary>
        /// HttpMethod
        /// </summary>
        public const string HttpMethod = nameof(HttpMethod);
        /// <summary>
        /// 请求地址
        /// </summary>
        public const string RequestUrl = nameof(RequestUrl);
        /// <summary>
        /// 请求头
        /// </summary>
        public const string Headers = nameof(Headers);
        /// <summary>
        /// 请求参数
        /// </summary>
        public const string RequestParameters = nameof(RequestParameters);
        /// <summary>
        /// 定时类型
        /// </summary>
        public const string TimerType = nameof(TimerType);
        /// <summary>
        /// Cron
        /// </summary>
        public const string Cron = nameof(Cron);
        /// <summary>
        /// 间隔
        /// </summary>
        public const string Interval = nameof(Interval);
        /// <summary>
        /// 立刻开始
        /// </summary>
        public const string StartNow = nameof(StartNow);
        /// <summary>
        /// 执行一次
        /// </summary>
        public const string DoOnce = nameof(DoOnce);
        /// <summary>
        /// 执行模式
        /// </summary>
        public const string ExecutMode = nameof(ExecutMode);
        /// <summary>
        /// 并行
        /// </summary>
        public const string Parallel = nameof(Parallel);
        /// <summary>
        /// 串行
        /// </summary>
        public const string Scceeding = nameof(Scceeding);
        /// <summary>
        /// 已执行次数
        /// </summary>
        public const string RunNumber = nameof(RunNumber);
        /// <summary>
        /// 异常信息
        /// </summary>
        public const string Exception = nameof(Exception);
        /// <summary>
        /// 是否启动
        /// </summary>
        public const string Started = nameof(Started);
        /// <summary>
        /// 任务运行异常次数
        /// </summary>
        public const string RunErrorNumber = nameof(RunErrorNumber);
    }
}
