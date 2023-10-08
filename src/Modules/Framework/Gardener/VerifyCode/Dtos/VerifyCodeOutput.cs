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
    /// 验证码返回结果
    /// </summary>
    public class VerifyCodeOutput
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public virtual VerifyCodeTypeEnum VerifyCodeType { get; }
        /// <summary>
        /// 验证码唯一键
        /// </summary>
        [DisplayName("验证码唯一键")]
        public string Key { get; set; } = null!;
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int? CodeLength { get; set; }
    }
}
