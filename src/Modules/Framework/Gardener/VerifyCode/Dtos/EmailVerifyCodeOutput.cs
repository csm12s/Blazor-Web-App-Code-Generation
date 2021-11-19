// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.VerifyCode.Enums;
using System.ComponentModel;

namespace Gardener.VerifyCode.Dtos
{
    /// <summary>
    /// 邮件验证码返回结果
    /// </summary>
    [Description("邮件验证码返回结果")]
    public class EmailVerifyCodeOutput : VerifyCodeOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Email;
    }
}
