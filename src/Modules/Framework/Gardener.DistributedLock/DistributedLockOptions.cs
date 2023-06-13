// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace Gardener.DistributedLock
{
    /// <summary>
    /// 分布式锁配置
    /// </summary>
    public class DistributedLockOptions : IConfigurableOptions
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = "FileSystem";

        /// <summary>
        /// Redis 配置
        /// </summary>
        public RedisDistributedLockOptions? Redis { get; set; }

        /// <summary>
        /// FileSystem 配置
        /// </summary>
        public FileSystemDistributedLockOptions? FileSystem { get; set; }

    }
    /// <summary>
    /// Redis Distributed Lock
    /// </summary>
    public class RedisDistributedLockOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Configuration { get; set; } = null!;
        /// <summary>
        /// Db Number
        /// </summary>
        public int DbNumber { get; set; } = -1;

    }
    /// <summary>
    /// File System Distributed Lock
    /// </summary>
    public class FileSystemDistributedLockOptions
    {
        /// <summary>
        /// 路径
        /// </summary>
        /// <remarks>
        /// 为空时，是根目录下 DistributedLock 目录
        /// </remarks>
        public string? Path { get; set; }
    }
}
