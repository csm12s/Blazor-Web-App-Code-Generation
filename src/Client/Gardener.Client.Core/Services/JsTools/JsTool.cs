// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Microsoft.JSInterop;

namespace Gardener.Client.Core.Services
{

    /// <summary>
    /// js操作工具
    /// </summary>
    [ScopedService]
    public class JsTool : IJsTool
    {
        /// <summary>
        /// JSRuntime
        /// </summary>
        private IJSRuntime js;
        /// <summary>
        /// js操作工具
        /// </summary>
        /// <param name="js"></param>
        /// <param name="localStorage"></param>
        /// <param name="sessionStorage"></param>
        /// <param name="document"></param>
        /// <param name="cookie"></param>
        public JsTool(IJSRuntime js, ILocalStorage localStorage, ISessionStorage sessionStorage, IDocument document, ICookie cookie)
        {
            this.js = js;
            SessionStorage = sessionStorage;
            Document = document;
            LocalStorage = localStorage;
            Cookie = cookie;
        }
        /// <summary>
        /// JSRuntime
        /// </summary>
        public IJSRuntime Js { get => js; }
        /// <summary>
        /// session storage
        /// </summary>
        public IWebStorage SessionStorage { get; init; }
        /// <summary>
        /// LocalStorage
        /// </summary>
        public IWebStorage LocalStorage { get; init; }
        /// <summary>
        /// Document
        /// </summary>
        public IDocument Document { get; init; }
        /// <summary>
        /// Cookie
        /// </summary>
        public ICookie Cookie { get; init; }
    }

}
