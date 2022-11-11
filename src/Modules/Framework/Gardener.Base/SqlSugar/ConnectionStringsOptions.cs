using Furion.ConfigurableOptions;
using System.Collections.Generic;

namespace Gardener.Base
{
    public class ConnectionStringsOptions : IConfigurableOptions
    {
        // Garden
        public string Default { get; set; }

        // Custom
        /// <summary>
        /// 默认数据库编号
        /// </summary>
        public string? DefaultDbNumber { get; set; }
        /// <summary>
        /// 默认数据库类型
        /// </summary>
        public string DefaultDbType { get; set; }
        /// <summary>
        /// 默认数据库连接字符串
        /// </summary>

        public string DefaultDbString { get; set; }
        /// <summary>
        /// 业务库集合
        /// </summary>
        public List<DbConfig> DbConfigs { get; set; }
    }

    public class DefaultDbSettingsOptions : IConfigurableOptions
    {
        public string DbProvider { get; set; }
        public bool InitSugarDb { get; set; }
        public bool InitDb { get; set; }
        public bool AutoMigration { get; set; }
        public string MigrationAssemblyName { get; set; }
    }
}
