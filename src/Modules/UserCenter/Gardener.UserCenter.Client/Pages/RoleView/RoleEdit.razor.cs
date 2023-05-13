// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.RoleView
{
    public partial class RoleEdit : EditOperationDialogBase<RoleDto, int, UserCenterResource>
    {
        [Inject]
        private ITenantService tenantService { get; set; } = null!;
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;

        /// <summary>
        /// 租户列表
        /// </summary>
        private IEnumerable<TenantDto>? _tenants;

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            StartLoading();
            var task1= LoadEditModelData();
            bool isTenant =  authenticationStateManager.CurrentUserIsTenant();
            if (!isTenant)
            {
                _tenants = await tenantService.GetAllUsable();
            }
            await task1;
            StopLoading();
        }
    }
}
