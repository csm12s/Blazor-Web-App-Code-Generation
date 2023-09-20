// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.UserCenter.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 岗位
    /// </summary>
    [Display(Name = nameof(UserCenterResource.Position), ResourceType = typeof(UserCenterResource))]
    public class PositionDto : TenantBaseDto<int>
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Name), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 设置该岗位的目标
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Target), ResourceType = typeof(UserCenterResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Target { get; set; }

        /// <summary>
        /// 职责
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Duty), ResourceType = typeof(UserCenterResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Duty { get; set; }

        /// <summary>
        /// 权利
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Right), ResourceType = typeof(UserCenterResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Right { get; set; }

        /// <summary>
        /// 岗位等级
        /// </summary>
        [CodeType("position-level")]
        [Display(Name = nameof(UserCenterResource.Grade), ResourceType = typeof(UserCenterResource))]
        [MaxLength(10, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Grade { get; set; }

        /// <summary>
        /// 岗位薪资
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Salary), ResourceType = typeof(UserCenterResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Salary { get; set; }


        /// <summary>
        /// 任职资格
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Qualifications), ResourceType = typeof(UserCenterResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Qualifications { get; set; }
    }
}
