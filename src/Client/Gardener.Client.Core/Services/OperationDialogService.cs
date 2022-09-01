// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.Base.Services;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services
{
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
        /// <typeparam name="TComponent"></typeparam>
        /// <typeparam name="TComponentOptions"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="drawerService"></param>
        /// <param name="modalService"></param>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="dialogSettings"></param>
        /// <param name="onClose"></param>
        /// <remarks>
        /// 可以是抽屉，可以是弹框
        /// </remarks>
        /// <returns></returns>
        public async Task OpenAsync<TComponent, TComponentOptions, TResult>(string title, TComponentOptions input, Func<TResult, Task> onClose = null, OperationDialogSettings dialogSettings = null) where TComponent : FeedbackComponent<TComponentOptions, TResult>
        {
            dialogSettings = dialogSettings ?? ClientConstant.DefaultOperationDialogSettings;

            if (dialogSettings.DialogType.Equals(OperationDialogType.Modal))
            {
                ModalRef<TResult> result = await modalService.CreateModalAsync<TComponent, TComponentOptions, TResult>(new ModalOptions()
                {
                    Title = title,
                    Centered = dialogSettings.ModalCentered,
                    MaskClosable = dialogSettings.MaskClosable,
                    Width = dialogSettings.Width,
                    Footer = null,
                    DestroyOnClose = true,
                }, input);
                if (onClose != null)
                {
                    result.OnOk = onClose;
                }

            }
            else if (dialogSettings.DialogType.Equals(OperationDialogType.Drawer))
            {
                var result = await drawerService.CreateDialogAsync<TComponent, TComponentOptions, TResult>(
                    input,
                    closable: dialogSettings.Closable,
                    maskClosable: dialogSettings.MaskClosable,
                    title: title,
                    width: dialogSettings.Width,
                    placement: dialogSettings.DrawerPlacement.ToString().ToLower());
                await onClose(result);

            }
        }
    }
}
