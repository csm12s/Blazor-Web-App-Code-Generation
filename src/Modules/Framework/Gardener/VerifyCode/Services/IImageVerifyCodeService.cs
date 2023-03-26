// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.VerifyCode.Dtos;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Services
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    public interface IImageVerifyCodeService
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<ImageVerifyCodeOutput?> Create(ImageVerifyCodeInput input);

        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Remove(string key);

        /// <summary>
        /// 验证图片验证码
        /// </summary>
        /// <param name="verifyCodeInput"></param>
        /// <returns></returns>
        Task<bool> Verify(ImageVerifyCodeCheckInput verifyCodeInput);
    }
}
