using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Base;

// 当前端需要将 Tree 转化为 PagedList 提升显示性能，会用到这个方法，
// 暂时从Gardener.Base转移到这里

/// <summary>
/// 将 Tree 转化为 PagedList 提升显示性能
/// </summary>
public static class LinqExtension
{
    /// <summary>
    /// In: PageIndex + PageSize
    /// Out: PagedList
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static PagedList<TEntity> ToPageList<TEntity>(this IEnumerable<TEntity> entities, PageRequest request)
        where TEntity : class, new()
    {
        int pageIndex = request.PageIndex;
        int pageSize = request.PageSize;

        var totalCount = entities.Count();
        var items = entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedList<TEntity>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPages = pageIndex < totalPages,
            HasPrevPages = pageIndex - 1 > 0
        };
    }
}
