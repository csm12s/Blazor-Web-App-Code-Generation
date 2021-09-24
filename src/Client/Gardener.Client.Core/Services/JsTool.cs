// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    [ScopedService]
    public class JsTool : IJsTool
    {
        public JsTool(IJSRuntime js)
        {
            SessionStorage = new SessionStorage(js);
            Document = new Document(js);
            LocalStorage = new LocalStorage(js);
        }
        /// <summary>
        /// session storage
        /// </summary>
        public IWebStorage SessionStorage { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public IWebStorage LocalStorage { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public IDocument Document { get; set; }
    }

    /// <summary>
    /// session storage
    /// </summary>
    public class SessionStorage : IWebStorage
    {
        private readonly IJSRuntime js;
        public SessionStorage(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task SetAsync(string key, object value)
        {
            await js.InvokeVoidAsync("sessionStorage.setItem", key, value);
        }
        public async Task<T> GetAsync<T>(string key)
        {
            return await js.InvokeAsync<T>("sessionStorage.getItem", key);
        }
        public async Task RemoveAsync(string key)
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", key);
        }
    }
    public class LocalStorage: IWebStorage
    {
        private readonly IJSRuntime js;
        public LocalStorage(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task SetAsync(string key, object value)
        {
            await js.InvokeVoidAsync("localStorage.setItem", key, value);
        }
        public async Task<T> GetAsync<T>(string key)
        {
            return await js.InvokeAsync<T>("localStorage.getItem", key);
        }
        public async Task RemoveAsync(string key)
        {
            await js.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
    public class Document : IDocument
    {
        private readonly IJSRuntime js;
        public Document(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task SetTitle(string title)
        {
            await js.InvokeVoidAsync("document.setTitle", title);
        }
    }
}
