// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Authorization.Dtos;
using Gardener.Client.Base;
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
        bool loading = false;
        bool autoLogin = true;
        private LoginInput loginInput = new LoginInput();
        private Gardener.Client.AntDesignUi.Base.Components.ImageVerifyCode? _imageVerifyCode;
        private string? returnUrl;

        [Inject]
        public MessageService MsgSvr { get; set; } = null!;
        [Inject]
        public IAccountService accountService { get; set; } = null!;
        [Inject]
        public NavigationManager Navigation { get; set; } = null!;
        [Inject]
        public IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        [Inject]
        public IClientLocalizer<UserCenterResource> localizer { get; set; } = null!;

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
            var user =  await authenticationStateManager.GetCurrentUser();
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
            var loginOutResult= await accountService.Login(loginInput);
            if (loginOutResult!=null)
            {
                await MsgSvr.Success(localizer.Combination(UserCenterResource.Login,UserCenterResource.Success),0.8);
                await authenticationStateManager.Login(loginOutResult, autoLogin);
                loading = false;
                Navigation.NavigateTo(returnUrl ?? "/");
            }
            else if(_imageVerifyCode!=null)
            {
                await _imageVerifyCode.ReLoadVerifyCode();
                loading = false;
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                MsgSvr.Error(localizer.Combination(UserCenterResource.Login, UserCenterResource.Fail));
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                              //await InvokeAsync(StateHasChanged);
            }
            
        }
    }
    
}
