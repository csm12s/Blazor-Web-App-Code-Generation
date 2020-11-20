// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------
using AntDesign.Pro.Layout;
using Gardener.Client.Models;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Shared
{
    public partial class MainLayout
    {

        private MenuDataItem[] _menuData = { };
        
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
        private ISystemConfigService SystemConfigService { get; set; }

        private SystemConfig systemConfig;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _menuData = new MenuDataItem[]{

                new MenuDataItem
                {
                    Path="/",
                    Name="首页",
                    Key="home",
                    Icon="home"
                },
                new MenuDataItem
                {
                    Name="用户权限",
                    Key="user_auth",
                    Icon="verified",
                    Children=new MenuDataItem[]
                    {
                        new MenuDataItem(){
                            Name="用户管理",
                            Key="user_auth.user",
                            Icon="user",
                            Path="/user_auth/user"
                        },
                        new MenuDataItem{
                            Name="角色管理",
                            Key="user_auth.role",
                            Icon="control",
                            Path="/user_auth/role"
                        }, new MenuDataItem{
                            Name="资源管理",
                            Key="user_auth.resource",
                            Icon="api",
                            Path="/user_auth/resource"
                        }
                    }
                }
            };
            systemConfig = SystemConfigService.GetSystemConfig();
            //_menuData = await HttpClient.GetFromJsonAsync<MenuDataItem[]>("data/menu.json");
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
