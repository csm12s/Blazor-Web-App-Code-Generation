// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;

namespace Gardener.Base
{
    /// <summary>
    /// 搜索排序
    /// </summary>
    public class ListSortDirection
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 0 asc 
        /// 1 desc
        /// </summary>
        public ListSortType SortType { get; set; } = ListSortType.Desc;
    }
    
}
