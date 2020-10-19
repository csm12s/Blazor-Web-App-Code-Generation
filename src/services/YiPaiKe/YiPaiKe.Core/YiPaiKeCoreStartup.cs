using Fur;
using Microsoft.Extensions.DependencyInjection;

namespace YiPaiKe.Core
{
    /// <summary>
    /// 
    /// </summary>
    [AppStartup(800)]
    public sealed class YiPaiKeCoreStartup : AppStartup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPolicyAuthorization<JWTAuthorizationHandler>(options =>
            {
                options.AddJWTAuthorization();
            });
        }
    }
}