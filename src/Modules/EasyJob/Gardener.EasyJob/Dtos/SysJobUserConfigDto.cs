// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.EasyJob.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// 定时任务用户配置
    /// </summary>
    [Display(Name = nameof(EasyJobLocalResource.SysJobUserConfig), ResourceType = typeof(EasyJobLocalResource))]
    public class SysJobUserConfigDto : BaseDto<int>
    {
        /// <summary>
        /// 身份唯一编号
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.IdentityId), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string IdentityId { get; set; } = null!;
        /// <summary>
        /// 身份类型
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.IdentityType), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public IdentityType IdentityType { get; set; } = IdentityType.Unknown;
        /// <summary>
        /// 是否启用实时监控
        /// </summary>
        [Display(Name = nameof(EasyJobLocalResource.EnableRealTimeMonitor), ResourceType = typeof(EasyJobLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public bool EnableRealTimeMonitor { get; set; } = false;
    }
}
