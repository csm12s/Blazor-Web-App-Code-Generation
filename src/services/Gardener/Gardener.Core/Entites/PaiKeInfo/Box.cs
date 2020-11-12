// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Gardener.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
