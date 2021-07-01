// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 搜索排序
    /// </summary>
    public class SearchSort
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 0 asc 
        /// 1 desc
        /// </summary>
        public SearchSortType SortType { get; set; } = SearchSortType.Desc;
    }
    /// <summary>
    /// 排序类型
    /// </summary>
    public enum SearchSortType
    { 
        /// <summary>
        /// 倒序
        /// </summary>
        Desc,
        /// <summary>
        /// 正序
        /// </summary>
        Asc
    }
}
