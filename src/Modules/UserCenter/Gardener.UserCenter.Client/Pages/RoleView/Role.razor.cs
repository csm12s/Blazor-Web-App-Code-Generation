﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Constants;
using Gardener.Base;
using Gardener.Base.Resources;
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
    public partial class Role : ListOperateTableBase<RoleDto, int, RoleEdit, UserCenterResource>
    {
        [Inject]
        public IRoleService roleService { get; set; } = null!;
        /// <summary>
        /// 排除搜索字段
        /// </summary>
        private List<string>? excludeSeatchFields;
        /// <summary>
        /// 点击分配资源
        /// </summary>
        /// <returns></returns>
        private async Task OnEditRoleResourceClick(int id)
        {
            await OpenOperationDialogAsync<RoleResourceEdit, OperationDialogInput<int>, bool>(Localizer["BindingResource"], OperationDialogInput<int>.Edit(id), width: 600);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            Task<string> seedData = roleService.GetRoleResourceSeedData();
            await OpenOperationDialogAsync<ShowSeedDataCode, Task<string>, bool>(Localizer[SharedLocalResource.SeedData], seedData, width: 1300);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Task OnParametersSetAsync()
        {
            bool admin = AuthenticationStateManager.IsTenantAdministrator();
            if(!admin)
            {
                excludeSeatchFields = new List<string>() { nameof(IModelTenant.TenantId) };
            }
            return base.OnParametersSetAsync();
        }
    }
}
