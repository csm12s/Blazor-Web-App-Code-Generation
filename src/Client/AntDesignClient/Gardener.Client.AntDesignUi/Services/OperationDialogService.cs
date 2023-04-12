// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.AntDesignUi.Base;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.AntDesignUi.Base.Services;
using Gardener.Client.Base;

namespace Gardener.Client.AntDesignUi.Services
{
    /// <summary>
    /// 操作对话框服务
    /// </summary>
    [ScopedService]
    public class OperationDialogService : IOperationDialogService
    {
        private readonly ModalService modalService;
        private readonly DrawerService drawerService;

        public OperationDialogService(ModalService modalService, DrawerService drawerService)
        {
            this.modalService = modalService;
            this.drawerService = drawerService;
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <remarks>
        /// 有输入，有输出，输出通过onClose回调返回
        /// </remarks>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <typeparam name="TDialogOutput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <returns></returns>
        public async Task OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput?, Task>? onClose = null, OperationDialogSettings? dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput>
        {
            dialogSettings = dialogSettings ?? ClientConstant.DefaultOperationDialogSettings;

            if (dialogSettings.DialogType.Equals(OperationDialogType.Modal))
            {
                ModalOptions modalOptions = new ModalOptions()
                {
                    Title = title,
                    Centered = dialogSettings.ModalCentered,
                    MaskClosable = dialogSettings.MaskClosable,
                    Width = dialogSettings.Width,
                    Footer = null,
                    DestroyOnClose = true,
                    Maximizable = dialogSettings.ModalMaximizable,
                    #pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                    DefaultMaximized = dialogSettings.ModalDefaultMaximized
                    #pragma warning restore BL0005 // Component parameter should not be set outside of its component.

                };

                ModalRef<TDialogOutput> result = await modalService.CreateModalAsync<TOperationDialog, TDialogInput, TDialogOutput>(modalOptions, input);

                if (onClose != null)
                {
                    result.OnOk = onClose;

                }
                result.OnClose = () =>
                {
                    onClose?.Invoke(default);
                    return Task.CompletedTask;
                };

            }
            else if (dialogSettings.DialogType.Equals(OperationDialogType.Drawer))
            {
                DrawerOptions config = new DrawerOptions()
                {
                    Closable = dialogSettings.Closable,
                    MaskClosable = dialogSettings.MaskClosable,
                    Title = title,
                    Width = dialogSettings.Width,
                    Height = dialogSettings.DrawerHeight,
                    Placement = dialogSettings.DrawerPlacement.ToString().ToLower()
                };

                var drawerRef = await drawerService.CreateAsync<TOperationDialog, TDialogInput, TDialogOutput>(config, input);
                drawerRef.OnClosed = r =>
                {
                    return onClose?.Invoke(r);
                };
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <remarks>
        /// 有输入，无输出
        /// </remarks>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <returns></returns>
        public async Task OpenAsync<TOperationDialog, TDialogInput>(string title, TDialogInput input, Func<Task>? onClose = null, OperationDialogSettings? dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput, bool>
        {
            Func<bool, Task> close = r =>
            {

                return onClose?.Invoke() ?? Task.CompletedTask;
            };
            await OpenAsync<TOperationDialog, TDialogInput, bool>(title, input, close, dialogSettings);
        }
    }
}
