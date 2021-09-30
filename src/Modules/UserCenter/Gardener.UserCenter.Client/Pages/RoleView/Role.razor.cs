// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.RoleView
{
    public partial class Role : TableBase<RoleDto,int, RoleEdit>
    {
       
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(int id)
        {
            var result = await drawerService.CreateDialogAsync<RoleResourceEdit, DrawerInput<int>, bool>(DrawerInput<int>.IsEdit(id), true, title: "分配资源", width: 600);
            Console.WriteLine(result);
        }
       

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            var result = await drawerService.CreateDialogAsync<RoleResourceDownload, string, bool>(
                      string.Empty,
                       true,
                       title: "种子数据",
                       width: 1300,
                       placement: "right");
        }

    }
}
