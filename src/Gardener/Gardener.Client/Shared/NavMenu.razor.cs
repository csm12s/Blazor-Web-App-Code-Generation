using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace Gardener.Client.Shared
{
    public partial class NavMenu
    {
        [Parameter]
        public bool Collapsed { get; set; }

        // 所有菜单key
        string[] rootSubmenuKeys = { "user_auth", "system_manager" };
        //当前打开的key
        string[] openKeys = { "user_auth" };
        /// <summary>
        /// 当菜单打开
        /// </summary>
        /// <param name="openKeys"></param>
        void onOpenChange(string[] openKeys)
        {
            //当菜单打开时，关闭其他菜单
            var latestOpenKey = openKeys.FirstOrDefault(key => !this.openKeys.Contains(key));
            if (!rootSubmenuKeys.Contains(latestOpenKey))
            {
                this.openKeys = openKeys;
            }
            else
            {
                this.openKeys = !string.IsNullOrEmpty(latestOpenKey) ? new[] { latestOpenKey } : Array.Empty<string>();
            }
        }

    }
}
