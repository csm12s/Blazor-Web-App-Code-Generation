﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Base;

public static class LinqExtension
{
    /// <summary>
    /// Only use PageIndex and PageSize to get a paged list
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
