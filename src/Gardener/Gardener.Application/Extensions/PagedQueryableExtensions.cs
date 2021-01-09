// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Application
{
    /// <summary>
    /// 分部拓展类
    /// </summary>
    public static class PagedQueryableExtensions
    {
        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Dtos.PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20)
            where TEntity : class, new()
        {
            var totalCount = entities.Count();
            var items = entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new Dtos.PagedList<TEntity>
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
        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="pagedSearchBaseInfo"></param>
        /// <returns></returns>
        public static Dtos.PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PagedSearchBaseInfo pagedSearchBaseInfo)
            where TEntity : class, new()
        {
            int pageIndex = pagedSearchBaseInfo.PageIndex;
            int pageSize = pagedSearchBaseInfo.PageSize;

            return entities.ToPagedList<TEntity>(pageIndex, pageSize);
        }
        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Dtos.PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
            where TEntity : class, new()
        {
            var totalCount = await entities.CountAsync(cancellationToken);
            var items = await entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new Dtos.PagedList<TEntity>
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
        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="pagedSearchBaseInfo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Dtos.PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> entities, PagedSearchBaseInfo pagedSearchBaseInfo, CancellationToken cancellationToken = default)
            where TEntity : class, new()
        {
            int pageIndex = pagedSearchBaseInfo.PageIndex;
            int pageSize = pagedSearchBaseInfo.PageSize;

            return await entities.ToPagedListAsync<TEntity>(pageIndex, pageSize, cancellationToken);
        }
        /// <summary>
        /// 多字段排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderConditions"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderConditions<T>(this IQueryable<T> query,SearchSort[] orderConditions)
        {
            if (orderConditions == null || !orderConditions.Any()) return query; 
            var parameter = Expression.Parameter(typeof(T), "o");
            for (var i= 0;i < orderConditions.Length;i++)
            {
                var orderinfo = orderConditions[i];
                var t = typeof(T);
                var property = t.GetProperty(orderinfo.FieldName);
                //创建一个访问属性的表达式
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                string OrderName = i > 0? "ThenBy" : "OrderBy";
                OrderName = OrderName + (orderinfo.SortType.Equals(SearchSortType.Desc) ? "Descending" : "");
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                query = query.Provider.CreateQuery<T>(resultExp);
            }
            return query;
        }
    }
}
