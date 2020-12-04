// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Enums;
using System.Collections.Generic;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 表
    /// </summary>
    public class Box : Entity
    {
        /// <summary>
        /// 星期
        /// </summary>
        public int Week { get; set; }
        /// <summary>
        /// 第几节
        /// </summary>
        public int Section { get; set; }
        /// <summary>
        /// Week和Section 合并得到的唯一no
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 所属时段
        /// </summary>
        public Frequency Frequency { get; set; }
        /// <summary>
        /// 该节点安排的课程信息
        /// </summary>
        public List<Fill> Fills { get; set; }
    }
}
