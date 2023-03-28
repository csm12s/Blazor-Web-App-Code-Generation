using Furion.ConfigurableOptions;

namespace Gardener.Sugar
{
    /// <summary>
    /// 连接字符串配置
    /// </summary>
    public class ConnectionStringsOptions : IConfigurableOptions
    {
        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public string Default { get; set; } = null!;
        /// <summary>
        /// 默认数据库编号
        /// </summary>
        public string DefaultDbNumber { get; set; } = null!;
        /// <summary>
        /// 默认数据库类型
        /// </summary>
        public string DefaultDbType { get; set; } = null!;
        /// <summary>
        /// 默认数据库连接字符串
        /// </summary>
        public string DefaultDbString { get; set; } = null!;
        /// <summary>
        /// 业务库集合
        /// </summary>
        public List<DbConfig>? DbConfigs { get; set; }
    }

    /// <summary>
    /// 默认连接配置
    /// </summary>
    public class DefaultDbSettingsOptions : IConfigurableOptions
    {
        /// <summary>
        /// 数据库提供者
        /// </summary>
        public string DbProvider { get; set; } = null!;
        /// <summary>
        /// 初始化SugarDb
        /// </summary>
        public bool InitSugarDb { get; set; }
        /// <summary>
        /// 初始化Db
        /// </summary>
        public bool InitDb { get; set; }
        /// <summary>
        /// 自动迁移
        /// </summary>
        public bool AutoMigration { get; set; }
        /// <summary>
        /// 迁移程序
        /// </summary>
        public string? MigrationAssemblyName { get; set; }
    }
}
