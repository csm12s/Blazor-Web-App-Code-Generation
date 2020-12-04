// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 年级
    /// </summary>
    public class Grade:Entity
    {
        /// <summary>
        /// 年级
        /// </summary>
        public int Number { get; set; }
    }
}
