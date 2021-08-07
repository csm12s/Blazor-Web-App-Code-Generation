// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 验证码输入
    /// </summary>
    public class VerifyCodeInput
    {
        /// <summary>
        /// 验证码Key
        /// </summary>
        [Required(ErrorMessage = "验证码Key不能为空。")]
        [DisplayName("验证码Key")]
        public string VerifyCodeKey { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空。")]
        [DisplayName("验证码")]
        public string VerifyCode { get; set; }
    }
}
