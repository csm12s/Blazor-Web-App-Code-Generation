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
using Mapster;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Components {
    //TODO: 这里原来继承ReuseTabsPageBase，修改后的页面可以被当作Modal打开,
    //请确保这里不会出错，如果出错，请修改CodeGenConfigView，不再使用ReuseTabsPageAndFormBase
    /// <summary>
    /// table基类
    /// </summary>
    public abstract class TableBase<TDto, TKey, TLocalResource> : ReuseTabsPageAndFormBase<TKey, bool> where TDto : BaseDto<TKey>, new() {
        /// <summary>
        /// table引用
        /// </summary>
        protected ITable _table;

        /// <summary>
        /// 数据集合
        /// </summary>
        protected IEnumerable<TDto> _datas;

        /// <summary>
        /// 选择的行
        /// </summary>
        protected IEnumerable<TDto> _selectedRows;

        /// <summary>
        /// table加载中控制
        /// </summary>
        protected bool _tableIsLoading = false;

        /// <summary>
        /// 多选删除按钮加载中控制
        /// </summary>
        protected bool _deletesBtnLoading = false;

        /// <summary>
        /// 导出数据加载中绑定数据
        /// </summary>
        protected bool _exportDataLoading = false;
        /// <summary>
        /// 搜索组件
        /// </summary>
        protected TableSearch<TDto>? tableSearch;

        /// <summary>
        /// 服务
        /// </summary>
        [Inject]
        protected IServiceBase<TDto, TKey> _service { get; set; }

        /// <summary>
        /// 本地化
        /// </summary>
        [Inject]
        protected IClientLocalizer<TLocalResource> localizer { get; set; }

        /// <summary>
        /// 操作对话框
        /// </summary>
        [Inject]
        protected IOperationDialogService operationDialogService { get; set; }

        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        protected MessageService messageService { get; set; }


        /// <summary>
        /// 获取操作会话配置
        /// </summary>
        /// <returns></returns>
        protected virtual OperationDialogSettings GetOperationDialogSettings() {
            OperationDialogSettings dialogSettings = new OperationDialogSettings();
            ClientConstant.DefaultOperationDialogSettings.Adapt(dialogSettings);
            SetOperationDialogSettings(dialogSettings);
            return dialogSettings;
        }

        /// <summary>
        /// 设置操作会话配置
        /// </summary>
        /// <param name="dialogSettings"></param>
        protected virtual void SetOperationDialogSettings(OperationDialogSettings dialogSettings) {
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
        protected async Task OpenOperationDialogAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput, Task> onClose = null, OperationDialogSettings operationDialogSettings = null, int? width = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput> {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            if (width.HasValue) {
                settings.Width = width.Value;
            }
            await operationDialogService.OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(title, input, onClose, settings);
        }


        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        protected virtual async Task OnChangeIsLocked(TDto model, bool isLocked) {
            var result = await _service.Lock(model.Id, isLocked);
            if (!result) {
                model.IsLocked = !isLocked;
                string msg = isLocked ? localizer[SharedLocalResource.Lock] : localizer[SharedLocalResource.Unlock];

                messageService.Error($"{msg} {localizer[SharedLocalResource.Fail]}");
            }
        }

        /// <summary>
        /// 获取当前搜索组件的搜索条件
        /// </summary>
        /// <returns></returns>
        protected virtual List<FilterGroup> GetTableSearchFilterGroups() {
            return tableSearch?.GetFilterGroups();
        }
    }
}
