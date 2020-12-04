// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Client.Services;
using Gardener.Client.Constants;
using System.Globalization;

namespace Gardener.Client.Components
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
        private JsTool JsTool { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetClassMap();
            _currentUser = authenticationStateManager.GetCurrentUser();
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }

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
                await JsTool.SessionStorage.SetAsync(SystemConstant.BlazorCultureKey, name);
                NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
            }
            
        }
    }
}