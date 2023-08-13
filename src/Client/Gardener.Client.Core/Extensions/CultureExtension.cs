// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.Client.Core.Services;
using Gardener.LocalizationLocalizer;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
        /// <param name="cultureStorageKey"></param>
        /// <param name="defaultCulture"></param>
        public async static Task<WebAssemblyHost> UseCulture(this WebAssemblyHost host, string cultureStorageKey, string defaultCulture)
        {
            var jsTool = host.Services.GetRequiredService<IJsTool>();
            var result = await jsTool.SessionStorage.GetAsync<string>(cultureStorageKey);
            var culture = new CultureInfo(string.IsNullOrEmpty(result) ? defaultCulture : result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            host.Services.InitLocalizationLocalizerUtil();
            return host;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDefaultResource"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCulture<TDefaultResource>(this IServiceCollection services)
        {
            //Culture 管理地区标示
            services.TryAddScoped<IClientCultureService, ClientCultureService>();
            //Localizer 处理不同地区本地化内容
            services.AddLocalizationLocalizer<TDefaultResource>();
            return services;
        }
    }
}
