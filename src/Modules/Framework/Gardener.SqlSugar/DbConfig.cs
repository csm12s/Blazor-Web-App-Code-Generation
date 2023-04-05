namespace Gardener.Sugar
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// 数据库编号
        /// </summary>
        public string DbNumber { get; set; } = null!;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; } = null!;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbString { get; set; } = null!;
    }
}
