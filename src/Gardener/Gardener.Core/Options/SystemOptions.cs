// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

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
