// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [Description("图片验证码")]
    public class ImageVerifyCodeDto
    {
        /// <summary>
        /// 验证码唯一键
        /// </summary>
        [DisplayName("验证码唯一键")]
        public string Key { get; set; }
        /// <summary>
        /// Base64图片
        /// </summary>
        [DisplayName("Base64图片")]
        public string Base64Image { get; set; }
    }
}
