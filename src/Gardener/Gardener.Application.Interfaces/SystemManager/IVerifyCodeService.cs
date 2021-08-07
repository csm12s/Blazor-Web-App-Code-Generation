// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Enums;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
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
