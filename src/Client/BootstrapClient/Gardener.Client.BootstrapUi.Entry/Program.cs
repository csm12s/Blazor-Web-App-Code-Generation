using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.BootstrapUi.Base;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 增加 BootstrapBlazor 组件
builder.Services.AddBootstrapBlazor(op =>
{
    // 设置组件默认使用中文
    op.DefaultCultureInfo = "zh";
});

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

#region module
builder.AddModuleLoader();
#endregion

#region 本地化
builder.Services.AddLocalization();
builder.Services.AddCulture<SharedLocalResource>();
#endregion

#region services

builder.Services.AddServicesWithAttributeOfTypeFromModuleContext(new[] { typeof(Program).Assembly });
#endregion

#region  SignalR
builder.AddSignalRClientManager();
#endregion

var host = builder.Build();
host=await host.UseCulture(ClientConstant.BlazorCultureKey, ClientConstant.DefaultCulture);
await host.RunAsync();

await builder.Build().RunAsync();
