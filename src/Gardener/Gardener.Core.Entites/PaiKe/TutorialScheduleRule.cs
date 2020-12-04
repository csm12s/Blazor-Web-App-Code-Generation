// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Enums;

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
