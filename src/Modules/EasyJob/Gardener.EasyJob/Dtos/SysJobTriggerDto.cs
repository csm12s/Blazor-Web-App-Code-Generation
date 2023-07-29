// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Gardener.Base;
using System.ComponentModel;
using Gardener.EasyJob.Enums;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 任务触发器
    /// </summary>
    [Description("任务触发器")]
    public class SysJobTriggerDto : BaseDto<int>
    {
        /// <summary>
        /// 作业触发器编号
        /// </summary>
        [DisplayName("TriggerId")]
        [Required, MaxLength(100)]
        public string TriggerId { get; set; } = null!;

        /// <summary>
        /// 作业 Id
        /// </summary>
        [DisplayName("JobId")]
        [Required, MaxLength(100)]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业触发器类型
        /// </summary>
        /// <remarks>存储的是类型的 FullName</remarks>
        [DisplayName("TriggerType")]
        public string? TriggerType { get; set; }

        /// <summary>
        /// 作业触发器类型所在程序集
        /// </summary>
        /// <remarks>存储的是程序集 Name</remarks>
        [DisplayName("AssemblyName")]
        public string? AssemblyName { get; set; }

        /// <summary>
        /// 作业触发器参数
        /// </summary>
        /// <remarks>运行时将反序列化为 object[] 类型并作为构造函数参数</remarks>
        [DisplayName("TriggerArgs")]
        public string? Args { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [DisplayName("Description")]
        public string? Description { get; set; }

        /// <summary>
        /// 作业触发器状态
        /// </summary>
        [DisplayName("Status")]
        public TriggerStatus Status { get; set; } = TriggerStatus.Ready;

        /// <summary>
        /// 起始时间
        /// </summary>
        [DisplayName("StartTime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("EndTime")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 最近运行时间
        /// </summary>
        [DisplayName("LastRunTime")]
        public DateTime? LastRunTime { get; set; }

        /// <summary>
        /// 下一次运行时间
        /// </summary>
        [DisplayName("NextRunTime")]
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// 触发次数
        /// </summary>
        [DisplayName("NumberOfRuns")]
        public long NumberOfRuns { get; set; }

        /// <summary>
        /// 最大触发次数
        /// </summary>
        /// <remarks>
        /// <para>0：不限制</para>
        /// <para>n：N 次</para>
        /// </remarks>
        [DisplayName("MaxNumberOfRuns")]
        public long MaxNumberOfRuns { get; set; }

        /// <summary>
        /// 出错次数
        /// </summary>
        [DisplayName("NumberOfErrors")]
        public long NumberOfErrors { get; set; }

        /// <summary>
        /// 最大出错次数
        /// </summary>
        /// <remarks>
        /// <para>0：不限制</para>
        /// <para>n：N 次</para>
        /// </remarks>
        [DisplayName("MaxNumberOfErrors")]
        public long MaxNumberOfErrors { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        [DisplayName("NumRetries")]
        public int NumRetries { get; set; } = 0;

        /// <summary>
        /// 重试间隔时间
        /// </summary>
        /// <remarks>默认1000毫秒</remarks>
        [DisplayName("RetryTimeout")]
        public int RetryTimeout { get; set; } = 1000;

        /// <summary>
        /// 是否立即启动
        /// </summary>
        [DisplayName("StartNow")]
        public bool StartNow { get; set; } = true;

        /// <summary>
        /// 是否启动时执行一次
        /// </summary>
        [DisplayName("RunOnceOnStart")]
        public bool RunOnStart { get; set; } = false;

        /// <summary>
        /// 是否在启动时重置最大触发次数等于一次的作业
        /// </summary>
        /// <remarks>解决因持久化数据已完成一次触发但启动时不再执行的问题</remarks>
        [DisplayName("ResetOnlyOnce")]
        public bool ResetOnlyOnce { get; set; } = true;

        /// <summary>
        /// 本次执行结果
        /// </summary>
        [DisplayName("Result")]
        public string? Result { get; set; }

        /// <summary>
        /// 本次执行耗时
        /// </summary>
        [DisplayName("ElapsedTime")]
        public long ElapsedTime { get; set; }
    }
}
