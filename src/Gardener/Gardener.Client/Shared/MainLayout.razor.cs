// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.Pro.Layout;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Client.Models;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Shared
{
    public partial class MainLayout
    {

        private MenuDataItem[] _menuData { get; set; } = { };

        /// <summary>
        /// 菜单栏收缩
        /// </summary>
        private bool collapsed;
        private readonly LinkItem[] _links =
        {
                new LinkItem{ Key = "1", BlankTarget = true, Title = "Furion" ,Href="https://gitee.com/monksoul/Furion"},
                new LinkItem{ Key = "2", BlankTarget = true, Title = "Ant Design",Href="https://github.com/ant-design-blazor/ant-design-blazor"},
                new LinkItem{ Key = "3", BlankTarget = true, Title = "Ant Design Pro",Href="https://github.com/ant-design-blazor/ant-design-pro-blazor"}
        };
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService systemConfigService { get; set; }
        [Inject]
        private JsTool JsTool { get; set; }
        [Inject]
        private IStringLocalizer<App> Loc { get; set; }
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; }

        private SystemConfig systemConfig;

        private List<MenuDataItem> menuDataItems = new List<MenuDataItem>();
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <param name="parent"></param>
        private void InitEnum(ResourceDto resourceDto, MenuDataItem parent = null)
        {

            string nameLocalizer = Loc[resourceDto.Key];

            string path = resourceDto.Path;
            //path为空，还没有子级的会报错，设置个key
            if (string.IsNullOrEmpty(path) && ( resourceDto.Children == null || !resourceDto.Children.Any()))
            {
                path = resourceDto.Key;
            }
            var current = new MenuDataItem
            {
                Path = path,
                Name = nameLocalizer.Equals(resourceDto.Key)? resourceDto.Name: nameLocalizer,
                Key = resourceDto.Key,
                Icon = resourceDto.Icon ?? "",
            };
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

        protected override async Task OnInitializedAsync()
        {
            Action<List<ResourceDto>> onEmnusLoaded = menus =>
            {
                menus.ForEach(x => InitEnum(x));
                _menuData = menuDataItems.ToArray();
            };
            //拿取已加载的数据
            var emnus = authenticationStateManager.GetCurrentUserEmnus();
            if (emnus.Count > 0)
            {
                //已加载到菜单数据
                onEmnusLoaded(emnus);
            }
            else {
                //可能尚未加载完成，设置个回调
                authenticationStateManager.SetOnMenusLoaded(onEmnusLoaded);
            }
            
            systemConfig = systemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
            await base.OnInitializedAsync();
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
