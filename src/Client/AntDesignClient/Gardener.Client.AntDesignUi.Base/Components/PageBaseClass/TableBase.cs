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
using Gardener.LocalizationLocalizer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

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
        /// 页面是否首次加载完成
        /// </summary>
        protected bool _pageFirstLoaded = false;

        /// <summary>
        /// TableSearch搜索条件提供器
        /// </summary>
        protected List<Func<List<FilterGroup>?>> _tableSearchFilterGroupProviders = new();

        #region TableSearch
        /// <summary>
        /// 搜索组件
        /// </summary>
        protected TableSearch<TDto>? _tableSearch;

        /// <summary>
        /// 搜索组件设置
        /// </summary>
        protected TableSearchSettings _tableSearchSettings = new TableSearchSettings();

        protected string searchInputStyle = $"margin-right:8px;margin-bottom:2px;width:100px";
        #endregion

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
        /// 用户在当前页面使用该资源是否越权-用于绑定式判断资源权限
        /// <para>true 越权</para> 
        /// <para>false 不越权</para> 
        /// </summary>
        /// <remarks>
        /// 方便列表中组件显示隐藏绑定
        /// 在组件参数设置后（OnParametersSet）才有效
        /// <para>使用方式<code>_userUnauthorizedResources[ResourceKey]</code></para> 
        /// </remarks>
        protected ClientListBindValue<string, bool> _userUnauthorizedResources = new ClientListBindValue<string, bool>(true);
        /// <summary>
        /// 用户在当前页面使用该资源是否可以-用于绑定式判断资源权限
        /// <para>true 可以</para> 
        /// <para>false 不可</para> 
        /// </summary>
        /// <remarks>
        /// 方便列表中组件显示隐藏绑定
        /// 在组件参数设置后（OnParametersSet）才有效
        /// <para>使用方式<code>_userAuthorizedResources[ResourceKey]</code></para> 
        /// </remarks>
        protected ClientListBindValue<string, bool> _userAuthorizedResources = new ClientListBindValue<string, bool>(false);

        #region services
        /// <summary>
        /// 确认提示服务
        /// </summary>
        [Inject]
        protected ConfirmService ConfirmService { get; set; } = null!;
        /// <summary>
        /// 路由导航服务
        /// </summary>
        [Inject]
        protected NavigationManager Navigation { get; set; } = null!;
        /// <summary>
        /// javascript 工具
        /// </summary>
        [Inject]
        protected IJsTool JsTool { get; set; } = null!;
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
        protected ILocalizationLocalizer<TLocalResource> Localizer { get; set; } = null!;

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
        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override void OnInitialized()
        {
            //资源绑定数据-用于绑定式判断资源权限
            _userUnauthorizedResources = new ClientListBindValue<string, bool>(true, key => !AuthenticationStateManager.CheckCurrentUserHaveResource(key));
            _userAuthorizedResources = new ClientListBindValue<string, bool>(false, AuthenticationStateManager.CheckCurrentUserHaveResource);
            //从url加载TableSearch参数
            var url = new Uri(Navigation.Uri);
            var query = url.Query;
            Dictionary<string, StringValues> urlParams = QueryHelpers.ParseQuery(query);
            if (urlParams != null && urlParams.Count() > 0)
            {
                urlParams.ForEach(x =>
                {
                    _tableSearchSettings.DefaultValue.Add(x.Key, x.Value.ToString());
                });
            }
            _tableSearchFilterGroupProviders.Add(GetTableSearchFilterGroups);
            //设置搜索组件的参数
            SetTableSearchParameters(_tableSearchSettings,_tableSearchFilterGroupProviders);
            base.OnInitialized();
        }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 添加需要排除的字段
        /// </summary>
        /// <param name="fields"></param>
        /// <remarks>
        /// 此方法在OnInitialized时执行有效
        /// </remarks>
        protected void AddExcludeSearchFields(params string[] fields)
        {
            if (_tableSearchSettings.ExcludeFields == null)
            {
                _tableSearchSettings.ExcludeFields=new List<string>();
            }
            foreach (string field in fields)
            {
                if (!_tableSearchSettings.ExcludeFields.Contains(field))
                {
                    _tableSearchSettings.ExcludeFields=_tableSearchSettings.ExcludeFields.Append(field);
                }
            }
        }

        /// <summary>
        /// 添加需要包含的字段
        /// </summary>
        /// <param name="fields"></param>
        /// <remarks>
        /// 此方法在OnInitialized时执行有效
        /// </remarks>
        protected void AddIncludeSearchFields(params string[] fields)
        {
            if (_tableSearchSettings.IncludeFields == null)
            {
                _tableSearchSettings.IncludeFields = new List<string>();
            }
            foreach (string field in fields)
            {
                if (!_tableSearchSettings.IncludeFields.Contains(field))
                {
                    _tableSearchSettings.IncludeFields = _tableSearchSettings.IncludeFields.Append(field);
                }
            }
        }

        /// <summary>
        /// 添加搜索条件提供器，在发送请求的时候将条件追加到请求中
        /// </summary>
        /// <param name="tableSearchFilterGroupProvider"></param>
        protected void AddTableSearchFilterGroupProvider(Func<List<FilterGroup>?> tableSearchFilterGroupProvider)
        {
            _tableSearchFilterGroupProviders.Add(tableSearchFilterGroupProvider);
        }

        /// <summary>
        /// 设置TableSearch特定参数
        /// </summary>
        /// <param name="tableSearchSettings">TableSearch设置</param>
        /// <param name="tableSearchFilterGroupProviders">条件结果获取方法</param>
        /// <remarks>
        /// 此方法在<see cref="TableBase{TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput}.OnInitialized"/>时执行
        /// </remarks>
        protected virtual void SetTableSearchParameters(TableSearchSettings tableSearchSettings, List<Func<List<FilterGroup>?>> tableSearchFilterGroupProviders)
        {
            //设置参数
        }

        /// <summary>
        /// 获取操作会话配置
        /// </summary>
        /// <returns></returns>
        protected virtual OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = ClientConstant.DefaultOperationDialogSettings;
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
            return _tableSearch?.GetFilterGroups();
        }

        #region loading
        /// <summary>
        /// table start loading
        /// </summary>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected bool StartTableLoading(bool forceRender = false)
        {
            var run = _tableLoading.Start();
            if (run && forceRender)
            {
                InvokeAsync(StateHasChanged);
            }
            return run;
        }

        /// <summary>
        /// table stop loading
        /// </summary>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected bool StopTableLoading(bool forceRender = false)
        {
            var stop = _tableLoading.Stop();
            if (stop && forceRender)
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
