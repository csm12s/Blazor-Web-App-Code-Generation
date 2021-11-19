// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;

namespace Gardener.VerifyCode.Dtos
{
    /// <summary>
    /// 字符验证码参数
    /// </summary>
    public class CharacterCodeCreateParam 
    {
        /// <summary>
        /// 校验码字符数量
        /// </summary>
        public int? CharacterCount { get; set; }

        /// <summary>
        /// 校验码类别,有三种类别可选，分别是纯数字，纯字符，数字加字符
        /// </summary>
        public CodeCharacterTypeEnum? Type { get; set; }
    }
}
