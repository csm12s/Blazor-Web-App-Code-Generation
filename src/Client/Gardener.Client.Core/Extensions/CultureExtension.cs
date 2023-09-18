// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Services;
using Gardener.Client.Core.Services;
using Gardener.LocalizationLocalizer;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
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
        public async static Task<WebAssemblyHost> UseAppLocalization(this WebAssemblyHost host, string cultureStorageKey, string defaultCulture)
        {
            IClientCultureService cultureService = host.Services.GetRequiredService<IClientCultureService>();
            await cultureService.Init(cultureStorageKey, defaultCulture);
            host.Services.InitLocalizationLocalizerUtil();
            return host;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDefaultResource"></typeparam>
        /// <param name="services"></param>
        /// <param name="resourcesPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppLocalization<TDefaultResource>(this IServiceCollection services, string? resourcesPath = default)
        {
            //Culture 管理地区标示
            services.TryAddScoped<IClientCultureService, ClientCultureService>();
            //本地化
            services.AddLocalization(options =>
            {
                if (!string.IsNullOrEmpty(resourcesPath))
                { 
                    options.ResourcesPath = resourcesPath;
                }
            });
            //注入内置 IStringLocalizer
            services.TryAddTransient<IStringLocalizer>(serviceProvider =>
            {
                IStringLocalizerFactory stringLocalizerFactory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();
                return stringLocalizerFactory.Create(typeof(TDefaultResource));
            });
            //注入封装的 Localizer 处理不同地区本地化内容
            services.AddLocalizationLocalizer<TDefaultResource>();
            return services;
        }
    }
}
