// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Gardener.EasyJob.Enums;

namespace Gardener.EasyJob.Impl.Domains
{
    /// <summary>
    /// 任务运行日志
    /// </summary>
    public class SysJobLog : GardenerEntityBase<long>
    {
        /// <summary>
        /// 作业编号
        /// </summary>
        [DisplayName("作业编号")]
        [Required, MaxLength(100)]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业触发器编号
        /// </summary>
        [DisplayName("作业触发器编号")]
        [Required, MaxLength(100)]
        public string TriggerId { get; set; } = null!;

        /// <summary>
        /// 作业触发器状态
        /// </summary>
        [DisplayName("作业触发器状态")]
        public TriggerStatus TriggerStatus { get; set; } = TriggerStatus.Ready;

        /// <summary>
        /// 本次执行结果
        /// </summary>
        [DisplayName("本次执行结果")]
        [MaxLength(5000)]
        public string? Result { get; set; }

        /// <summary>
        /// 本次执行耗时
        /// </summary>
        [DisplayName("本次执行耗时")]
        [Required]
        public long ElapsedTime { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        [DisplayName("是否成功")]
        [Required]
        public bool Succeeded { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        [DisplayName("异常")]
        [MaxLength(5000)]
        public string? Exception { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [DisplayName("异常信息")]
        [MaxLength(1000)]
        public string? ExceptionMessage { get; set; }

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
        [Required]
        public long NumberOfRuns { get; set; }

        /// <summary>
        /// 出错次数
        /// </summary>
        [DisplayName("出错次数")]
        [Required]
        public long NumberOfErrors { get; set; }


        /// <summary>
        /// 任务描述
        /// </summary>
        [DisplayName("任务描述")]
        public string? JobDetailDescription { get; set; }

        /// <summary>
        /// 触发器描述
        /// </summary>
        [DisplayName("触发器描述")]
        public string? JobTriggerDescription { get; set; }
    }
}
