// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.VerifyCode.Dtos;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Core
{
    /// <summary>
    /// 验证码基础服务
    /// </summary>
    public interface IVerifyCode
    {
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="input">验证码类型</param>
        /// <returns></returns>
        public Task<VerifyCodeOutput> Create(VerifyCodeInput input);
        /// <summary>
        /// 验证校验码
        /// </summary>
        /// <returns></returns>
        Task<bool> Verify(string key, string code);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Remove(string key);
    }
}
