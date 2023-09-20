// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.UserCenter.Resources;
using System;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.TenantView
{
    public partial class Tenant: ListOperateTableBase<SystemTenantDto, Guid, TenantEdit, UserCenterResource>
    {
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditTenantResourceClick(SystemTenantDto tenant)
        {
            await OpenOperationDialogAsync<TenantResourceEdit, SystemTenantDto, bool>(Localizer[nameof(SharedLocalResource.BindingResource)], tenant, width: 600);
        }

    }
}
