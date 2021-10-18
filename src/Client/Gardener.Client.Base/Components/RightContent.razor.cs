// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AntDesign;
using System.Globalization;
using Gardener.UserCenter.Dtos;
using AntDesign.ProLayout;
using Gardener.Client.Base.Constants;

namespace Gardener.Client.Base.Components
{
    public partial class RightContent
    {
        private UserDto _currentUser;
        public string[] Locales { get; set; } = { "zh-CN", "en-US" };
        [Inject] 
        protected NavigationManager NavigationManager { get; set; }
        [Inject] 
        protected MessageService MessageService { get; set; }
        [Inject]
        protected IAuthenticationStateManager authenticationStateManager { get; set; }

        [Inject]
        private IJsTool JsTool { get; set; }
        [Inject]
        private IClientLocalizer localizer{ get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetClassMap();
            _currentUser =await authenticationStateManager.GetCurrentUser();
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }
        public AvatarMenuItem[] AvatarMenuItems { get; set; } = new AvatarMenuItem[]
        {
            new() { Key = "center", IconType = "user", Option = LocalizerUtil.GetValue("个人中心")},
            new() { Key = "setting", IconType = "setting", Option =LocalizerUtil.GetValue("个人设置")},
            new() { IsDivider = true },
            new() { Key = "logout", IconType = "logout", Option = LocalizerUtil.GetValue("退出登录")}
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
                    await InvokeAsync(StateHasChanged);
                    break;
            }
        }

        public async Task HandleSelectLang(MenuItem item)
        {
            string name = item.Key;
            if (CultureInfo.CurrentCulture.Name != name)
            {
                CultureInfo.CurrentCulture = new CultureInfo(name);
                await JsTool.SessionStorage.SetAsync(ClientConstant.BlazorCultureKey, name);
                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
            
        }
    }
}