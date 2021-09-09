// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Gardener.EntityFramwork.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 排序集合
        /// </summary>
        public List<ListSortDirection> OrderConditions { get; set; } = new List<ListSortDirection>();
        /// <summary>
        /// 查询条件组
        /// </summary>
        //public FilterGroup FilterGroup { get; set; } = new FilterGroup() { Condition=Enums.FilterCondition.And };
        public List<FilterGroup> FilterGroups { get; set; } = new List<FilterGroup>() ;

    }
}
