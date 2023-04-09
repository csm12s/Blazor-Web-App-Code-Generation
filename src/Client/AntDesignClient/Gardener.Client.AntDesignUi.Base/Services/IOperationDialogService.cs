// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;

namespace Gardener.Client.AntDesignUi.Base.Services
{
    /// <summary>
    /// 操作对话框服务
    /// </summary>
    public interface IOperationDialogService
    {
        /// <summary>
        /// 打开
        /// </summary>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <typeparam name="TDialogOutput"></typeparam>
        /// <param name="drawerService"></param>
        /// <param name="modalService"></param>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <remarks>
        /// 可以是抽屉，可以是弹框
        /// </remarks>
        /// <returns></returns>
        public Task OpenAsync<TOperationDialog, TDialogInput, TDialogOutput>(string title, TDialogInput input, Func<TDialogOutput?, Task>? onClose = null, OperationDialogSettings? dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput, TDialogOutput>;

        /// <summary>
        /// 打开
        /// </summary>
        /// <typeparam name="TOperationDialog"></typeparam>
        /// <typeparam name="TDialogInput"></typeparam>
        /// <param name="drawerService"></param>
        /// <param name="modalService"></param>
        /// <param name="title"></param>
        /// <param name="input"></param>
        /// <param name="onClose"></param>
        /// <param name="dialogSettings"></param>
        /// <remarks>
        /// 可以是抽屉，可以是弹框
        /// </remarks>
        /// <returns></returns>
        public Task OpenAsync<TOperationDialog, TDialogInput>(string title, TDialogInput input, Func<Task>? onClose = null, OperationDialogSettings? dialogSettings = null) where TOperationDialog : FeedbackComponent<TDialogInput, bool>;

    }
}
