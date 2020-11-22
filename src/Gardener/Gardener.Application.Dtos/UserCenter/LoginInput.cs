using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Dtos
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空。")]
        [MinLength(5, ErrorMessage = "长度不能小于5位。")]
        [MaxLength(32,ErrorMessage = "长度不能大于32位。")]
        [DisplayName("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空。")]
        [MinLength(5, ErrorMessage = "长度不能小于5位。")]
        [MaxLength(32, ErrorMessage = "长度不能大于32位。")]
        [DisplayName("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public string LoginType { get; set; }
    }
}