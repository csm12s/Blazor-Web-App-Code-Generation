using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Gardener.Client.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using Gardener.Base.Resources;
using AntDesign.ProLayout;
using Gardener.Client.AntDesignUi.Base.Extensions;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Core;

namespace Gardener.Client.AntDesignUi.Entry
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            #region api settings
            builder.AddApiSetting();
            #endregion

            #region httpclient
            builder.Services.AddScoped(sp => {
                IOptions<ApiSettings> settings = sp.GetRequiredService<IOptions<ApiSettings>>();
                return new HttpClient { BaseAddress = new Uri(settings.Value.BaseAddres) };
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
                var defaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
                if (defaultPolicy != null)
                {
                    option.DefaultPolicy = defaultPolicy;
                }
            });
            builder.Services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
            #endregion

            #region module
            builder.AddModuleLoader();
            #endregion

            #region 本地化
            builder.Services.AddAppLocalization<SharedLocalResource>();
            #endregion

            #region services

            builder.Services.AddServicesWithAttributeOfTypeFromModuleContext(new [] { typeof(Program).Assembly });
            #endregion

            #region  Mapster 配置
            builder.Services.AddTypeAdapterConfigs();
            #endregion

            #region  SignalR
            builder.AddSignalRClientManager();
            #endregion

            var host =builder.Build();
            await host.UseAppLocalization(ClientConstant.BlazorCultureKey,ClientConstant.DefaultCulture);
            await host.RunAsync();
        }
    }
}
