// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.RoleView
{
    public partial class Role : ListOperateTableBase<RoleDto, int, RoleEdit>
    {
        [Inject]
        public IRoleService roleService { get; set; } = null!;
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(int id)
        {
            await OpenOperationDialogAsync<RoleResourceEdit, OperationDialogInput<int>, bool>(Localizer["BindingResource"], OperationDialogInput<int>.IsEdit(id), width: 600);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            string seedData = await roleService.GetRoleResourceSeedData();
            await OpenOperationDialogAsync<ShowSeedDataCode, string, bool>(Localizer[SharedLocalResource.SeedData], seedData, width: 1300);
        }

    }
}
