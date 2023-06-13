// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Medallion.Threading;
using Medallion.Threading.FileSystem;
using Medallion.Threading.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Gardener.DistributedLock
{
    /// <summary>
    /// 分布式锁扩展
    /// </summary>
    public static class DistributedLockExtensions
    {
        /// <summary>
        /// 启用分布式锁扩展
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// <para>说明：https://gitee.com/hgflydream/Gardener/wikis/%E5%BF%AB%E9%80%9F%E5%85%A5%E9%97%A8/%E5%88%86%E5%B8%83%E5%BC%8F%E9%94%81</para>
        /// <para>支持更多分布式锁请查阅：https://github.com/madelson/DistributedLock/tree/master/docs</para>
        /// </remarks>
        public static IServiceCollection AddDistributedLock(this IServiceCollection services)
        {
            services.AddConfigurableOptions<DistributedLockOptions>();

            using var serviceProvider = services.BuildServiceProvider();
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            var optionProvider = serviceProvider.GetService<IOptions<DistributedLockOptions>>();
            if (optionProvider == null)
            {
                throw new ArgumentNullException(nameof(DistributedLockOptions));
            }
            DistributedLockOptions options = optionProvider.Value;
            switch (options.Type)
            {
                case "FileSystem":
                    string path = Path.Combine(App.HostEnvironment.ContentRootPath, "DistributedLock");

                    if (options.FileSystem != null && !string.IsNullOrEmpty(options.FileSystem.Path))
                    {
                        path = options.FileSystem.Path;
                    }
                    DirectoryInfo lockFileDirectory = new DirectoryInfo(path);
                    services.AddSingleton<IDistributedLockProvider>(_ => new FileDistributedSynchronizationProvider(lockFileDirectory));
                    break;
                case "Redis":
                    IDatabase? redis = null;
                    if (options.Redis != null)
                    {
                        //优先使用配置
                        ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options.Redis.Configuration);
                        redis = connection.GetDatabase(options.Redis.DbNumber);
                    }
                    else
                    {
                        //没有配置，尝试使用已注册的IDatabase
                        redis = serviceProvider.GetService<IDatabase>();
                    }
                    if (redis == null)
                    {
                        throw new ArgumentNullException(nameof(RedisDistributedLockOptions));
                    }
                    services.AddSingleton<IDistributedLockProvider>(_ => new RedisDistributedSynchronizationProvider(redis));
                    break;
            }
            return services;
        }
    }
}