// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign.Pro.Layout;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Gardener.Client.Shared
{
    public partial class MainLayout
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
        private MenuDataItem[] _menuData = { };

        /// <summary>
        /// 菜单栏收缩
        /// </summary>
        private bool collapsed;
        /// <summary>
        /// copyright
        /// </summary>
        private string copyright;

        void toggle()
        {
            collapsed = !collapsed;
        }
        void OnCollapse(bool isCollapsed)
        {
            Console.WriteLine($"Collapsed: {isCollapsed}");
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            copyright = SystemConfigService.GetCopyright();
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

            //_menuData = await HttpClient.GetFromJsonAsync<MenuDataItem[]>("data/menu.json");
        }

    }
}
