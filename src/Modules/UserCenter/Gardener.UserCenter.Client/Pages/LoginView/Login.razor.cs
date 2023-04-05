// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Authorization.Dtos;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.LoginView
{
    public partial class Login
    {
        private bool loading = false;
        private bool autoLogin = true;
        private LoginInput loginInput = new LoginInput();
        private Gardener.Client.AntDesignUi.Base.Components.ImageVerifyCode? _imageVerifyCode;
        private string? returnUrl;

        [Inject]
        private IClientMessageService MessageService { get; set; } = null!;
        [Inject]
        private IAccountService AccountService { get; set; } = null!;
        [Inject]
        private NavigationManager Navigation { get; set; } = null!;
        [Inject]
        private IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        [Inject]
        private IClientLocalizer<UserCenterResource> Localizer { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override  async Task OnInitializedAsync()
        {
            var url = new Uri(Navigation.Uri);
            var query = url.Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out StringValues value))
            {
                if (!value.Equals(Navigation.Uri))
                {
                    returnUrl = value;
                }
            }
            //已登录
            var user =  await AuthenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                Navigation.NavigateTo(returnUrl ?? "/");
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnLogin()
        {
            loading = true;
            var loginResult= await AccountService.Login(loginInput);
            if (loginResult!=null)
            {
                await MessageService.SuccessAsync(Localizer.Combination(UserCenterResource.Login,UserCenterResource.Success),0.8);
                await AuthenticationStateManager.Login(loginResult, autoLogin);
                loading = false;
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            else 
            {
                loading = false;
                MessageService.Error(Localizer.Combination(UserCenterResource.Login, UserCenterResource.Fail));
                if (_imageVerifyCode != null)
                {
                    await _imageVerifyCode.ReLoadVerifyCode();
                }
                //await InvokeAsync(StateHasChanged);
            }
            
        }
    }
    
}
