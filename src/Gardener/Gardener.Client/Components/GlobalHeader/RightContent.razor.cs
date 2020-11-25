using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign.Pro.Layout;
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

        private NoticeIconData[] _notifications = { };
        private NoticeIconData[] _messages = { };
        private NoticeIconData[] _events = { };
        private int _count = 0;

        private List<AutoCompleteDataItem<string>> DefaultOptions { get; set; } = new List<AutoCompleteDataItem<string>>
        {
            new AutoCompleteDataItem<string>
            {
                Label = "umi ui",
                Value = "umi ui"
            },
            new AutoCompleteDataItem<string>
            {
                Label = "Pro Table",
                Value = "Pro Table"
            },
            new AutoCompleteDataItem<string>
            {
                Label = "Pro Layout",
                Value = "Pro Layout"
            }
        };

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

            _notifications = new NoticeIconData[] {

                new NoticeIconData(){
                Avatar="",
                Description="_notifications",
                Title="_notifications"

                }
            };
            _messages = new NoticeIconData[] {

                new NoticeIconData(){
                Avatar="",
                Description="_messages",
                Title="_messages"

                }
            }; 
            _events = new NoticeIconData[] {

                new NoticeIconData(){
                Avatar="",
                Description="_events",
                Title="_events"
                }
            };
            _count = 8;
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

        public async Task HandleClear(string key)
        {
            switch (key)
            {
                case "notification":
                    _notifications = new NoticeIconData[] { };
                    break;
                case "message":
                    _messages = new NoticeIconData[] { };
                    break;
                case "event":
                    _events = new NoticeIconData[] { };
                    break;
            }
            await MessageService.Success($"清空了{key}");
        }

        public async Task HandleViewMore(string key)
        {
            await MessageService.Info("Click on view more");
        }
    }
}