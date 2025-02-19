﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.VerifyCode.Core
{
    /// <summary>
    /// 验证码自动验证
    /// 但是要保证参数要继承<see cref="Gardener.VerifyCode.Dtos.VerifyCodeCheckInput"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class VerifyCodeAutoVerificationAttribute : Attribute
    {

    }
}
