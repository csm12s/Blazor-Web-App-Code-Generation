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
using Mapster;
using Microsoft.AspNetCore.Components;

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
    public abstract class TableBase<TDto, TKey, TLocalResource, TSelfOperationDialogInput, TSelfOperationDialogOutput> : ReuseTabsPageAndFormBase<TSelfOperationDialogInput, TSelfOperationDialogOutput> where TDto : BaseDto<TKey>, new() where TLocalResource : SharedLocalResource
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
            var result = await BaseService.Lock(model.Id, isLocked);
            if (!result)
            {
                model.IsLocked = !isLocked;
                string msg = isLocked ? Localizer[SharedLocalResource.Lock] : Localizer[SharedLocalResource.Unlock];

                MessageService.Error($"{msg} {Localizer[SharedLocalResource.Fail]}");
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
    public abstract class TableBase<TDto, TKey, TLocalResource> : TableBase<TDto, TKey, TLocalResource, TKey, bool> where TDto : BaseDto<TKey>, new() where TLocalResource : SharedLocalResource
    { 
    }

}
