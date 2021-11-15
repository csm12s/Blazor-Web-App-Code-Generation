// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.VerifyCode.Enums
{
    /// <summary>
    /// 验证码类型
    /// </summary>
    public enum VerifyCodeTypeEnum
    {
        /// <summary>
        /// 图形验证码
        /// </summary>
        Image=1,
        /// <summary>
        /// 邮件验证码
        /// </summary>
        Email=2,
        /// <summary>
        /// 短信验证码
        /// </summary>
        SMS=3

    }
}
