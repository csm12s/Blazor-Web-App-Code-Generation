// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace Gardener.VerifyCode.Core.Settings
{
    /// <summary>
    /// 图片验证码
    /// </summary>
    public class ImageVerifyCodeOptions : VerifyCodeOptions, IConfigurableOptions
    {
        
        /// <summary>
        /// 校验码字体大小（默认18）
        /// </summary>
        public int CodeFontSize { get; set; } = 18;
    }
}
