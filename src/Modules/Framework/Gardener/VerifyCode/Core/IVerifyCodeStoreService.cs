// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.VerifyCode.Enums;
using System;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Core
{
    /// <summary>
    /// 图片验证码存储
    /// </summary>
    public interface IVerifyCodeStoreService
    {
        /// <summary>
        /// 保存校验码
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        Task Add(VerifyCodeTypeEnum verifyCodeType, string key, string code, TimeSpan expire);

        /// <summary>
        /// 获取校验码
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string?> GetCode(VerifyCodeTypeEnum verifyCodeType, string key);

        /// <summary>
        /// 移除校验码
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Remove(VerifyCodeTypeEnum verifyCodeType, string key);
    }
}
