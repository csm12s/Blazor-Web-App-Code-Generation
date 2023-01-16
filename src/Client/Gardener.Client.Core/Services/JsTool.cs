// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class JsToolBase
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private IJSObjectReference jSObjectReference;
        public JsToolBase(IJSRuntime js)
        {
            moduleTask = new(() => js.InvokeAsync<IJSObjectReference>("import", $"./js/js-tool/js-tool.js?_={DateTime.Now.ToString("yyyyMMddHHmmss")}").AsTask());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IJSObjectReference> GetJsToolModule()
        {
            if (jSObjectReference == null) 
            { 
                jSObjectReference = await moduleTask.Value;
            }
            return jSObjectReference;
        }

    }

    /// <summary>
    /// js操作工具
    /// </summary>
    [ScopedService]
    public class JsTool : JsToolBase, IJsTool
    {
        /// <summary>
        /// js操作工具
        /// </summary>
        /// <param name="js"></param>
        public JsTool(IJSRuntime js) : base(js)
        {
            SessionStorage = new SessionStorage(js);
            Document = new Document(js);
            LocalStorage = new LocalStorage(js);
            Cookie = new Cookie(js);
        }
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
    /// <summary>
    /// LocalStorage
    /// </summary>
    public class LocalStorage : IWebStorage
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
    /// <summary>
    /// Document
    /// </summary>
    public class Document : JsToolBase, IDocument
    {
        public Document(IJSRuntime js) : base(js)
        {
        }

        public async Task SetTitle(string title)
        {
            var module = await GetJsToolModule();
            await module.InvokeVoidAsync("setDocumentTitle", title);
        }

        public async Task DownloadFile(string url)
        {
            var module = await GetJsToolModule();
            await module.InvokeVoidAsync("downloadFile", url);
        }
    }
    /// <summary>
    /// Cookie
    /// </summary>
    public class Cookie : JsToolBase, ICookie
    {
        public Cookie(IJSRuntime js) : base(js)
        {
        }

        public async Task<Dictionary<string, string>> Get()
        {
            var module = await GetJsToolModule();
            return await module.InvokeAsync<Dictionary<string, string>>("getAllCookies");
        }

        public async Task<string> Get(string key, string domain = null)
        {
            var module = await GetJsToolModule();
            return await module.InvokeAsync<string>("getCookies", key, new { domain });
        }

        public async Task Remove(string key, string path = null, string domain = null)
        {
            var module = await GetJsToolModule();
            await module.InvokeVoidAsync("removeCookies", key, new { domain, path });
        }

        public async Task Set(string key, string value, int? expires = null, string path = null)
        {
            var module = await GetJsToolModule();
            await module.InvokeVoidAsync("setCookies", key, value, new { expires, path });
        }
    }
}
