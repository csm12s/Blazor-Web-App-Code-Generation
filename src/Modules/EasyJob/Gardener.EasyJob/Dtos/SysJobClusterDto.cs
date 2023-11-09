// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.EasyJob.Enums;
using Gardener.EasyJob.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 系统作业集群表
    /// </summary>
    [Display(Name = nameof(EasyJobLocalResource.SysJobCluster), ResourceType = typeof(EasyJobLocalResource))]
    public class SysJobClusterDto : BaseDto<int>
    {
        /// <summary>
        /// 作业集群Id
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.ClusterId), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(64, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ClusterId { get; set; } = null!;

        /// <summary>
        /// 描述信息
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Description), ResourceType = typeof(EasyJobLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.Status), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public ClusterStatus Status { get; set; } = ClusterStatus.Crashed;
    }
}
