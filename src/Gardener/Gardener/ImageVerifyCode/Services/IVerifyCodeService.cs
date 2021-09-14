// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using Gardener.ImageVerifyCode.Dtos;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode.Services
{
    public interface IVerifyCodeService
    {
        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <returns></returns>
        public Task<ImageVerifyCodeDto> GetImageVerifyCode(CodeCharacterTypeEnum codeType);
    }
}
