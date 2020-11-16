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
        [Required, MinLength(5)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, MinLength(5)]
        public string Password { get; set; }
    }
}