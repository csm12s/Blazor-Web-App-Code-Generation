using Gardener.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign.Pro.Layout;
using Gardener.Client.Extensions;

namespace Gardener.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new RestClient("https://localhost:44323/api"));

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44323/api/") });

            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            //Console.WriteLine("==================================ProSettings>" + System.Text.Json.JsonSerializer.Serialize(builder.Configuration.GetSection("ProSettings")));
            //builder.Services.AddOptions<ProSettings>("ProSettings");
            //builder.Services.Configure<ProSettings>("ProSettings", config =>
            //{

            //    Console.WriteLine("==================================ProSettings2>" + System.Text.Json.JsonSerializer.Serialize(config));
            //});

            builder.Services.AddScoped<IAuthenticationStateManager, AuthenticationStateManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore(option =>
            {
                //option.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
                option.AddPolicy("default", a => a.RequireAuthenticatedUser());
                option.DefaultPolicy = option.GetPolicy("default");
            });
            //支持本地化
            builder.Services.AddLocalization(option => {
                option.ResourcesPath = "Resources";
            });
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
