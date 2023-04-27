// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.ProLayout;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.EventBus.Events;
using Gardener.Client.Base.Services;
using Gardener.EventBus;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;


namespace Gardener.Client.AntDesignUi.Base.Shared
{
    public partial class MainLayout
    {

        private MenuDataItem[] _menuData { get; set; } = { };

        /// <summary>
        /// 菜单栏收缩
        /// </summary>
        private bool collapsed;
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService systemConfigService { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IJsTool JsTool { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IClientLocalizer<MenuNameLocalResource> Loc { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private IEventBus eventBus { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        private SystemConfig systemConfig = null!;
        /// <summary>
        /// 
        /// </summary>
        private List<MenuDataItem> menuDataItems = new List<MenuDataItem>();
        /// <summary>
        /// 
        /// </summary>
        private ReuseTabs reuseTabs = null!;
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <param name="parent"></param>
        private void InitEnum(ResourceDto resourceDto, MenuDataItem? parent = null)
        {
            string key = "menu:" + resourceDto.Key;
            string localName = Loc[key];
            if (localName.Equals(key))
            {
                //未配置菜单本地化使用名称
                localName = resourceDto.Name;
            }

            string? path = resourceDto.Path;
            //path为空，还没有子级的会报错，设置个key
            if (string.IsNullOrEmpty(path) && (resourceDto.Children == null || !resourceDto.Children.Any()))
            {
                path = resourceDto.Key;
            }
            var current = new MenuDataItem
            {
                Path = path,
                Name = localName,
                Key = resourceDto.Key,
                Icon = resourceDto.Icon ?? "",
            };
            ClientMenuCache.Add(current);
            if (parent == null)
            {
                menuDataItems.Add(current);
            }
            else
            {
                var tempList = parent.Children ?? new MenuDataItem[] { };
                var temp = tempList.ToList();
                temp.Add(current);
                parent.Children = temp.ToArray();
            }
            if (resourceDto.Children != null)
            {
                foreach (var c in resourceDto.Children)
                {
                    InitEnum(c, current);
                }
            }
        }

        protected override Task OnInitializedAsync()
        {
            systemConfig = systemConfigService.GetSystemConfig();
            
            var currentMenus = authenticationStateManager.GetCurrentUserMenus();
            if (currentMenus != null)
            {
                //已有数据
                menuDataItems = new List<MenuDataItem>();
                currentMenus.ForEach(x => InitEnum(x));
                _menuData = menuDataItems.ToArray();
            }
            else
            {
                //设置个回调
                eventBus.Subscribe<ReloadCurrentUserEvent>(e => {
                    var menus = e.MenuResources;
                    if (menus != null)
                    {
                        menuDataItems = new List<MenuDataItem>();
                        menus.ForEach(x => InitEnum(x));
                        _menuData = menuDataItems.ToArray();
                    }

                    return Task.CompletedTask;
                });
            }
            ClientNavTabControl.SetReuseTabs(reuseTabs);

            var task1= JsTool.Document.SetTitle(systemConfig.SystemName);
            //导航控制
            var task2= base.OnInitializedAsync();

            return Task.WhenAll(task1, task2);
        }

        void toggle()
        {
            collapsed = !collapsed;
        }
        void OnCollapse(bool isCollapsed)
        {
            Console.WriteLine($"Collapsed: {isCollapsed}");
        }

    }
}
