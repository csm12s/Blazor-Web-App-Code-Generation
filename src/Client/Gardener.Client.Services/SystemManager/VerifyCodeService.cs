// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Client.Core;
using Gardener.Enums;
using System.Threading.Tasks;

namespace Gardener.Client.Services.SystemManager
{
    [ScopedService]
    public class VerifyCodeService : IVerifyCodeService
    {
        private readonly static string controller = "verify-code";
        private readonly IApiCaller apiCaller;

        public VerifyCodeService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<ImageVerifyCodeDto> GetImageVerifyCode(CodeCharacterTypeEnum codeType)
        {
            return await apiCaller.GetAsync<ImageVerifyCodeDto>($"{controller}/image-verify-code/{codeType}");
        }
    }
}
