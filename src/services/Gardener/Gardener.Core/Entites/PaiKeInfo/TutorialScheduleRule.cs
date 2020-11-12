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
    /// 上课安排规则
    /// </summary>
    public class TutorialScheduleRule:Entity
    {
        /// <summary>
        /// 被规则对象
        /// </summary>
        public string ForObject { get; set; }

        /// <summary>
        /// 被规则位置
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 排序规则
        /// </summary>
        public RuleSort RuleSort { get; set; }
    }
}
