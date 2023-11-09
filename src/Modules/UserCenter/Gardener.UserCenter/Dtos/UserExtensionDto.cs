// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.UserCenter.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 用户扩展数据
    /// </summary>
    [Display(Name = nameof(UserCenterResource.UserExtension), ResourceType = typeof(UserCenterResource))]

    public class UserExtensionDto: TenantBaseDtoNoKey
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [Display(Name = nameof(UserCenterResource.UserId), ResourceType = typeof(UserCenterResource))]
        public int UserId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        [Display(Name = nameof(UserCenterResource.QQ), ResourceType = typeof(UserCenterResource))]
        [MaxLength(15, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? QQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [Display(Name = nameof(UserCenterResource.WeChat), ResourceType = typeof(UserCenterResource))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? WeChat { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        [Display(Name = nameof(UserCenterResource.CityId), ResourceType = typeof(UserCenterResource))]
        public int? CityId { get; set; }
    }
}
