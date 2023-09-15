// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.UserCenter.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 更新头像
    /// </summary>
    [Display(Name = nameof(UserCenterResource.UpdateUserAvatar), ResourceType = typeof(UserCenterResource))]
    public class UpdateUserAvatarInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = nameof(UserCenterResource.UserId), ResourceType = typeof(UserCenterResource))]
        public int Id { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = nameof(UserCenterResource.Avatar), ResourceType = typeof(UserCenterResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? Avatar { get; set; }
    }
}
