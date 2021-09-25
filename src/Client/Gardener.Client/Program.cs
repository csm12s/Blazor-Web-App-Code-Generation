// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Gardener.Client.Services;
using Gardener.Client.Core;
using Microsoft.Extensions.Configuration;
using Gardener.Client.Base;

namespace Gardener.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            #region api settings
            builder.AddApiSetting();
            #endregion

            #region httpclient
            builder.Services.AddSingleton(sp => {
                ApiSettings settings = sp.GetService<ApiSettings>();
                return new HttpClient { BaseAddress = new Uri(settings.BaseAddres) }; 
            });
            #endregion

            #region log
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );
            #endregion

            #region ant design
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            #endregion

            #region 认证、授权
            builder.Services.AddScoped<IAuthenticationStateManager, AuthenticationStateManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore(option =>
            {
                option.AddPolicy(AuthConstant.DefaultAuthenticatedPolicy, a => a.RequireAuthenticatedUser());
                option.AddPolicy(AuthConstant.ClientUIResourcePolicy, a => a.Requirements.Add(new ClientUIAuthorizationRequirement()));
                option.DefaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
            });
            builder.Services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
            #endregion

            #region 本地化
            builder.Services.AddLocalization(option =>
            {
                option.ResourcesPath = "Resources";
            });
            #endregion

            #region services
            builder.Services.AddServicesWithAttributeOfType<ScopedServiceAttribute>(
                typeof(Entry).GetTypeInfo().Assembly);
            builder.Services.AddServicesWithAttributeOfType<TransientServiceAttribute>(
                typeof(Entry).GetTypeInfo().Assembly);
            builder.Services.AddServicesWithAttributeOfType<SingletonServiceAttribute>(
                typeof(Entry).GetTypeInfo().Assembly);
            #endregion

            builder.Services.AddTypeAdapterConfigs();

            var host = await builder
                 .Build().UseCulture();
            await host.RunAsync();
        }
    }
}
