// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Threading.Tasks;
using System;

namespace Gardener.Client.Base.Services
{
    /// <summary>
    /// 操作对话框服务
    /// </summary>
    public interface IOperationDialogService
    {
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
        /// <param name="onClose"></param>
        /// <param name="drawerSettings"></param>
        /// <remarks>
        /// 可以是抽屉，可以是弹框
        /// </remarks>
        /// <returns></returns>
        public Task OpenAsync<TComponent, TComponentOptions, TResult>(string title, TComponentOptions input, Func<TResult, Task> onClose = null, OperationDialogSettings drawerSettings = null) where TComponent : FeedbackComponent<TComponentOptions, TResult>;
    }
}
