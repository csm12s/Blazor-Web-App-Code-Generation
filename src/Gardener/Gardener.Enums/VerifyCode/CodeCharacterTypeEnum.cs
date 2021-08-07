// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Enums
{
    /// <summary>
    /// 校验码字符类别，有数字，字符，数字加字符
    /// </summary>
    public enum CodeCharacterTypeEnum
    {
        /// <summary>
        /// 数字
        /// </summary>
        Number = 1,

        /// <summary>
        /// 字符
        /// </summary>
        Character = 2,

        /// <summary>
        /// 数字加字符
        /// </summary>
        NumberAndCharacter = 3,
    }
}
