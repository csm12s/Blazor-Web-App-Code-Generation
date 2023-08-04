// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System.Reflection;

namespace Gardener.EasyJob.Resources
{
    /// <summary>
    /// EasyJob 本地化资源
    /// </summary>
    public class EasyJobLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public const string JobId= nameof(JobId);
        /// <summary>
        /// 分组名
        /// </summary>
        public const string GroupName = nameof(GroupName);
        /// <summary>
        /// 
        /// </summary>
        public const string JobTypeFullName = nameof(JobTypeFullName);
        /// <summary>
        /// 并行
        /// </summary>
        public const string Concurrent = nameof(Concurrent);
        /// <summary>
        /// 
        /// </summary>
        public const string AssemblyName = nameof(AssemblyName);
        /// <summary>
        /// 
        /// </summary>
        public const string IncludeAnnotations = nameof(IncludeAnnotations);
        /// <summary>
        /// 
        /// </summary>
        public const string JobType = nameof(JobType);
        /// <summary>
        /// 属性
        /// </summary>
        public const string Properties = nameof(Properties);
        /// <summary>
        /// 触发器编号
        /// </summary>
        public const string TriggerId = nameof(TriggerId);
        /// <summary>
        /// 触发器参数
        /// </summary>
        public const string TriggerArgs = nameof(TriggerArgs);
        /// <summary>
        /// 触发次数
        /// </summary>
        public const string NumberOfRuns = nameof(NumberOfRuns);
        /// <summary>
        /// 立即启动
        /// </summary>
        public const string StartNow = nameof(StartNow);
        /// <summary>
        /// 启动时执行一次
        /// </summary>
        public const string RunOnceOnStart = nameof(RunOnceOnStart);
        /// <summary>
        /// 在启动时重置最大触发次数等于一次的作业
        /// </summary>
        public const string ResetOnlyOnce = nameof(ResetOnlyOnce);
        /// <summary>
        /// 最大触发次数
        /// </summary>
        public const string MaxNumberOfRuns = nameof(MaxNumberOfRuns);
        /// <summary>
        /// 出错次数
        /// </summary>
        public const string NumberOfErrors = nameof(NumberOfErrors);
        /// <summary>
        /// 最大出错次数
        /// </summary>
        public const string MaxNumberOfErrors = nameof(MaxNumberOfErrors);
        /// <summary>
        /// 重试次数
        /// </summary>
        public const string NumRetries = nameof(NumRetries);
        /// <summary>
        /// 重试间隔时间
        /// </summary>
        public const string RetryTimeout = nameof(RetryTimeout);
        /// <summary>
        /// 最近运行时间
        /// </summary>
        public const string LastRunTime = nameof(LastRunTime);
        /// <summary>
        /// 下一次运行时间
        /// </summary>
        public const string NextRunTime = nameof(NextRunTime);
        /// <summary>
        /// 作业触发器类型
        /// </summary>
        public const string TriggerAssemblyType = nameof(TriggerAssemblyType);
        /// <summary>
        /// 消耗时间
        /// </summary>
        public const string ElapsedTime = nameof(ElapsedTime);
        /// <summary>
        /// 暂停
        /// </summary>
        public const string Pause = nameof(Pause);
        /// <summary>
        /// 启动
        /// </summary>
        public const string Start = nameof(Start);
        /// <summary>
        /// 实时监控
        /// </summary>
        public const string RealTimeMonitor = nameof(RealTimeMonitor);


    }
}
