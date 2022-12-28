// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Client.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.Core.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
        /// <typeparam name="TDefaultResource"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCulture<TDefaultResource>(this IServiceCollection services)
        {
            //默认的
            services.TryAddScoped<IClientLocalizer, ClientSharedLocalizer<TDefaultResource>>();
            services.TryAddScoped(typeof(IClientLocalizer<>),typeof(ClientLocalizer<>));
            return services;
        }
    }
}
