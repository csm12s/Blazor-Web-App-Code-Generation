// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Attributes
{
    /// <summary>
    /// 排序
    /// </summary>
    public class OrderAttribute : Attribute
    {
        /// <summary>
        /// 排序
        /// </summary>
        public OrderAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
    }
}
