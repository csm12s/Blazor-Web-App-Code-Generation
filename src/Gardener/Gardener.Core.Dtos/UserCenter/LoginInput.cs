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
        [Required(ErrorMessage ="不能为空。")]
        [MinLength(5, ErrorMessage = "长度不能小于5位")]
        [MaxLength(32,ErrorMessage = "长度不能大于32位")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MinLength(5, ErrorMessage = "长度不能小于5位")]
        [MaxLength(32, ErrorMessage = "长度不能大于32位")]
        public string Password { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public string LoginType { get; set; }
    }
}