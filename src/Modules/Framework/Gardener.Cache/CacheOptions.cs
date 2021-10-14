// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace Gardener.Cache
{
    public class CacheOptions : IConfigurableOptions
    {
        /// <summary>
        /// 使用缓存类型
        /// Memory 内存
        /// SqlServer SqlServer
        /// Redis Redis
        /// </summary>
        public string Type { get; set; } = "Memory";
        public SqlServerCacheOptions SqlServer { get; set; }
        public RedisCacheOptions Redis { get; set; }
        //public NCacheOptions NCache { get; set; }
    }

    public class SqlServerCacheOptions
    {
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }
    public class RedisCacheOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Configuration { get; set; }
        /// <summary>
        /// 键名前缀
        /// </summary>
        public string InstanceName { get; set; }
    }
    public class NCacheOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string CacheName { get; set; }
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
