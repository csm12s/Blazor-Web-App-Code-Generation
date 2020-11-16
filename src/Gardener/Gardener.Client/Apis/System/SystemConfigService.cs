// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Apis
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
        public string GetFooterContent()
        {
            return "园丁系统";
        }
    }
}
