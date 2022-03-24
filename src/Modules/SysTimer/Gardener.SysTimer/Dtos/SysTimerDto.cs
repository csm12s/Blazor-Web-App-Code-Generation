// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
using Gardener.Base;
using Gardener.SysTimer.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.SysTimer.Dtos
{
    /// <summary>
    /// 定时任务Dto
    /// </summary>
    public class SysTimerDto:BaseDto<int>
    {
        /// <summary>
        /// 已执行次数
        /// </summary>
        [DisplayName("已执行次数")]
        public long? RunNumber { get; set; }
        /// <summary>
        /// 定时器状态
        /// </summary>
        [DisplayName("状态")]
        public TimerStatus TimerStatus { get; set; } = TimerStatus.Stopped; //已停止或未启动
        /// <summary>
        /// 异常信息
        /// </summary>
        [DisplayName("异常信息")]
        public string Exception { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [DisplayName("任务名称")]
        [Required, MaxLength(20)]
        public string JobName { get; set; }

        /// <summary>
        /// 只执行一次
        /// </summary>
        [DisplayName("只执行一次")]
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        [DisplayName("立即执行")]
        public bool StartNow { get; set; } = false;

        /// <summary>
        /// 执行类型(并行、列队)
        /// </summary>
        [DisplayName("执行类型")]
        public ExecutType ExecuteType { get; set; }

        /// <summary>
        /// 执行间隔时间（单位秒）
        /// </summary>
        /// <example>5</example>
        [DisplayName("间隔(秒)")][Required]
        public int? Interval { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [DisplayName("Cron表达式")]
        [MaxLength(20)]
        public string Cron { get; set; }

        /// <summary>
        /// 定时器类型
        /// </summary>
        [DisplayName("定时器类型")]
        public TimerTypes TimerType { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        [DisplayName("请求url")]
        [MaxLength(200)]
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求参数（Post，Put请求用）
        /// </summary>
        [DisplayName("请求参数")]
        public string RequestParameters { get; set; }

        /// <summary>
        /// Headers(可以包含如：Authorization授权认证)
        /// 格式：{"Authorization":"userpassword.."}
        /// </summary>
        [DisplayName("请求头")]
        public string Headers { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        [DisplayName("请求类型")]
        public RequestType RequestType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(100)]
        public string Remark { get; set; }
    }

    public class StopJobInput
    {
        public string JobName { get; set; }
    }

    public class DeleteJobInput : BaseDto<int>
    {
    }

    public class QueryJobInput : BaseDto<int>
    {
    }
}
