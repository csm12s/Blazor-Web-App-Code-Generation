// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigService : ISystemConfigService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCopyright()
        {
            return GetSystemConfig().Copyright;
        }

        public SystemConfig GetSystemConfig()
        {
            return new SystemConfig {
            
                Copyright= "2002 园丁系统",
                SystemName= "园丁系统",
                SystemDescription= "园丁系统 是最牛逼的教育管理系统"

            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSystemName()
        {
            return GetSystemConfig().SystemName;
        }
    }
}
