// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.AntDesignUi.Base.Services;
using Gardener.LocalizationLocalizer;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.CustomService
{
    /// <summary>
    /// table基类（无主键表，TDto不继承BaseDto）
    /// </summary>
    public abstract class BaseTable<TDto, TKey, TLocalResource> 
        : ReuseTabsPageAndFormBase<TKey, bool> 
        where TDto : class, new()
    {
        /// <summary>
        /// table引用
        /// </summary>
        protected ITable? _table;

        /// <summary>
        /// 当前页数据集合
        /// </summary>
        protected IEnumerable<TDto>? _datas;

        /// <summary>
        /// 选择的行
        /// </summary>
        protected IEnumerable<TDto>? _selectedRows;

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
        /// 服务
        /// </summary>
        [Inject]
        protected IServiceBase<TDto, TKey> _service { get; set; } = null!;

        /// <summary>
        /// 本地化
        /// </summary>
        [Inject]
        protected ILocalizationLocalizer<TLocalResource> localizer { get; set; } = null!;

        /// <summary>
        /// 操作对话框
        /// </summary>
        [Inject]
        protected IOperationDialogService operationDialogService { get; set; } = null!;

        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        protected MessageService messageService { get; set; } = null!;


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
        protected async Task OpenOperationDialogAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput?, Task>? onClose = null, OperationDialogSettings? operationDialogSettings = null, int? width = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput>
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            if (width.HasValue)
            {
                settings.Width = width.Value;
            }
            await operationDialogService.OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(title, input, onClose, settings);
        }
    }
}
