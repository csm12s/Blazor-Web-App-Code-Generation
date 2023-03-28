// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.VerifyCode.Dtos;
using Gardener.VerifyCode.Services;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Client.Services
{
    [ScopedService]
    public class ImageVerifyCodeService : IImageVerifyCodeService
    {
        private readonly static string controller = "image-verify-code";
        private readonly IApiCaller apiCaller;
        public ImageVerifyCodeService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<ImageVerifyCodeOutput> Create(ImageVerifyCodeInput input)
        {
            return await apiCaller.PostAsync<VerifyCodeInput, ImageVerifyCodeOutput>($"{controller}", input);
        }

        public async Task<bool> Remove(string key)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{key}");
        }

        public async Task<bool> Verify(ImageVerifyCodeCheckInput verifyCodeInput)
        {
            return await apiCaller.PostAsync<ImageVerifyCodeCheckInput, bool>($"{controller}/verify", verifyCodeInput);
        }
    }
}
