// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 安全服务
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddConfigurableOptions<CacheOptions>();
            using var serviceProvider = services.BuildServiceProvider();
            var cacheOptions = serviceProvider.GetService<IOptions<CacheOptions>>().Value;
            if (cacheOptions.Type.Equals("Memory"))
            {
                services.AddDistributedMemoryCache();
            }else if (cacheOptions.Type.Equals("SqlServer"))
            {
                if (cacheOptions.SqlServer == null) 
                {
                    throw new OptionsValidationException("Cache:SqlServer",typeof(SqlServerCacheOptions),new[] { "请配置SqlServer缓存配置" });
                }
                string connectionString = App.Configuration[cacheOptions.SqlServer.ConnectionString];
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
