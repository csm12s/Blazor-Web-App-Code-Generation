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
        /// 密码加密Key
        /// </summary>
        public string PasswordEncryptKey { get; set; }
    }
}
