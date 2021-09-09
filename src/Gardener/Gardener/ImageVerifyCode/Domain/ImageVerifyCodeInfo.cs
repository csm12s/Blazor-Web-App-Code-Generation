// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.ImageVerifyCode.Domain
{
    /// <summary>
    /// 验证码信息
    /// </summary>
    public class ImageVerifyCodeInfo
    {
        /// <summary>
        /// 校验码KEY，校验时回传
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 校验码（注意：校验码字段只做程序调试使用，不可传递到前端）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 校验码图片
        /// </summary>
        public byte[] Image { get; set; }
    }
}
