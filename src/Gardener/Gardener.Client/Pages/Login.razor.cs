// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Core.Dtos;
using Gardener.Client.Services;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using Microsoft.AspNetCore.WebUtilities;
using System.Threading.Tasks;

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

        [Inject] public AuthenticationStateProvider AuthProvider { get; set; }

        private string returnUrl;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected async override void OnInitialized()
        {
            var user = (await authenticationStateTask).User;
            if (user.Identity.IsAuthenticated)
            {
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            var query = new Uri(Navigation.Uri).Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out var value))
            {
                returnUrl = value;
            }
        }

        public async void OnLogin()
        {
            loading = true;
            var loginOutResult= await AuthorizeService.Login(loginInput);
            if (loginOutResult.Successed)
            {
                loading = false;
                ((AuthProvider)AuthProvider).MarkUserAsAuthenticated(loginOutResult.Data.AccessToken);
                Navigation.NavigateTo(returnUrl ?? "/");
                await MsgSvr.Success($"登录成功",1);
            }
            else {
                loading = false;
                ((AuthProvider)AuthProvider).MarkUserAsLoggedOut();
                Navigation.NavigateTo("/login"+(string.IsNullOrEmpty(returnUrl)?"": "?returnUrl=" + returnUrl));
                await MsgSvr.Error($"登录失败：{loginOutResult.Errors}",duration:1);
            }
        }
    }
    
}
