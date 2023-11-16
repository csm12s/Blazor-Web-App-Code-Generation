// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 客户端模块对象
    /// </summary>
    public class ClientModuleManager
    {
        /// <summary>
        /// 模块Assembly集合
        /// </summary>
        public Assembly[] ModeuleAssemblies
        {
            get
            {
                return modeuleAssemblies.ToArray();
            }
        }

        /// <summary>
        /// 模块
        /// </summary>
        private ICollection<Assembly> modeuleAssemblies { get; set; } = new List<Assembly>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        public void Add(Assembly module)
        {
            modeuleAssemblies.Add(module);
        }
    }
}
