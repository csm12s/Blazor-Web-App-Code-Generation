// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.AntDesignUi.Base.Services;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Client.Base.Services;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// table列表页面基类(可以被当作OperationDialog打开)
    /// </summary>
    /// <typeparam name="TDto">对象Dto</typeparam>
    /// <typeparam name="TKey">对象的主键</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    public abstract class TableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : ReuseTabsPageAndFormBase<TSelfOperationDialogInput, TSelfOperationDialogOutput> where TDto : class, new() where TLocalResource : SharedLocalResource
    {
        /// <summary>
        /// table引用
        /// </summary>
        protected ITable? _table;

        /// <summary>
        /// 数据集合
        /// </summary>
        protected IEnumerable<TDto>? _datas;

        /// <summary>
        /// 选择的行
        /// </summary>
        protected IEnumerable<TDto>? _selectedRows;

        /// <summary>
        /// table加载中控制
        /// </summary>
        protected ClientLoading _tableLoading = new ClientLoading();

        /// <summary>
        /// 多选删除按钮加载中控制
        /// </summary>
        protected bool _deletesBtnLoading = false;

        /// <summary>
        /// 导出数据加载中绑定数据
        /// </summary>
        protected bool _exportDataLoading = false;

        /// <summary>
        /// 锁定按钮加载中
        /// </summary>
        protected ClientMultiLoading _lockBtnLoading = new ClientMultiLoading(false);
        /// <summary>
        /// 搜索组件
        /// </summary>
        protected TableSearch<TDto>? tableSearch;
        /// <summary>
        /// 用户在当前页面使用该资源是否越权
        /// <para>true 越权</para> 
        /// <para>false 不越权</para> 
        /// </summary>
        /// <remarks>
        /// 方便列表中组件显示隐藏绑定
        /// 在组件参数设置后（OnParametersSet）才有效
        /// <para>使用方式<code>UserUnauthorizedResources[ResourceKey]</code></para> 
        /// </remarks>
        protected ClientListBindValue<string, bool> UserUnauthorizedResources = new ClientListBindValue<string, bool>(true);
        /// <summary>
        /// 用户在当前页面使用该资源是否可以
        /// <para>true 可以</para> 
        /// <para>false 不可</para> 
        /// </summary>
        /// <remarks>
        /// 方便列表中组件显示隐藏绑定
        /// 在组件参数设置后（OnParametersSet）才有效
        /// <para>使用方式<code>UserAuthorizedResources[ResourceKey]</code></para> 
        /// </remarks>
        protected ClientListBindValue<string, bool> UserAuthorizedResources = new ClientListBindValue<string, bool>(false);
        /// <summary>
        /// 租户数据
        /// </summary>
        protected Dictionary<Guid, TenantDto> tenantMap = new Dictionary<Guid, TenantDto>();
        /// <summary>
        /// 租户服务    
        /// </summary>
        [Inject]
        private ITenantService tenantService { get; set; } = null!;
        /// <summary>
        /// 身份状态管理
        /// </summary>
        [Inject]
        protected IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        /// <summary>
        /// 服务
        /// </summary>
        [Inject]
        protected IServiceBase<TDto, TKey> BaseService { get; set; } = null!;

        /// <summary>
        /// 本地化
        /// </summary>
        [Inject]
        protected IClientLocalizer<TLocalResource> Localizer { get; set; } = null!;

        /// <summary>
        /// 操作对话框
        /// </summary>
        [Inject]
        protected IOperationDialogService OperationDialogService { get; set; } = null!;

        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        protected IClientMessageService MessageService { get; set; } = null!;
        /// <summary>
        /// 参数设置完成
        /// </summary>
        /// <returns></returns>
        protected override void OnParametersSet()
        {
            //资源越权绑定数据
            UserUnauthorizedResources = new ClientListBindValue<string, bool>(true, key => !AuthenticationStateManager.CheckCurrentUserHaveResource(key));
            UserAuthorizedResources = new ClientListBindValue<string, bool>(false, key => AuthenticationStateManager.CheckCurrentUserHaveResource(key));
            base.OnParametersSet();
        }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            bool isTenant = AuthenticationStateManager.CurrentUserIsTenant();
            if (!isTenant)
            {
                List<TenantDto> tenants = await tenantService.GetAll();
                foreach (TenantDto tenant in tenants)
                {
                    tenantMap.TryAdd(tenant.Id, tenant);
                }
            }
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 获取操作会话配置
        /// </summary>
        /// <returns></returns>
        protected virtual OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = new OperationDialogSettings();
            ClientConstant.DefaultOperationDialogSettings.Adapt(dialogSettings);
            SetOperationDialogSettings(dialogSettings);
            return dialogSettings;
        }

        /// <summary>
        /// 设置操作会话配置
        /// </summary>
        /// <param name="dialogSettings"></param>
        protected virtual void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            //set OperationDialogSettings
        }


        /// <summary>
        /// 打开操作对话框
        /// </summary>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <typeparam name="TDialogOutput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        protected Task OpenOperationDialogAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput?, Task>? onClose = null, OperationDialogSettings? operationDialogSettings = null, int? width = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput>
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            if (width.HasValue)
            {
                settings.Width = width.Value;
            }
            return OperationDialogService.OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(title, input, onClose, settings);
        }


        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        protected virtual async Task OnChangeIsLocked(TDto model, bool isLocked)
        {
            _lockBtnLoading.Start(model);
            if (model is IModelId<TKey> temp && model is IModelLocked temp1)
            {
                var result = await BaseService.Lock(temp.Id, isLocked);
                if (!result)
                {
                    temp1.IsLocked = !isLocked;
                    string msg = isLocked ? Localizer[SharedLocalResource.Lock] : Localizer[SharedLocalResource.Unlock];

                    MessageService.Error($"{msg} {Localizer[SharedLocalResource.Fail]}");
                }
            }
            else 
            {
                MessageService.Error($"{Localizer[SharedLocalResource.Error]}:{typeof(TDto).Name} no implement {nameof(IModelId<TKey>)} or {nameof(IModelLocked)}");
            }
            
            _lockBtnLoading.Stop(model);
        }

        /// <summary>
        /// 获取当前搜索组件的搜索条件
        /// </summary>
        /// <returns></returns>
        protected virtual List<FilterGroup>? GetTableSearchFilterGroups()
        {
            return tableSearch?.GetFilterGroups();
        }

        #region Page loading
        /// <summary>
        /// Page start loading
        /// </summary>
        /// <returns></returns>
        protected bool StartLoading()
        {
            var run = _tableLoading.Start();
            if (run)
            {
                InvokeAsync(StateHasChanged);
            }
            return run;
        }

        /// <summary>
        /// Page stop loading
        /// </summary>
        /// <returns></returns>
        protected bool StopLoading()
        {
            var stop = _tableLoading.Stop();
            if (stop)
            {
                InvokeAsync(StateHasChanged);
            }
            return stop;
        }
        #endregion
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual TKey GetKey(TDto dto)
        {
            if (dto is IModelId<TKey> temp)
            {
                return temp.Id;
            }
            else
            {
                throw new ArgumentException($"{Localizer[SharedLocalResource.Error]}:{typeof(TDto).Name} no implement {nameof(IModelId<TKey>)}");
            }
        }
        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 租户管理才有数据
        /// </remarks>
        protected TenantDto? GetTenant(Guid? tenantId)
        {
            if (tenantId == null || tenantId.Equals(Guid.Empty) || !tenantMap.ContainsKey(tenantId.Value))
            {
                return null;
            }
            return tenantMap[tenantId.Value];
        }
    }
    /// <summary>
    /// table列表页面基类(可以被当作OperationDialog打开)
    /// </summary>
    /// <typeparam name="TDto">对象Dto</typeparam>
    /// <typeparam name="TKey">对象的主键</typeparam>
    /// <typeparam name="TLocalResource">本地化资源</typeparam>
    /// <remarks>
    /// 此基类方便那些不需要弹出或弹出时没有输入输出时使用
    /// 自身作为OperationDialog接收的参数，默认为类型 <see cref="TKey"/>
    /// 自身作为OperationDialog返回的参数，默认为类型 <see cref="bool"/>
    /// </remarks>
    public abstract class TableBase<TDto, TKey, TLocalResource> : TableBase<TDto, TKey, TLocalResource, TKey, bool> 
        where TDto : class, new() 
        where TLocalResource : SharedLocalResource
    { 
    }

}
