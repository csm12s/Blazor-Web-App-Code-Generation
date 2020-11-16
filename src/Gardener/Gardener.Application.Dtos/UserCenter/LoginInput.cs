using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage ="用户名必填"), MinLength(5,ErrorMessage ="用户名不正确")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, MinLength(5)]
        public string Password { get; set; }
    }
}