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
        private ClientModuleContext moduleContext { get; set; }

        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService systemConfigService { get; set; }
        [Inject]
        private IJsTool JsTool { get; set; }
        [Inject]
        private IClientLocalizer<MenuNameLocalResource> Loc { get; set; }
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; }

        private SystemConfig systemConfig;

        private UserDto? user { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        protected async override Task OnInitializedAsync()
        {
            Action<List<ResourceDto>> onEmnusLoaded = menus =>
            {
                if (menus == null) return;
                
            };
            //拿取已加载的数据
            var emnus = authenticationStateManager.GetCurrentUserEmnus();
            if (emnus.Count > 0)
            {
                //已加载到菜单数据
                onEmnusLoaded(emnus);
            }
            else
            {
                //可能尚未加载完成，设置个回调
                authenticationStateManager.SetOnAuthenticationRefreshSuccessed(onEmnusLoaded);
            }

            user = await authenticationStateManager.GetCurrentUser();


            systemConfig = systemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);


            base.OnInitialized();

            
        }
    }
}
