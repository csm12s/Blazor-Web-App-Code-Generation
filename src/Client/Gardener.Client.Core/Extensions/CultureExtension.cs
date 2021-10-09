// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.Core.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    public static class CultureExtension
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public async static Task<WebAssemblyHost> UseCulture(this WebAssemblyHost host)
        {
            var jsTool = host.Services.GetRequiredService<IJsTool>();
            var result = await jsTool.SessionStorage.GetAsync<string>(ClientConstant.BlazorCultureKey);
            var culture = new CultureInfo(string.IsNullOrEmpty(result) ? "zh-CN" : result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            //
            LocalizerUtil.Localizer = host.Services.GetRequiredService<IClientLocalizer>();

            return host;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public static IServiceCollection AddCulture<T>(this IServiceCollection services)
        {
            services.AddScoped<IClientLocalizer, ClientLocalizer<T>>();
            return services;
        }
    }
}
