// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Services;
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
        /// 模块Assembly集合
        /// </summary>
        private ICollection<Assembly> modeuleAssemblies = new List<Assembly>();
        /// <summary>
        /// 添加模块Assembly
        /// </summary>
        /// <param name="module"></param>
        public void Add(Assembly module)
        {
            modeuleAssemblies.Add(module);
        }
        /// <summary>
        /// 模块
        /// </summary>
        private ICollection<IModule> _modules=new List<IModule>();
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="module"></param>
        public void Add(IModule module)
        {
            _modules.Add(module);
        }
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <returns></returns>
        public ICollection<IModule> GetModules()
        {
            return _modules;
        }
    }
}
