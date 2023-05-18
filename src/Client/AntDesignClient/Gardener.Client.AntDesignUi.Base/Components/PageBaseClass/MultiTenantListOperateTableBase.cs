// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户相关功能
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TOperationDialogInput">操作弹框页输入参数</typeparam>
    /// <typeparam name="TOperationDialogOutput">操作弹框页输出参数</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// </remarks>
    public abstract class MultiTenantListOperateTableBase<TDto, TKey, TOperationDialog, TOperationDialogInput, TOperationDialogOutput, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : ListOperateTableBase<TDto, TKey, TOperationDialog, TOperationDialogInput, TOperationDialogOutput, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<TOperationDialogInput, TOperationDialogOutput, TLocalResource>
        where TLocalResource : SharedLocalResource
        where TOperationDialogInput : OperationDialogInput<TKey>, new()
        where TOperationDialogOutput : OperationDialogOutput, new()
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
        protected override void SetTableSearchParameters()
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
                    _tableSearchFieldDisplayNameConverts.Add(nameof(IModelTenantId.TenantId), (old) => nameof(IModelTenant.Tenant));
                    //非租户租户编号设置下拉数据
                    _tableSearchFieldSelectItemsProviders.Add(nameof(IModelTenantId.TenantId), async field =>
                    {
                        List<SystemTenantDto> tenants = await TenantService.GetAll();
                        return tenants.Select(x => new KeyValuePair<string, string>(x.Id.ToString(),x.Name));
                    });
                }

            }
            base.SetTableSearchParameters();
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
            //如果有租户数据，装配一下
            if (typeof(TDto).IsAssignableTo(typeof(IModelTenant)))
            {
                foreach (TDto item in datas)
                {
                    if (item is IModelTenant modelTenant)
                    {
                        modelTenant.Tenant = GetTenant(modelTenant.TenantId);
                    }
                }
            }
        }
    }


    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户相关功能
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// </remarks>
    public abstract class MultiTenantListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : MultiTenantListOperateTableBase<TDto, TKey, TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <returns></returns>
        protected Task OpenOperationDialogAsync(string title, OperationDialogInput<TKey> input, Func<OperationDialogOutput<TKey>?, Task>? onClose = null, OperationDialogSettings? operationDialogSettings = null)
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            return OpenOperationDialogAsync<TOperationDialog, OperationDialogInput<TKey>, OperationDialogOutput<TKey>>(title, input, onClose, settings);
        }
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户相关功能
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class MultiTenantListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource> : MultiTenantListOperateTableBase<TDto, TKey, TOperationDialog, TLocalResource, TKey, bool>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, TLocalResource>
        where TLocalResource : SharedLocalResource
    {
    }

    /// <summary>
    /// table列表基类(可以被当作OperationDialog打开，也能快速打开其它操作框)-支持多租户相关功能
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOperationDialog">操作弹框页</typeparam>
    /// <remarks>
    /// 包含列表加载、删除、导出、种子数据、添加、修改、详情
    /// 本地化资源，默认使用<see cref="SharedLocalResource"/>
    /// 快递打开操作框，输入 OperationDialogInput_TKey
    /// 快递打开操作框，输出 OperationDialogOutput_TKey
    /// 
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class MultiTenantListOperateTableBase<TDto, TKey, TOperationDialog> : MultiTenantListOperateTableBase<TDto, TKey, TOperationDialog, SharedLocalResource>
        where TDto : class, new()
        where TOperationDialog : OperationDialogBase<OperationDialogInput<TKey>, OperationDialogOutput<TKey>, SharedLocalResource>
    {
    }
}
