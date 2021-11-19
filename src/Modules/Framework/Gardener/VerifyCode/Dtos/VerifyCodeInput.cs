﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.VerifyCode.Enums;

namespace Gardener.VerifyCode.Dtos
{
    /// <summary>
    /// 验证码输入
    /// </summary>
    public class VerifyCodeInput
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public virtual VerifyCodeTypeEnum VerifyCodeType { get;}
        /// <summary>
        /// 创建code参数
        /// 不穿时使用配置
        /// </summary>
        public CharacterCodeCreateParam CreateCodeParam { get; set; }
    }
}
