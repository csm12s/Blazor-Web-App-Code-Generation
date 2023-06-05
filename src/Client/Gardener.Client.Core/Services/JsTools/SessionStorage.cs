// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services.JsTools
{
    /// <summary>
    /// session storage
    /// </summary>
    [ScopedService]
    public class SessionStorage : ISessionStorage
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IJSRuntime js;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="js"></param>
        public SessionStorage(IJSRuntime js)
        {
            this.js = js;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetAsync(string key, object value)
        {
            await js.InvokeVoidAsync("sessionStorage.setItem", key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return await js.InvokeAsync<T>("sessionStorage.getItem", key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string key)
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", key);
        }
    }
}
