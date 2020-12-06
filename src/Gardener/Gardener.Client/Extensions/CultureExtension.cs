// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Constants;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading.Tasks;

namespace Gardener.Client.Extensions
{
    public static class CultureExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public async static Task<WebAssemblyHost> UseCulture(this WebAssemblyHost host)
        {
            var jsTool = host.Services.GetRequiredService<JsTool>();
            var result = await jsTool.SessionStorage.GetAsync<string>(SystemConstant.BlazorCultureKey);
            var culture = new CultureInfo(string.IsNullOrEmpty(result) ? "zh-CN" : result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            return host;
        }

    }
}
