// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Gardener.Base;
using Gardener.EasyJob.Enums;
using Gardener.Base.Resources;
using Gardener.EasyJob.Resources;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 任务触发器
    /// </summary>
    [Display(Name = nameof(EasyJobLocalResource.SysJobTrigger), ResourceType = typeof(EasyJobLocalResource))]
    public class SysJobTriggerDto : BaseDto<int>
    {
        /// <summary>
        /// 作业触发器编号
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.TriggerId), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string TriggerId { get; set; } = null!;

        /// <summary>
        /// 作业 Id
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobId), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业触发器类型
        /// </summary>
        /// <remarks>存储的是类型的 FullName</remarks>
        [Display(Name = nameof(EasyJobLocalResource.TriggerAssemblyType), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? TriggerType { get; set; }

        /// <summary>
        /// 作业触发器类型所在程序集
        /// </summary>
        /// <remarks>存储的是程序集 Name</remarks>
        [Display(Name = nameof(EasyJobLocalResource.AssemblyName), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? AssemblyName { get; set; }

        /// <summary>
        /// 作业触发器参数
        /// </summary>
        /// <remarks>运行时将反序列化为 object[] 类型并作为构造函数参数</remarks>
        [Display(Name = nameof(EasyJobLocalResource.TriggerArgs), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Args { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Description), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Description { get; set; }

        /// <summary>
        /// 作业触发器状态
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Status), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public TriggerStatus Status { get; set; } = TriggerStatus.Ready;

        /// <summary>
        /// 起始时间
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.StartTime), ResourceType = typeof(EasyJobLocalResource))]
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.EndTime), ResourceType = typeof(EasyJobLocalResource))]
        public DateTimeOffset? EndTime { get; set; }

        /// <summary>
        /// 最近运行时间
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.LastRunTime), ResourceType = typeof(EasyJobLocalResource))]
        public DateTimeOffset? LastRunTime { get; set; }

        /// <summary>
        /// 下一次运行时间
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.NextRunTime), ResourceType = typeof(EasyJobLocalResource))]
        public DateTimeOffset? NextRunTime { get; set; }

        /// <summary>
        /// 触发次数
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.NumberOfRuns), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public long NumberOfRuns { get; set; }

        /// <summary>
        /// 最大触发次数
        /// </summary>
        /// <remarks>
        /// <para>0：不限制</para>
        /// <para>n：N 次</para>
        /// </remarks>
        [Display(Name = nameof(EasyJobLocalResource.MaxNumberOfRuns), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public long MaxNumberOfRuns { get; set; }

        /// <summary>
        /// 出错次数
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.NumberOfErrors), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public long NumberOfErrors { get; set; }

        /// <summary>
        /// 最大出错次数
        /// </summary>
        /// <remarks>
        /// <para>0：不限制</para>
        /// <para>n：N 次</para>
        /// </remarks>
        [Display(Name = nameof(EasyJobLocalResource.MaxNumberOfErrors), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public long MaxNumberOfErrors { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.NumRetries), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int NumRetries { get; set; } = 0;

        /// <summary>
        /// 重试间隔时间
        /// </summary>
        /// <remarks>默认1000毫秒</remarks>
        [Display(Name = nameof(EasyJobLocalResource.RetryTimeout), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int RetryTimeout { get; set; } = 1000;

        /// <summary>
        /// 是否立即启动
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.StartNow), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool StartNow { get; set; } = true;

        /// <summary>
        /// 是否启动时执行一次
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.RunOnceOnStart), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool RunOnStart { get; set; } = false;

        /// <summary>
        /// 是否在启动时重置最大触发次数等于一次的作业
        /// </summary>
        /// <remarks>解决因持久化数据已完成一次触发但启动时不再执行的问题</remarks>
        [Display(Name = nameof(EasyJobLocalResource.ResetOnlyOnce), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool ResetOnlyOnce { get; set; } = true;

        /// <summary>
        /// 本次执行结果
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Result), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Result { get; set; }

        /// <summary>
        /// 本次执行耗时
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.ElapsedTime), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public long ElapsedTime { get; set; }
    }
}
