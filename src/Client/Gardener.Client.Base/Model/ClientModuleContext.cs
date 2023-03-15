// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 客户端模块对象
    /// </summary>
    public class ClientModuleContext
    {
        /// <summary>
        /// 模块dll
        /// </summary>
        public List<string>? ModeuleDlls { get; set; }
        /// <summary>
        /// 模块Assembly集合
        /// </summary>
        public Assembly[]? ModeuleAssemblies { get; set; }
    }
}
