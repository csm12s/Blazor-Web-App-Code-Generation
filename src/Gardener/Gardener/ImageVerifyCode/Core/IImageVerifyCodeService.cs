// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using Gardener.ImageVerifyCode.Dtos;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode.Core
{
    /// <summary>
    /// 校验码服务接口
    /// </summary>
    public interface IImageVerifyCodeService
    {
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="codeType">验证码类型</param>
        /// <returns></returns>
        public Task<ImageVerifyCodeInfo> CreateFromType(CodeCharacterTypeEnum codeType = CodeCharacterTypeEnum.NumberAndCharacter);
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<ImageVerifyCodeInfo> Create(ImageVerifyCodeCreateParam param = null);

        /// <summary>
        /// 验证校验码
        /// </summary>
        /// <returns></returns>
        Task<bool> Verify(string key, string code);
    }
}
