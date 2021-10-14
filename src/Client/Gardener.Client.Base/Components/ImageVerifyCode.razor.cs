// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using Gardener.ImageVerifyCode.Dtos;
using Gardener.ImageVerifyCode.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
    public partial class ImageVerifyCode
    {
        [Inject]
        private IVerifyCodeService verifyCodeService { get; set; }
        private string verifyCodeImage = "data:image/gif;base64,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";

        [Parameter]
        public string Style { get; set; } = "height:38px;margin-left:2px;";
        [Parameter]
        public CodeCharacterTypeEnum Type { get; set; } = CodeCharacterTypeEnum.Number;
        [Parameter]
        public string VerifyCodeKey { get; set; }
        [Parameter]
        public EventCallback<string> VerifyCodeKeyChanged { get; set; }

        private bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await ReLoadVerifyCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ReLoadVerifyCode()
        {
            isLoading = true;
            //移除上次的验证码
            if (!string.IsNullOrEmpty(VerifyCodeKey))
            {
                await verifyCodeService.RemoveImageVerifyCode(VerifyCodeKey);
            }

            ImageVerifyCodeDto imageVerifyCode = await verifyCodeService.GetImageVerifyCode(Type);
            if (imageVerifyCode != null)
            {
                VerifyCodeKey = imageVerifyCode.Key;
                await VerifyCodeKeyChanged.InvokeAsync(VerifyCodeKey);
                verifyCodeImage = "data:image/gif;base64," + imageVerifyCode.Base64Image;
            }
            isLoading = false;
        }
    }
}
