// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using System;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 系统配置-暂时写死吧
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
        /// <summary>
        /// 获取系统配置
        /// 可放置到数据库中
        /// </summary>
        /// <returns></returns>
        public SystemConfig GetSystemConfig()
        {

            return new SystemConfig {
            
                Copyright= DateTime.Now.Year+ " 园丁",
                SystemName= "园丁",
                SystemDescription= "园丁,是个很简单的管理系统"

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
