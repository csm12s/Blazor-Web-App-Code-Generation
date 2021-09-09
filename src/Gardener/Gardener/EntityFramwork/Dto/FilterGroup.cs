// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Gardener.EntityFramwork.Dto
{
    /// <summary>
    /// 过滤组
    /// </summary>
    public class FilterGroup
    {
        /// <summary>
        /// 获取或设置 条件集合
        /// </summary>
        public ICollection<FilterRule> Rules { get; set; } = new List<FilterRule>();

        /// <summary>
        /// 获取或设置 条件组集合
        /// </summary>
        //public ICollection<FilterGroup> Groups { get; set; } = new List<FilterGroup>();
        /// <summary>
        /// 添加规则
        /// </summary>
        public FilterGroup AddRule(FilterRule rule)
        {
            if (Rules.All(m => !m.Equals(rule)))
            {
                Rules.Add(rule);
            }

            return this;
        }
    }
}
