// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.FileStore;
using Gardener.FileStore.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 文件存储扩展
    /// </summary>
    public static class FileStoreExtensions
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileLocalStore(this IServiceCollection services)
        {
            //配置
            services.AddConfigurableOptions<FileStoreSettings>();

            //文件存储服务工厂
            services.AddSingleton<IFileStoreServiceFactory, FileStoreServiceFactory>();

            //默认的文件存储服务
            services.AddSingleton<IFileStoreService>(serviceProvider => {
                var fileStoreServiceFactory = serviceProvider.GetRequiredService<IFileStoreServiceFactory>();
                var service = fileStoreServiceFactory.GetDefaultFileStoreService();
                if(service == null)
                {
                    throw new ArgumentNullException("DefaultFileStoreService");
                }
                return service;
            });
            return services;
        }
    }
}
