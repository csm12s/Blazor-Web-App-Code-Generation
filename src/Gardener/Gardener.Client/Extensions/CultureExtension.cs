// -----------------------------------------------------------------------------
// 文件头
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

           var result= await jsTool.SessionStorage.GetAsync<string>(SystemConstant.BlazorCultureKey);
            System.Console.WriteLine("========================>"+ (string.IsNullOrEmpty(result) ? "zh-CN" : result));
            var culture = new CultureInfo(string.IsNullOrEmpty(result)? "zh-CN" : result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            return host;
        }

    }
}
