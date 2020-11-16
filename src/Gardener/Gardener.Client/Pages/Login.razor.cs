// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Core.Dtos;
using Gardener.Client.Apis;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using Microsoft.AspNetCore.WebUtilities;

namespace Gardener.Client.Pages
{
    public partial class Login
    {
        private bool isLoading = false;

        public void Loading()
        {
            isLoading = true;
        }

        public void UnLoading()
        {
            isLoading = false;
        }

        public void SwitchLoading()
        {
            isLoading = !isLoading;
        }

        private LoginInput loginInput = new LoginInput();
        [Inject]
        public MessageService MsgSvr { get; set; }
        [Inject]
        public IAuthorizeService AuthorizeService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject] public AuthenticationStateProvider AuthProvider { get; set; }

        private string returnUrl;

        protected override void OnInitialized()
        {
            var query = new Uri(Navigation.Uri).Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out var value))
            {
                returnUrl = value;
            }
        }

        public async void OnLogin()
        {
            SwitchLoading();
            var loginOutResult= await AuthorizeService.Login(loginInput);
            SwitchLoading();
            if (loginOutResult.Successed)
            {
                MsgSvr.Success($"登录成功");
                ((AuthProvider)AuthProvider).MarkUserAsAuthenticated(loginOutResult.Data.AccessToken);
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            else {
                MsgSvr.Error($"登录失败：{loginOutResult.Errors}");
            }
        }
        public void OnCancel()
        {
            SwitchLoading();
        }
    }
    
}
