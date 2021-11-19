// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.VerifyCode.Enums;

namespace Gardener.VerifyCode.Dtos
{
    /// <summary>
    /// 图片验证码输入
    /// </summary>
    public class ImageVerifyCodeInput : VerifyCodeInput
    {
        /// <summary>
        /// 校验码字体大小
        /// </summary>
        public int? FontSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Image;
    }
}
