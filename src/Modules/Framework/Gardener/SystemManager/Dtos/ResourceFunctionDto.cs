// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.SystemManager.Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 资源功能关系信息
    /// </summary>
    [Display(Name = nameof(SystemManagerResource.ResourceFunction), ResourceType = typeof(SystemManagerResource))]
    public class ResourceFunctionDto
    {

        /// <summary>
        /// 权限Id
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.ResourceId), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.FunctionId), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CreatedTime), ResourceType = typeof(SystemManagerResource))]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
    }
}
