// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Components
{
    public partial class ImageVerifyCode
    {
        [Inject]
        private IVerifyCodeService verifyCodeService { get; set; }
        private string verifyCodeImage = "";

        [Parameter]
        public string Style { get; set; } = "height:38px;margin-left:2px;";
        [Parameter]
        public string Key { get; set; }
        [Parameter]
        public EventCallback<string> KeyChanged { get; set; }

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
            ImageVerifyCodeDto imageVerifyCode = await verifyCodeService.GetImageVerifyCode(Enums.CodeCharacterTypeEnum.NumberAndCharacter);
            if (imageVerifyCode != null)
            {
                Key = imageVerifyCode.Key;
                verifyCodeImage = "data:image/gif;base64," + imageVerifyCode.Base64Image;
            }
            isLoading = false;
        }
    }
}
