// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Enums;
using Gardener.VerifyCode.Dtos;
using Gardener.VerifyCode.Services;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Client.Services
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

        public async Task<bool> RemoveImageVerifyCode(string key)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/image-verify-code/{key}");
        }

        public async Task<bool> VerifyImageVerifyCode(VerifyCodeInput verifyCodeInput)
        {
            return await apiCaller.PostAsync<VerifyCodeInput, bool>($"/{controller}/verify-image-verify-code", verifyCodeInput);
        }
    }
}
