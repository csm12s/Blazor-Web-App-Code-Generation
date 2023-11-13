// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.EasyJob.Enums;
using Gardener.EasyJob.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 任务详情
    /// </summary>
    [Display(Name = nameof(EasyJobLocalResource.SysJobDetail), ResourceType = typeof(EasyJobLocalResource))]
    public class SysJobDetailDto : BaseDto<int>
    {
        /// <summary>
        /// 作业编号
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobId), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string JobId { get; set; } = null!;

        /// <summary>
        /// 作业组名称
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.GroupName), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? GroupName { get; set; }
        /// <summary>
        /// 作业处理程序类型
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型，存储的是类型的 FullName
        /// </remarks>
        [Display(Name = nameof(EasyJobLocalResource.JobTypeFullName), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? JobType { get; set; }
        /// <summary>
        /// 作业处理程序类型所在程序集
        /// </summary>
        /// <remarks>
        /// 作业处理程序类型所在程序集，存储的是程序集 Name
        /// </remarks>
        [Display(Name = nameof(EasyJobLocalResource.AssemblyName), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? AssemblyName { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Description), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Description { get; set; }
        /// <summary>
        /// 并行处理
        /// </summary>
        /// <remarks>
        /// 作业执行方式，
        /// 设置为 false 串行 执行
        /// 设置为 true  并行 执行
        /// </remarks>
        [Display(Name = nameof(EasyJobLocalResource.Concurrent), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool Concurrent { get; set; } = true;
        /// <summary>
        /// 是否扫描特性
        /// </summary>
        /// <remarks>
        /// IJob 实现类[Trigger] 特性触发器
        /// </remarks>
        [Display(Name = nameof(EasyJobLocalResource.IncludeAnnotations), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool IncludeAnnotations { get; set; } = false;
        /// <summary>
        /// 作业信息额外数据
        /// </summary>
        /// <remarks>
        /// 作业信息额外数据，由 Dictionary{string,object} 序列化成字符串存储 
        /// </remarks>
        [Display(Name = nameof(EasyJobLocalResource.Properties), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(2000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Properties { get; set; } = "{\"key\":\"value\"}";
        /// <summary>
        /// 作业创建类型
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobType), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public JobCreateType CreateType { get; set; } = JobCreateType.BuiltIn;
        /// <summary>
        /// 脚本代码
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.ScriptCode), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(5000, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? ScriptCode { get; set; }
        /// <summary>
        /// 触发器集合
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.JobTriggers), ResourceType = typeof(EasyJobLocalResource))]
        public IEnumerable<SysJobTriggerDto>? JobTriggers { get; set; }
    }
}
