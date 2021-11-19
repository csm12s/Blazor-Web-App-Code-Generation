// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;

namespace Gardener.VerifyCode.Core.Settings
{
    /// <summary>
    /// 验证码配置
    /// </summary>
    public class VerifyCodeOptions
    {
        /// <summary>
        /// 校验码过期时间(默认10分钟)
        /// </summary>
        public TimeSpan CodeExpire { get; set; } = new TimeSpan(0, 10, 0);

        /// <summary>
        /// 校验码字符数量（默认4个字符）
        /// </summary>
        public int CodeCharacterCount { get; set; } = 4;

        /// <summary>
        /// 校验码类别,有三种类别可选，分别是纯数字，纯字符，数字加字符
        /// </summary>
        public CodeCharacterTypeEnum CodeType { get; set; } = CodeCharacterTypeEnum.NumberAndCharacter;

        /// <summary>
        /// 是否忽略字母大小写(默认为true)
        /// </summary>
        public bool IgnoreCase { get; set; } = true;

        /// <summary>
        /// 只验证一次（验证通过后，删除校验码，防止用一个校验码反复提交）
        /// </summary>
        public bool UseOnce { get; set; } = true;
    }
}
