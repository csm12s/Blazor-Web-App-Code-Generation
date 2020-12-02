using Gardener.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign.Pro.Layout;
using Gardener.Client.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Gardener.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44323/api/") });

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
                option.AddPolicy(AuthConstant.ClientPageResourcePolicy, a => a.Requirements.Add(new ClientPageAuthorizationRequirement()));
                option.DefaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
            });

            builder.Services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, ClientPageResourceAuthorizationHandler>();
            #endregion


            #region 本地化
            builder.Services.AddLocalization(option =>
            {
                option.ResourcesPath = "Resources";
            });
            #endregion

            #region api services
            builder.Services.AddScoped<JsTool>();
            builder.Services.AddScoped<HttpClientManager>();
            builder.Services.AddScoped<IApiCaller, ApiCaller>();
            builder.Services.AddScoped<ILogger, ConsoleLogger>();
            builder.Services.AddScoped<IApiErrorNotifier, ApiErrorNotifier>();

            builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();
            builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IResourceService, ResourceService>();

            #endregion

            var host = await builder
                .Build()
                .UseCulture();
            await host.RunAsync();
        }
    }
}
