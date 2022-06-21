// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Gardener.Client.Base;
using Gardener.Client.Core;
using Gardener.Client.MAUI.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Gardener.Client.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
           
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            #if DEBUG
		    builder.Services.AddBlazorWebViewDeveloperTools();
            #endif

            #region 加载 Appsettings
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Gardener.Client.MAUI.appsettings.json");
            var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
            builder.Configuration.AddConfiguration(config);
            #endregion

            #region api settings
            builder.AddApiSetting();
            #endregion

            #region httpclient
            builder.Services.AddScoped(sp => {
                IOptions<ApiSettings> settings = sp.GetService<IOptions<ApiSettings>>();
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
            builder.Services.AddCulture<App>();
            #endregion

            #region module
            builder.AddModuleLoader().Wait();
            #endregion

            #region services

            builder.Services.AddServicesWithAttributeOfTypeFromModuleContext(new[] { typeof(App).Assembly });
            #endregion

            #region  Mapster 配置
            builder.Services.AddTypeAdapterConfigs();
            #endregion

            #region  SignalR
            builder.AddSignalRClientManager();
            #endregion

            return builder.Build();
        }
    }
}