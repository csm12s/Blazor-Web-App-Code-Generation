// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.RoleView
{
    public partial class Role : ListOperateTableBase<RoleDto, int, RoleEdit, UserCenterResource>
    {
        [Inject]
        public IRoleService roleService { get; set; } = null!;
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(RoleDto role)
        {
            await OpenOperationDialogAsync<RoleResourceEdit, RoleDto, bool>(Localizer["BindingResource"], role, width: 600);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            Task<string> seedData = roleService.GetRoleResourceSeedData();
            await OpenOperationDialogAsync<ShowSeedDataCode, Task<string>, bool>(Localizer[SharedLocalResourceKeys.SeedData], seedData, width: 1300);
        }
    }
}
