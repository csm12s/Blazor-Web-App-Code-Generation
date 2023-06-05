// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services.JsTools
{
    /// <summary>
    /// Cookie
    /// </summary>
    [ScopedService]
    public class Cookie : JsModuleBase, ICookie
    {
        public Cookie(IJSRuntime js) : base(js, "./_content/Gardener.Client.Core/js.cookie.min.mjs")
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> Get()
        {
            var module = await GetModule();
            return await module.InvokeAsync<Dictionary<string, string>>("Cookies.get");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<string> Get(string key, string? domain = null)
        {
            var module = await GetModule();
            return await module.InvokeAsync<string>("Cookies.get", key, new { domain });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task Remove(string key, string? path = null, string? domain = null)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("Cookies.remove", key, new { domain, path });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task Set(string key, string value, int? expires = null, string? path = null)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("Cookies.set", key, value, new { expires, path });
        }
    }
}
