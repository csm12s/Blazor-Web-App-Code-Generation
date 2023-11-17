// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="selector"></param>
        public ModuleComponent(Type component, string selector)
        {
            Component = component;
            Selector = selector;
        }


        /// <summary>
        /// 
        /// </summary>
        public Type Component { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Selector { get; set; }
    }
}
