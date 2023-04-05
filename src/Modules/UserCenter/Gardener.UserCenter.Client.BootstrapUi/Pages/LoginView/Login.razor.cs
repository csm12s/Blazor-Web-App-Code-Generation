using BootstrapBlazor.Components;
using Gardener.Authorization.Dtos;
using Gardener.Client.Base;
using Gardener.Enums;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Gardener.VerifyCode.Dtos;
using Gardener.VerifyCode.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.BootstrapUi.Pages.LoginView
{
    public partial class Login
    {
        [Inject]
        private IClientLocalizer<UserCenterResource> localizer { get; set; } = null!;

        [Inject]
        private IImageVerifyCodeService imageVerifyCodeService { get; set; } = null!;

        [Inject]
        private IAccountService accountService { get; set; } = null!;

        [Inject]
        public IAuthenticationStateManager authenticationStateManager { get; set; } = null!;


        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        [Inject]
        [NotNull]
        private MessageService MessageService { get; set; } = null!;

        private string? returnUrl;

        private LoginInput loginInput = new LoginInput();

        private string? imageVerifyCode;

        protected override async Task OnInitializedAsync()
        {
            var url = new Uri(Navigation.Uri);
            var query = url.Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out var value))
            {
                if (!value.Equals(Navigation.Uri))
                {
                    returnUrl = value;
                }
            }
            //已登录
            //var user = await authenticationStateManager.GetCurrentUser();
            //if (user != null)
            //{
            //    Navigation.NavigateTo(returnUrl ?? "/");
            //}

            var result = await imageVerifyCodeService.Create(new VerifyCode.Dtos.ImageVerifyCodeInput()
            {
                FontSize = 12,
                CreateCodeParam = new CharacterCodeCreateParam
                {
                    Type = CodeCharacterTypeEnum.Number,
                    CharacterCount = 5
                }
            });
            if (result != null)
            {
                imageVerifyCode = "data:image/gif;base64," + result.Base64Image;
                loginInput.VerifyCodeKey = result.Key;
            }
            await base.OnInitializedAsync();
        }

        private async Task OnLogin()
        {

            var loginOutResult = await accountService.Login(loginInput);

            if (loginOutResult != null)
            {
                await authenticationStateManager.Login(loginOutResult, true);
                Navigation.NavigateTo(returnUrl ?? "/");
                await MessageService.Show(new MessageOption()
                {
                    Content = localizer.Combination(UserCenterResource.Login,UserCenterResource.Success),
                    Color= Color.Success
                });
            }
            else
            {
                await MessageService.Show(new MessageOption()
                {
                    Content = localizer.Combination(UserCenterResource.Login, UserCenterResource.Fail),
                    Color = Color.Warning
                });
            }

        }
    }
}
