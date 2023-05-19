// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// 树形table基类-支持多租户相关功能
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    /// <typeparam name="TDialogInput"></typeparam>
    /// <typeparam name="TDialogOutput"></typeparam>
    /// <typeparam name="TLocalResource"></typeparam>
    public abstract class MultiTenantTreeTableBase<TDto, TKey, TOperationDialog, TDialogInput, TDialogOutput, TLocalResource> : TreeTableBase<TDto, TKey, TOperationDialog, TDialogInput, TDialogOutput, TLocalResource>
        where TOperationDialog : OperationDialogBase<TDialogInput, TDialogOutput, TLocalResource>
        where TDto : class, new()
        where TKey : notnull
        where TLocalResource : SharedLocalResource
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
            #region 加载租户数据
            if (!this.IsTenant())
            {
                List<SystemTenantDto> tenants = await TenantService.GetAll();
                foreach (SystemTenantDto tenant in tenants)
                {
                    _tenantMap.TryAdd(tenant.Id, tenant);
                }
            }
            #endregion

            await base.OnInitializedAsync();
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
                if (AuthenticationStateManager.CurrentUserIsTenant())
                {
                    //租户不需要租户编号搜索
                    this.AddExcludeSearchFields(nameof(IModelTenantId.TenantId));
                }
                else
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
            bool isTenant = AuthenticationStateManager.CurrentUserIsTenant();
            return isTenant;
        }

        /// <summary>
        /// 列表接口返回后，对页面列表数据进行处理
        /// </summary>
        /// <param name="datas"></param>
        protected override void PageListDataHadnle(IEnumerable<TDto> datas)
        {
            if (!datas.Any()) return;
            if (typeof(TDto).IsAssignableTo(typeof(IModelTenant)))
            {
                foreach (TDto dto in datas)
                {
                    RecursionTree(dto, item =>
                    {
                        if (item is IModelTenant modelTenant)
                        {
                            System.Console.WriteLine(GetKey(item));
                            modelTenant.Tenant = GetTenant(modelTenant.TenantId);
                            System.Console.WriteLine((modelTenant.TenantId ?? Guid.Empty) + "_" + (modelTenant.Tenant?.Name ?? ""));

                        }

                    });
                }
            }
        }
    }
    /// <summary>
    /// 树形table基类-支持多租户相关功能
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    /// <typeparam name="TDialogInput"></typeparam>
    /// <typeparam name="TDialogOutput"></typeparam>
    public abstract class MultiTenantTreeTableBase<TDto, TKey, TOperationDialog, TDialogInput, TDialogOutput> : MultiTenantTreeTableBase<TDto, TKey, TOperationDialog, TDialogInput, TDialogOutput, SharedLocalResource>
        where TOperationDialog : OperationDialogBase<TDialogInput, TDialogOutput, SharedLocalResource>
        where TDto : class, new()
        where TKey : notnull
    {

    }

    /// <summary>
    /// 树形table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    /// <typeparam name="TLocalResource"></typeparam>
    public abstract class MultiTenantTreeTableBase<TDto, TKey, TOperationDialog, TLocalResource> : MultiTenantTreeTableBase<TDto, TKey, TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TDto : class, new() where TKey : notnull where TLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 根据<TDto>获取查看时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetSelectOption(TDto dto)
        {
            return OperationDialogInput<TKey>.Select(GetKey(dto));
        }
        /// <summary>
        /// 根据<TDto>获取编辑时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetEditOption(TDto dto)
        {
            return OperationDialogInput<TKey>.Edit(GetKey(dto));
        }
        /// <summary>
        /// 根据<TDto>获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetAddOption(TDto dto)
        {
            return OperationDialogInput<TKey>.Add(GetKey(dto));
        }
        // <summary>
        /// 获取添加时传入抽屉的数据项<TEditOption>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override OperationDialogInput<TKey> GetAddOption()
        {
            return OperationDialogInput<TKey>.Add();
        }
        /// <summary>
        /// 当编辑结束
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dialogOutput"></param>
        /// <returns></returns>
        protected override async Task OnEditFinish(TDto dto, OperationDialogOutput<TKey>? dialogOutput)
        {
            if (dialogOutput != null && dialogOutput.Succeeded)
            {
                //最新的数据
                var newEntity = await BaseService.Get(GetKey(dto));
                //父级变化重新加载列表
                if (newEntity != null)
                {
                    var pKey = GetParentKey(newEntity);
                    if (pKey != null && !pKey.Equals(GetParentKey(dto)))
                    {
                        //最新的数据
                        await ReLoadTable();
                        return;
                    }
                }
                //父级未变化直接变化本地对象
                ICollection<TDto>? children = GetChildren(dto);
                //重新赋值给界面对象
                newEntity.Adapt(dto);
                if (children != null)
                {
                    //子集也重新赋值给他
                    SetChildren(dto, children);
                }
                //给子集重新排队
                _datas.ForEach(x =>
                {
                    var pKey = GetParentKey(x);
                    if (pKey != null)
                    {
                        var p = GetNodeFromTree(pKey, x);
                        if (p != null)
                        {
                            ICollection<TDto>? children = GetChildren(x);
                            if (children != null)
                            {
                                SetChildren(p, SortChildren(children));
                            }
                        }
                    }

                });
                await RefreshPageDom();
            }
        }

        /// <summary>
        /// 当添加结束
        /// </summary>
        /// <param name="dialogOutput"></param>
        /// <returns></returns>
        protected override Task OnAddFinish(OperationDialogOutput<TKey>? dialogOutput)
        {
            if (dialogOutput != null && dialogOutput.Succeeded)
            {
                //最新的数据
                return ReLoadTable();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 当添加子节点结束 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dialogOutput"></param>
        /// <returns></returns>
        protected override async Task OnAddChildrenFinish(TDto dto, OperationDialogOutput<TKey>? dialogOutput)
        {
            if (dialogOutput != null && dialogOutput.Succeeded && dialogOutput.Data != null)
            {
                //最新的数据
                var newEntity = await BaseService.Get(dialogOutput.Data);
                ICollection<TDto> children = GetChildren(dto) ?? new List<TDto>();

                children.Add(newEntity);
                SetChildren(dto, SortChildren(children));

                await RefreshPageDom();
            }
        }
    }

    /// <summary>
    /// 树形table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog"></typeparam>
    public abstract class MultiTenantTreeTableBase<TDto, TKey, TOperationDialog> : MultiTenantTreeTableBase<TDto, TKey, TOperationDialog, SharedLocalResource> where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, SharedLocalResource> where TDto : class, new() where TKey : notnull
    {
    }
}
