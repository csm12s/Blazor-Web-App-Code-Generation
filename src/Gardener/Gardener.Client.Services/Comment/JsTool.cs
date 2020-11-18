﻿// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class JsTool
    {
        public JsTool(IJSRuntime js)
        {
            SessionStorage = new SessionStorage(js);
        }
        /// <summary>
        /// session storage
        /// </summary>
        public SessionStorage SessionStorage { get; init; }
    }
    /// <summary>
    /// session storage
    /// </summary>
    public class SessionStorage
    {
        private readonly IJSRuntime js;
        public SessionStorage(IJSRuntime js)
        {
            this.js = js;
        }
        public async Task Set(string key, string value)
        {
            await js.InvokeVoidAsync("sessionStorage.setItem", key, value);
        }
        public async Task<T> Get<T>(string key)
        {
           return await js.InvokeAsync<T>("sessionStorage.getItem", key);
        }
        public async Task Remove(string key)
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", key);
        }
    }
}
