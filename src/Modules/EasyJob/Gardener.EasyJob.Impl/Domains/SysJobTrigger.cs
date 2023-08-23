// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Gardener.Base;
using System.ComponentModel;
using Gardener.EasyJob.Enums;

namespace Gardener.EasyJob.Impl.Domains
{
    /// <summary>
    /// 任务触发器
    /// </summary>
    [Description("任务触发器")]
    public class SysJobTrigger : GardenerEntityBase<int>
    {
        /// <summary>
        /// 作业触发器编号
        /// </summary>
        [DisplayName("作业触发器编号")]
        [Required, MaxLength(100)]
        public string TriggerId { get; set; } = null!;

        /// <summary>
        /// 作业 Id
        /// </summary>
        [DisplayName("作业编号")]
        [Required, MaxLength(100)]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业触发器类型
        /// </summary>
        /// <remarks>存储的是类型的 FullName</remarks>
        [DisplayName("作业触发器类型")]
        public string? TriggerType { get; set; }

        /// <summary>
        /// 作业触发器类型所在程序集
        /// </summary>
        /// <remarks>存储的是程序集 Name</remarks>
        [DisplayName("作业触发器类型所在程序集")]
        public string? AssemblyName { get; set; }

        /// <summary>
        /// 作业触发器参数
        /// </summary>
        /// <remarks>运行时将反序列化为 object[] 类型并作为构造函数参数</remarks>
        [DisplayName("作业触发器参数")]
        public string? Args { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [DisplayName("描述信息")]
        public string? Description { get; set; }

        /// <summary>
        /// 作业触发器状态
        /// </summary>
        [DisplayName("作业触发器状态")]
        public TriggerStatus Status { get; set; } = TriggerStatus.Ready;

        /// <summary>
        /// 起始时间
        /// </summary>
        [DisplayName("起始时间")]
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("结束时间")]
        public DateTimeOffset? EndTime { get; set; }

        /// <summary>
        /// 最近运行时间
        /// </summary>
        [DisplayName("最近运行时间")]
        public DateTimeOffset? LastRunTime { get; set; }

        /// <summary>
        /// 下一次运行时间
        /// </summary>
        [DisplayName("下一次运行时间")]
        public DateTimeOffset? NextRunTime { get; set; }

        /// <summary>
        /// 触发次数
        /// </summary>
        [DisplayName("触发次数")]
        public long NumberOfRuns { get; set; }

        /// <summary>
        /// 最大触发次数
        /// </summary>
        /// <remarks>
        /// <para>0：不限制</para>
        /// <para>n：N 次</para>
        /// </remarks>
        [DisplayName("最大触发次数")]
        public long MaxNumberOfRuns { get; set; }

        /// <summary>
        /// 出错次数
        /// </summary>
        [DisplayName("出错次数")]
        public long NumberOfErrors { get; set; }

        /// <summary>
        /// 最大出错次数
        /// </summary>
        /// <remarks>
        /// <para>0：不限制</para>
        /// <para>n：N 次</para>
        /// </remarks>
        [DisplayName("最大出错次数")]
        public long MaxNumberOfErrors { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        [DisplayName("重试次数")]
        public int NumRetries { get; set; } = 0;

        /// <summary>
        /// 重试间隔时间
        /// </summary>
        /// <remarks>默认1000毫秒</remarks>
        [DisplayName("重试间隔时间")]
        public int RetryTimeout { get; set; } = 1000;

        /// <summary>
        /// 是否立即启动
        /// </summary>
        [DisplayName("是否立即启动")]
        public bool StartNow { get; set; } = true;

        /// <summary>
        /// 是否启动时执行一次
        /// </summary>
        [DisplayName("是否启动时执行一次")]
        public bool RunOnStart { get; set; } = false;

        /// <summary>
        /// 是否在启动时重置最大触发次数等于一次的作业
        /// </summary>
        /// <remarks>解决因持久化数据已完成一次触发但启动时不再执行的问题</remarks>
        [DisplayName("是否在启动时重置最大触发次数等于一次的作业")]
        public bool ResetOnlyOnce { get; set; } = true;

        /// <summary>
        /// 本次执行结果
        /// </summary>
        [DisplayName("本次执行结果")]
        public string? Result { get; set; }

        /// <summary>
        /// 本次执行耗时
        /// </summary>
        [DisplayName("本次执行耗时")]
        public long ElapsedTime { get; set; }
    }
}
