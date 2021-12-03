// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// 过滤条件
    /// </summary>
    public enum FilterCondition
    {

        /// <summary>
        /// 并且
        /// </summary>
        [Code("and")]
        [Description("并且")]
        And = 1,

        /// <summary>
        /// 或者
        /// </summary>
        [Code("or")]
        [Description("或者")]
        Or = 2
    }
}
