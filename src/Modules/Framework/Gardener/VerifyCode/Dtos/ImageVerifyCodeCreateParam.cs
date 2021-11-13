// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;

namespace Gardener.VerifyCode.Dtos
{
    /// <summary>
    /// 生成校验码参数
    /// </summary>
    public class ImageVerifyCodeCreateParam
    {
        /// <summary>
        /// 校验码字体大小
        /// </summary>
        public int FontSize { get; set; } = 18;

        /// <summary>
        /// 校验码字符数量
        /// </summary>
        public int CharacterCount { get; set; } = 4;

        /// <summary>
        /// 校验码类别,有三种类别可选，分别是纯数字，纯字符，数字加字符
        /// </summary>
        public CodeCharacterTypeEnum Type { get; set; } = CodeCharacterTypeEnum.NumberAndCharacter;
    }
}
