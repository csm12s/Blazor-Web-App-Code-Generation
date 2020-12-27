// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class PagedSearchBaseInfo
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
        public SearchSort [] OrderConditions { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class PagedSearchBaseInfo<T>:PagedSearchBaseInfo where T : new()
    {
        /// <summary>
        /// 查询实体数
        /// </summary>
        
        public T SearchData { get; set; } = new T();
    }
}
