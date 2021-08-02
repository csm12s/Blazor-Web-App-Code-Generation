// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 过滤组
    /// </summary>
    public class FilterGroup
    {
        private FilterOperate _operate;
        /// <summary>
        /// 获取或设置 条件间操作方式，仅限And, Or
        /// </summary>
        public FilterOperate Operate
        {
            get { return _operate; }
            set
            {
                if (value != FilterOperate.And && value != FilterOperate.Or)
                {
                    throw new InvalidOperationException("无效的操作方式");
                }
                _operate = value;
            }
        }
        /// <summary>
        /// 获取或设置 条件集合
        /// </summary>
        public ICollection<FilterRule> Rules { get; set; } = new List<FilterRule>();

        /// <summary>
        /// 获取或设置 条件组集合
        /// </summary>
        public ICollection<FilterGroup> Groups { get; set; } = new List<FilterGroup>();
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
        /// <summary>
        /// 
        /// </summary>
        public FilterGroup ResetRule()
        {
            this.Rules = new List<FilterRule>();
            return this;
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        public FilterGroup AddRule(string field, object value, FilterOperate operate = FilterOperate.Equal)
        {
            FilterRule rule = new FilterRule() 
            {
                Field=field,
                Value=value,
                Operate=operate
            };
            return AddRule(rule);
        }
    }
}
