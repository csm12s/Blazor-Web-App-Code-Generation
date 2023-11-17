using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gardener.Client.AntDesignUi.Entry
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //ÅäÖÃ
            builder.Services.TryAddSingleton<IConfiguration>(builder.Configuration);
            //¸ù×é¼þ
            builder.Services.TryAddSingleton(builder.RootComponents);

            #region log
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );
            #endregion

            builder.Services.Inject(builder.HostEnvironment.BaseAddress);

            var host = builder.Build();
            await host.Services.UseInject();
            await host.RunAsync();
        }
    }
}
