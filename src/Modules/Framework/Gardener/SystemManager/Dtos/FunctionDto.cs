// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Enums;
using Gardener.SystemManager.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 功能信息
    /// </summary>
    [Display(Name = nameof(SystemManagerResource.Function), ResourceType = typeof(SystemManagerResource))]
    public class FunctionDto : BaseDto<Guid>
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Group), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Group { get; set; } = null!;

        /// <summary>
        /// 服务
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Service), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Service { get; set; } = null!;

        /// <summary>
        /// 概要
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Summary), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Summary { get; set; } = null!;

        /// <summary>
        /// 唯一键
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Key), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Key { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Description), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Description { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Path), ResourceType = typeof(SharedLocalResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Path { get; set; }

        /// <summary>
        /// 接口请求方法
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Method), ResourceType = typeof(SharedLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public HttpMethod Method { get; set; }

        /// <summary>
        /// 启用审计
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.EnableAudit), ResourceType = typeof(SystemManagerResource))]
        public bool EnableAudit { get; set; }
    }
}
