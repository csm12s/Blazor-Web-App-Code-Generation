// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using AntDesign;
using Gardener.UserCenter.Dtos;
using AntDesign.ProLayout;
using Gardener.Client.Base.Services;
using Gardener.Client.Base;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    public partial class RightContent
    {
        private UserDto _currentUser;
        private string[] _locales;
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected MessageService MessageService { get; set; }
        [Inject]
        protected IAuthenticationStateManager authenticationStateManager { get; set; }
        [Inject]
        private IClientLocalizer localizer { get; set; }
        [Inject]
        private IClientCultureService clientCultureService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SetClassMap();
            _currentUser = await authenticationStateManager.GetCurrentUser();
            _locales= clientCultureService.GetSupportedCultures();
            await base.OnInitializedAsync();
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }
        public AvatarMenuItem[] AvatarMenuItems { get; set; } = new AvatarMenuItem[]
        {
            new() { Key = "center", IconType = "user", Option = LocalizerUtil.GetValue("UserCenter")},
            new() { Key = "setting", IconType = "setting", Option =LocalizerUtil.GetValue("PersonalSettings")},
            new() { IsDivider = true },
            new() { Key = "logout", IconType = "logout", Option = LocalizerUtil.GetValue("Logout")}
        };
        public async Task HandleSelectUser(MenuItem item)
        {
            switch (item.Key)
            {
                case "center":
                    NavigationManager.NavigateTo("/account/center");
                    break;
                case "setting":
                    NavigationManager.NavigateTo("/account/settings");
                    break;
                case "logout":
                    await authenticationStateManager.Logout();
                    //NavigationManager.NavigateTo("/auth/login?returnUrl="+ Uri.EscapeDataString(NavigationManager.Uri));
                    //await InvokeAsync(StateHasChanged);
                    //移除所有tab
                    ClientNavTabControl.RemoveAllNavTabPage();
                    break;
            }
        }
        /// <summary>
        /// HandleSelectLang
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task HandleSelectLang(MenuItem item)
        {
            string name = item.Key;
            if (await clientCultureService.SetCulture(name))
            {
                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
        }
    }
}