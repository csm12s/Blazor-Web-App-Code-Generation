using Gardener.Client.Apis;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new RestClient("https://localhost:44323/api"));
           
            builder.Services.AddAntDesign();
            builder.Services.AddAuthorizationCore(option =>
            {
                option.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));

            });
            builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();

            builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();

            builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();

            await builder.Build().RunAsync();
        }
    }
}
