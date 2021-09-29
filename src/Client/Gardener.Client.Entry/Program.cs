using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Gardener.Client.Base;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components.Authorization;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace Gardener.Client.Entry
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            #region api settings
            builder.AddApiSetting();
            #endregion

            #region httpclient
            builder.Services.AddScoped(sp => {
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
            builder.Services.AddServicesWithAttributeOfType(Core.Entry.GetAssemblies());
            #endregion

            builder.Services.AddTypeAdapterConfigs();

            var host = await builder
                 .Build().UseCulture();

            await host.RunAsync();
        }
    }
}
