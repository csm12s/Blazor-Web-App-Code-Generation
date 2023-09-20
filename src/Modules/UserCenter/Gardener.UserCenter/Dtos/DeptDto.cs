// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.UserCenter.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Display(Name = nameof(UserCenterResource.Dept), ResourceType = typeof(UserCenterResource))]
    public class DeptDto: TenantBaseDto<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Name), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(30, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Contacts), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Tel), ResourceType = typeof(UserCenterResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Tel { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Order), ResourceType = typeof(UserCenterResource))]
        public int Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Remark), ResourceType = typeof(UserCenterResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Remark { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [Display(Name = nameof(UserCenterResource.ParentId), ResourceType = typeof(UserCenterResource))]
        public int? ParentId { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<DeptDto>? Children { get; set; }
    }
}
