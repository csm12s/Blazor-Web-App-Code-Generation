// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.ConfigurableOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Core
{
    /// <summary>
    /// 系统选项
    /// </summary>
    public class SystemOptions: IConfigurableOptions
    {
        /// <summary>
        /// 超级管理员角色Id
        /// </summary>
        public int SuperAdministratorRoleId { get; set; }
    }
}
