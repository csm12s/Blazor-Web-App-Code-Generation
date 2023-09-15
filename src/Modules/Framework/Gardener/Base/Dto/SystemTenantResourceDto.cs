// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using Gardener.Base.Resources;

namespace Gardener.Base.Dto
{
    /// <summary>
    /// 租户资源
    /// </summary>
    [Display(Name = nameof(SharedLocalResource.SystemTenantResource), ResourceType = typeof(SharedLocalResource))]
    public class SystemTenantResourceDto : TenantBaseDtoNoKey
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [Display(Name = nameof(SharedLocalResource.TenantId), ResourceType = typeof(SharedLocalResource))]
        public new Guid TenantId { get; set; }

        /// <summary>
        /// 资源编号
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [Display(Name = nameof(SharedLocalResource.ResourceId), ResourceType = typeof(SharedLocalResource))]
        public Guid ResourceId { get; set; }
    }
}
