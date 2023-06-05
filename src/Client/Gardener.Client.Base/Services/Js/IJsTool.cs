// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Services;
using Microsoft.JSInterop;

namespace Gardener.Client.Base
{
    /// <summary>
    /// JsTool
    /// </summary>
    public interface IJsTool
    {
        /// <summary>
        /// JSRuntime
        /// </summary>
        IJSRuntime Js { get; }
        /// <summary>
        /// Document
        /// </summary>
        IDocument Document { get; init; }
        /// <summary>
        /// LocalStorage
        /// </summary>
        IWebStorage LocalStorage { get; init; }
        /// <summary>
        /// SessionStorage
        /// </summary>
        IWebStorage SessionStorage { get; init; }
        /// <summary>
        /// Cookie
        /// </summary>
        ICookie Cookie { get; init; }
        /// <summary>
        /// 打印
        /// </summary>
        public IPrintingService Printing { get; init; }
    }
}