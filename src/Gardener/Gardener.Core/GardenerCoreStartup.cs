// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Core.FileStore;
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
            services.AddConfigurableOptions<JwtRefreshTokenSettings>()
                .AddConfigurableOptions<LocalFileStoreSettings>()
                .AddRemoteRequest();
        }
    }
}