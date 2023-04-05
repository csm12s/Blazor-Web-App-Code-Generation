// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace Gardener.Cache
{
    /// <summary>
    /// CacheOptions
    /// </summary>
    public class CacheOptions : IConfigurableOptions
    {
        /// <summary>
        /// 使用缓存类型
        /// Memory 内存
        /// SqlServer SqlServer
        /// Redis Redis
        /// </summary>
        public string Type { get; set; } = "Memory";
        /// <summary>
        /// SqlServerCacheOptions
        /// </summary>
        public SqlServerCacheOptions? SqlServer { get; set; }
        /// <summary>
        /// RedisCacheOptions
        /// </summary>
        public RedisCacheOptions? Redis { get; set; }
        //public NCacheOptions NCache { get; set; }
    }
    /// <summary>
    /// SqlServerCacheOptions
    /// </summary>
    public class SqlServerCacheOptions
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        public string ConnectionString { get; set; } = null!;
        /// <summary>
        /// SchemaName
        /// </summary>
        public string SchemaName { get; set; } = null!;
        /// <summary>
        /// TableName
        /// </summary>
        public string TableName { get; set; } = null!;
    }
    /// <summary>
    /// RedisCacheOptions
    /// </summary>
    public class RedisCacheOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Configuration { get; set; } = null!;
        /// <summary>
        /// 键名前缀
        /// </summary>
        public string InstanceName { get; set; } = null!;
    }
    /// <summary>
    /// NCacheOptions
    /// </summary>
    public class NCacheOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string CacheName { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public bool EnableLogs { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public bool ExceptionsEnabled { get; set; } = true;
    }
}
