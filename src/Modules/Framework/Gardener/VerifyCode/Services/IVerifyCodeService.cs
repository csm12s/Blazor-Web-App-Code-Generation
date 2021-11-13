// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using Gardener.VerifyCode.Dtos;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Services
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    public interface IVerifyCodeService
    {
        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <returns></returns>
        public Task<ImageVerifyCodeDto> GetImageVerifyCode(CodeCharacterTypeEnum codeType);

        /// <summary>
        /// 移除图片验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> RemoveImageVerifyCode(string key);

        /// <summary>
        /// 验证图片验证码
        /// </summary>
        /// <param name="verifyCodeInput"></param>
        /// <returns></returns>
        Task<bool> VerifyImageVerifyCode(VerifyCodeInput verifyCodeInput);

    }
}
