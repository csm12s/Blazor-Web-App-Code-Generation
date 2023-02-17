using BootstrapBlazor.Components;
using Gardener.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Diagnostics.CodeAnalysis;

namespace Gardener.Client.BootstrapUi.Base.Shared
{
    public partial class MainLayout
    {
        [Inject]
        [NotNull]
        private ClientModuleContext moduleContext { get; set; }
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
        {
            new MenuItem() { Text = "Index", Icon = "fa-solid fa-fw fa-flag", Url = "/" , Match = NavLinkMatch.All},
            new MenuItem() { Text = "测试", Icon = "fa-solid fa-fw fa-users", Url = "/test" },
            new MenuItem() { Text = "登录", Icon = "fa-solid fa-fw fa-users", Url = "/login" }
        };

            return menus;
        }
    }
}
