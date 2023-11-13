// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Enums;
using Gardener.Base.Resources;
using Gardener.Common;
using Gardener.SystemManager.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.SystemManager.Dtos
{
    /// <summary>
    /// 资源
    /// </summary>
    [Display(Name = nameof(SystemManagerResource.Resource), ResourceType = typeof(SystemManagerResource))]
    public class ResourceDto : BaseDto<Guid>, ITreeNode<ResourceDto>
    {
        /// <summary>
        /// 名称
        /// Locale Key
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Name), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 唯一键
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Key), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Key { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Remark), ResourceType = typeof(SystemManagerResource))]
        [MaxLength(500, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Path), ResourceType = typeof(SystemManagerResource))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Path { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Icon), ResourceType = typeof(SystemManagerResource))]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [DefaultValue(0)]
        [Display(Name = nameof(SystemManagerResource.Order), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public int Order { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.ParentId), ResourceType = typeof(SystemManagerResource))]
        [NotMapped]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Children), ResourceType = typeof(SystemManagerResource))]
        [NotMapped]
        public ICollection<ResourceDto>? Children { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DefaultValue(ResourceType.Menu)]
        [Display(Name = nameof(SystemManagerResource.Type), ResourceType = typeof(SystemManagerResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public ResourceType Type { get; set; }
        /// <summary>
        /// 是否支持多租户
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.SupportMultiTenant), ResourceType = typeof(SystemManagerResource))]
        public bool SupportMultiTenant { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        /// <remarks>
        /// 菜单类型：控制在界面中是否展示该菜单
        /// </remarks>
        [Display(Name = nameof(SystemManagerResource.Hide), ResourceType = typeof(SystemManagerResource))]
        public bool Hide { get; set; }
    }
}
