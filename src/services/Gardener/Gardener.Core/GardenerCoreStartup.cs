using Fur;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Core
{
    /// <summary>
    /// 
    /// </summary>
    [AppStartup(800)]
    public sealed class GardenerCoreStartup : AppStartup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppAuthorization<JWTAuthorizationHandler>(options =>
            {
                options.AddJWTAuthorization();
            });
        }
    }
}