// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Web
{
    /// <summary>
    /// 自定义异常处理
    /// </summary>
    public class CustomErrorBoundary : ErrorBoundaryBase
    {
        [Inject]
        private IClientLogger _clientLogger { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, ChildContent);
        }
        /// <summary>
        /// 发生异常
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected override Task OnErrorAsync(Exception exception)
        {
            _clientLogger.Error("全局异常捕获：" + exception.Message, ex: exception);

            return Task.CompletedTask;
        }
    }
}
