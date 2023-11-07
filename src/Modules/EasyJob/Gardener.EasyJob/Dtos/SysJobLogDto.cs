// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EasyJob.Enums;
using System.ComponentModel.DataAnnotations;
using Gardener.Base;
using Gardener.EasyJob.Resources;
using Gardener.Base.Resources;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 定时任务日志
    /// </summary>
    [Display(Name = nameof(EasyJobLocalResource.SysJobLog), ResourceType = typeof(EasyJobLocalResource))]
    public class SysJobLogDto : BaseDto<long>
    {
        /// <summary>
        /// 作业编号
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobId), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业触发器编号
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.TriggerId), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string TriggerId { get; set; } = null!;

        /// <summary>
        /// 作业触发器状态
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.TriggerStatus), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public TriggerStatus TriggerStatus { get; set; } = TriggerStatus.Ready;

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

        /// <summary>
        /// 是否成功
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Succeeded), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool Succeeded { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Exception), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Exception { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.ExceptionMessage), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ExceptionMessage { get; set; }

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
        /// 出错次数
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.NumberOfErrors), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public long NumberOfErrors { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobDetailDescription), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? JobDetailDescription { get; set; }

        /// <summary>
        /// 触发器描述
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobTriggerDescription), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? JobTriggerDescription { get; set; }
    }
}
