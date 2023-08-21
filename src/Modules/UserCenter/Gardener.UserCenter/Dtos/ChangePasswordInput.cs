// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 修改密码输入
    /// </summary>
    public class ChangePasswordInput
    {
        /// <summary>
        /// 修改密码输入
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmNewPassword"></param>
        public ChangePasswordInput(string oldPassword, string newPassword, string confirmNewPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }

        /// <summary>
        /// 老密码
        /// </summary>
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string NewPassword { get; set; }
        /// <summary>
        /// 新密码-确认
        /// </summary>
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string ConfirmNewPassword { get; set; }
    }
}
