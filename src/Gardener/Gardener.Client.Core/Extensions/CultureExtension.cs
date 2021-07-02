// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    public static class CultureExtension
    {
        /// <summary>
        /// 本地化浏览器缓存key
        /// </summary>
        public readonly static string BlazorCultureKey = "BlazorCulture";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public async static Task<WebAssemblyHost> UseCulture(this WebAssemblyHost host)
        {
            var jsTool = host.Services.GetRequiredService<JsTool>();
            var result = await jsTool.SessionStorage.GetAsync<string>(BlazorCultureKey);
            var culture = new CultureInfo(string.IsNullOrEmpty(result) ? "zh-CN" : result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            return host;
        }

    }
}
