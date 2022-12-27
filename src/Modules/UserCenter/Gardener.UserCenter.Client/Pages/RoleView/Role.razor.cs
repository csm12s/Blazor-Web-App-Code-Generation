// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.UserCenter.Dtos;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.RoleView
{
    public partial class Role : ListOperateTableBase<RoleDto, int, RoleEdit>
    {

        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(int id)
        {
            await OpenOperationDialogAsync<RoleResourceEdit, OperationDialogInput<int>, bool>(localizer["BindingResource"], OperationDialogInput<int>.IsEdit(id), width: 600);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            await OpenOperationDialogAsync<RoleResourceDownload, string, bool>(
                localizer[SharedLocalResource.SeedData],
                      string.Empty,
                       width: 1300);
        }

    }
}
