// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Authorization.Dtos;
using Gardener.Client.Base;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.LoginView
{
    public partial class Login
    {
        bool loading = false;
        bool autoLogin = true;
        private LoginInput loginInput = new LoginInput();
        [Inject]
        public MessageService MsgSvr { get; set; }
        [Inject]
        public IAccountService accountService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IAuthenticationStateManager authenticationStateManager { get; set; }

        private Gardener.Client.Base.Components.ImageVerifyCode _imageVerifyCode;

        private string returnUrl;

        protected override  async Task OnInitializedAsync()
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
            var user =  await authenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                Navigation.NavigateTo(returnUrl ?? "/");
            }
           
        }
        
        private async Task OnLogin()
        {
            loading = true;
            var loginOutResult= await accountService.Login(loginInput);
            if (loginOutResult!=null)
            {
                await MsgSvr.Success($"登录成功",0.8);
                await authenticationStateManager.Login(loginOutResult, autoLogin);
                loading = false;
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            else {
                await _imageVerifyCode.ReLoadVerifyCode();
                loading = false;
                MsgSvr.Error($"登录失败");
                //await InvokeAsync(StateHasChanged);
            }
            
        }
    }
    
}
