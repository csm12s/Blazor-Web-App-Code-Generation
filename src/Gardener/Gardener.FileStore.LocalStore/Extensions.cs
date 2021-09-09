// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.FileStore;
using Gardener.FileStore.LocalStore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileLocalStore(this IServiceCollection services)
        {
            services.AddScoped<IFileStoreService, LocalFileStoreService>();
            services
               .AddConfigurableOptions<LocalFileStoreSettings>();

            return services;
        }
    }
}
