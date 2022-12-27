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
        public async static Task<WebAssemblyHost> UseCulture(this WebAssemblyHost host)
        {
            var jsTool = host.Services.GetRequiredService<IJsTool>();
            var result = await jsTool.SessionStorage.GetAsync<string>(ClientConstant.BlazorCultureKey);
            var culture = new CultureInfo(string.IsNullOrEmpty(result) ? "zh-CN" : result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            LocalizerUtil.Services = host.Services;
            return host;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isDefault">是否是默认的资源</param>
        public static IServiceCollection AddCulture<T>(this IServiceCollection services, bool isDefault = false)
        {
            //默认的
            if (isDefault)
            {
                services.TryAddScoped<IClientLocalizer, ClientSharedLocalizer<T>>();
            }
            services.AddScoped<IClientLocalizer<T>, ClientLocalizer<T>>();
            return services;
        }
    }
}
