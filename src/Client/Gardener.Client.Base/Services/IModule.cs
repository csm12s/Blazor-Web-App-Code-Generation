// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Services
{
    /// <summary>
    /// 模块定义
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        Task Load();

        /// <summary>
        /// 获取自动注册的组件
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModuleComponent> GetAutoRegisterComponents();
    }
}
