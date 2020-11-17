using Gardener.Client.Services;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
            builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
            builder.Services.AddAuthorizationCore(option =>
            {
                //option.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
                option.AddPolicy("default", a => a.RequireAuthenticatedUser());
                option.DefaultPolicy = option.GetPolicy("default");
            });
            

            #region api services
            builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();

            builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
            #endregion
  
            await builder.Build().RunAsync();
        }
    }
}
