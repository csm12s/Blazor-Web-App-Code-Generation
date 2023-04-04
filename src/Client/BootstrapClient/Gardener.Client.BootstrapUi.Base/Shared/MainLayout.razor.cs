using BootstrapBlazor.Components;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.BootstrapUi.Base.Shared
{
    public partial class MainLayout
    {

        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }
        private List<MenuItem> menuDataItems = new List<MenuItem>();
        [Inject]
        [NotNull]
        private ClientModuleContext moduleContext { get; set; } = null!;

        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService systemConfigService { get; set; } = null!;
        [Inject]
        private IJsTool JsTool { get; set; } = null!;
        [Inject]
        private IClientLocalizer<MenuNameLocalResource> Loc { get; set; } = null!;
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;

        private UserDto? user { get; set; }

        private string LogoutUrl = "/auth/logout";

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        protected async override Task OnInitializedAsync()
        {
            List<ResourceDto>? menus = authenticationStateManager.GetCurrentUserMenus();
            var user = await authenticationStateManager.GetCurrentUser();
            if (menus != null && user!=null)
            {
                //已加载到菜单数据
                this.Menus = InitMenus(menus);
                //用户数据
                this.user = user;

            }
            else 
            {
                //暂未加载
                Action<UserDto, bool, List<ResourceDto>, List<string>> onAuthenticationRefreshSuccessed = (user, isSuperAdmin, menus, uiResourceKeys) =>
                {
                    if (menus != null)
                    {
                        //已加载到菜单数据
                        this.Menus = InitMenus(menus);
                    }
                    if (user != null)
                    {
                        this.user = user;
                    }
                };
                //设置个回调
                authenticationStateManager.SetOnAuthenticationRefreshSuccessed(onAuthenticationRefreshSuccessed);
            }
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="menus"></param>
        private List<MenuItem> InitMenus(ICollection<ResourceDto> resources)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            if (resources != null && resources.Count > 0)
            {
                foreach (var resource in resources)
                {
                    var menu = new MenuItem();
                    menu.Text = resource.Name;
                    menu.Url = resource.Path;

                    menu.Items = resource.Children == null ? new() : InitMenus(resource.Children);
                    menuItems.Add(menu);
                }

            }
            return menuItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private Task<bool> OnAuthorizing(string url)
        {
            return Task.FromResult(true);
        }
    }
}
