// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using Microsoft.AspNetCore.WebUtilities;
using System.Threading.Tasks;
using Gardener.Application.Interfaces;

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

        protected override  async Task OnInitializedAsync()
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
        private async Task OnLogin()
        {
            loading = true;
            var loginOutResult= await AuthorizeService.Login(loginInput);
            if (loginOutResult!=null)
            {
                await MsgSvr.Success($"登录成功",0.8);
                await authenticationStateManager.Login(loginOutResult);
                loading = false;
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            else {
                loading = false;
                MsgSvr.Error($"登录失败");
                //await InvokeAsync(StateHasChanged);
            }
        }
    }
    
}
