// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Core.Dtos;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using Microsoft.AspNetCore.WebUtilities;

namespace Gardener.Client.Pages
{
    public partial class Login
    {
        bool loading = false;

        private LoginInput loginInput = new LoginInput();
        [Inject]
        public MessageService MsgSvr { get; set; }
        [Inject]
        public IAuthorizeService AuthorizeService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject] 
        public IAuthenticationStateManager authenticationStateManager { get; set; }

        private string returnUrl;

        protected override void OnInitialized()
        {
            var user =  authenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            var url = new Uri(Navigation.Uri);
            var query = url.Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out var value))
            {
                if (!value.Equals(Navigation.Uri))
                {
                    returnUrl = value;
                }
            }
        }
        public async void OnLogin()
        {
            loading = true;
            var loginOutResult= await AuthorizeService.Login(loginInput);
            if (loginOutResult.Successed)
            {
                await MsgSvr.Success($"登录成功", 0.5);
                await authenticationStateManager.Login(loginOutResult.Data.AccessToken);
                loading = false;
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            else {
                loading = false;
                await MsgSvr.Error($"登录失败：{loginOutResult.Errors}",duration:0.5);
                await InvokeAsync(StateHasChanged);
            }
        }
    }
    
}
