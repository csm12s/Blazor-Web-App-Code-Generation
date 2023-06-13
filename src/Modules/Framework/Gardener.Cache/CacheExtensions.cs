// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Cache;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 安全服务
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 添加缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>说明：https://gitee.com/hgflydream/Gardener/wikis/%E5%BF%AB%E9%80%9F%E5%85%A5%E9%97%A8/%E6%9C%8D%E5%8A%A1%E7%AB%AF%E7%BC%93%E5%AD%98</para>
        /// <para>支持更多缓存请查阅：http://furion.baiqian.ltd/docs/cache/</para>
        /// </remarks>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddConfigurableOptions<CacheOptions>();
            using var serviceProvider = services.BuildServiceProvider();
            if(serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
            var cacheOptionProvider = serviceProvider.GetService<IOptions<CacheOptions>>();
            if (cacheOptionProvider == null)
            {
                throw new ArgumentNullException(nameof(CacheOptions));
            }
            var cacheOptions = cacheOptionProvider.Value;
            if (cacheOptions.Type.Equals("Memory"))
            {
                services.AddDistributedMemoryCache();
            }else if (cacheOptions.Type.Equals("SqlServer"))
            {
                if (cacheOptions.SqlServer == null) 
                {
                    throw new OptionsValidationException("Cache:SqlServer",typeof(SqlServerCacheOptions),new[] { "请配置SqlServer缓存配置" });
                }
                string? connectionString = App.Configuration[cacheOptions.SqlServer.ConnectionString];
                services.AddDistributedSqlServerCache(options => { 
                    options.ConnectionString = connectionString;
                    options.SchemaName = cacheOptions.SqlServer.SchemaName; 
                    options.TableName = cacheOptions.SqlServer.TableName; }
                );
            }else if (cacheOptions.Type.Equals("Redis"))
            {
                if (cacheOptions.Redis == null) 
                {
                    throw new OptionsValidationException("Cache:Redis", typeof(SqlServerCacheOptions),new[] { "请配置Redis缓存配置" });
                }
                services.AddStackExchangeRedisCache(options => {    
                    options.Configuration = cacheOptions.Redis.Configuration;    
                    options.InstanceName = cacheOptions.Redis.InstanceName;
                });
            }
            services.AddSingleton<ICache, CacheImpl>();
            return services;
        }
    }
}
