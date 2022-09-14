// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base.Constants;
using Gardener.Client.Base.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System;
using Mapster;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 弹出框基类
    /// </summary>
    /// <typeparam name="TComponentOptions"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class OperationDialogBase<TComponentOptions, TResult> : FeedbackComponent<TComponentOptions, TResult> 
    {
        /// <summary>
        /// 操作对话框
        /// </summary>
        [Inject]
        protected IOperationDialogService operationDialogService { get; set; }

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
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="operationDialogSettings "></param>
        /// <param name="width "></param>
        /// <returns></returns>
        protected async Task OpenOperationDialogAsync<TComponent, TComponentOptions, TResult>(string title, TComponentOptions input, Func<TResult, Task> onClose = null, OperationDialogSettings operationDialogSettings = null, int? width = null) where TComponent : FeedbackComponent<TComponentOptions, TResult>
        {
            OperationDialogSettings settings = operationDialogSettings ?? GetOperationDialogSettings();
            if (width.HasValue)
            {
                settings.Width = width.Value;
            }
            await operationDialogService.OpenAsync<TComponent, TComponentOptions, TResult>(title, input, onClose, settings);
        }

        /// <summary>
        /// 关闭自己
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected async Task CloseAsync<T>(T result)
        {
            await base.FeedbackRef.CloseAsync(result);
        }
        /// <summary>
        /// 强制刷新
        /// </summary>
        /// <returns></returns>
        protected async Task RefreshPage()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
