// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Components.PageBaseClass
{
    /// <summary>
    /// table列表页面基类(可以被当作OperationDialog打开)-支持多租户
    /// </summary>
    /// <typeparam name="TDto">对象Dto</typeparam>
    /// <typeparam name="TKey">对象的主键</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    public class MultiTenantTableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : TableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> where TDto : class, new()
    {
        /// <summary>
        /// 租户数据
        /// </summary>
        protected Dictionary<Guid, SystemTenantDto> _tenantMap = new Dictionary<Guid, SystemTenantDto>();
        /// <summary>
        /// 租户服务    
        /// </summary>
        [Inject]
        protected ITenantService TenantService { get; set; } = null!;

        /// <summary>
        /// 初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await LoadTenants();
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 加载租户数据
        /// </summary>
        /// <returns></returns>
        protected virtual async Task LoadTenants()
        {
            #region 加载租户数据
            if (this.IsTenantAdministrator() && !_tenantMap.Any())
            {
                List<SystemTenantDto> tenants = await TenantService.GetAll();
                foreach (SystemTenantDto tenant in tenants)
                {
                    _tenantMap.TryAdd(tenant.Id, tenant);
                }
            }
            #endregion
        }

        /// <summary>
        /// 设置TableSearch特定参数
        /// </summary>
        /// <param name="tableSearchSettings"></param>
        /// <param name="tableSearchFilterGroupProviders"></param>
        protected override void SetTableSearchParameters(TableSearchSettings tableSearchSettings, List<Func<List<FilterGroup>?>> tableSearchFilterGroupProviders)
        {

            if (typeof(TDto).IsAssignableTo(typeof(IModelTenantId)))
            {
                if (this.IsTenantAdministrator())
                {
                    //非租户租户编号搜索=》租户
                    tableSearchSettings.FieldDisplayNameConverts.Add(nameof(IModelTenantId.TenantId), (old) => nameof(IModelTenant.Tenant));
                    //非租户租户编号设置下拉数据
                    tableSearchSettings.FieldSelectItemsProviders.Add(nameof(IModelTenantId.TenantId), async field =>
                    {
                        List<SystemTenantDto> tenants = await TenantService.GetAll();
                        return tenants.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
                    });
                }
                else 
                {
                    //不需要租户编号搜索
                    this.AddExcludeSearchFields(nameof(IModelTenantId.TenantId));
                }

            }
            base.SetTableSearchParameters(tableSearchSettings, tableSearchFilterGroupProviders);
        }

        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        protected virtual SystemTenantDto? GetTenant(Guid? tenantId)
        {
            if (tenantId == null || tenantId.Equals(Guid.Empty) || !_tenantMap.ContainsKey(tenantId.Value))
            {
                return null;
            }
            return _tenantMap[tenantId.Value];
        }

        /// <summary>
        /// 是否是租户
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsTenant()
        {
            return AuthenticationStateManager.CurrentUserIsTenant();
        }

        /// <summary>
        /// 是否是租户管理员
        /// </summary>
        /// <remarks>
        /// 是否分配资源 <see cref="Authorization.Constants.ResourceKeys.SystemTenantAdministratorKey"/>
        /// </remarks>
        /// <returns></returns>
        protected virtual bool IsTenantAdministrator()
        {
            return AuthenticationStateManager.CurrentUserIsTenantAdministrator();
        }

    }
    /// <summary>
    /// table列表页面基类(可以被当作OperationDialog打开)-支持多租户
    /// </summary>
    /// <typeparam name="TDto">对象Dto</typeparam>
    /// <typeparam name="TKey">对象的主键</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <remarks>
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class MultiTenantTableBase<TDto, TKey, TLocalResource> : MultiTenantTableBase<TDto, TKey, TLocalResource, TKey, bool>
        where TDto : class, new()
    {
    }

}
