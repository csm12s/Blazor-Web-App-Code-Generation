// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Gardener.Application.Dtos;
using System;
using System.Linq.Expressions;

namespace Gardener.Core.DataFilter
{
    /// <summary>
    /// 查询表达式服务
    /// </summary>
    public class FilterService : IFilterService, ISingleton
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 初始化一个<see cref="FilterService"/>类型的新实例
        /// </summary>
        public FilterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Implementation of IFilterService

        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="group">查询条件组，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        public virtual Expression<Func<T, bool>> GetExpression<T>(FilterGroup group)
        {
            return FilterHelper.GetExpression<T>(group);

        }

        /// <summary>
        /// 获取指定查询条件的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="rule">查询条件，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        public virtual Expression<Func<T, bool>> GetExpression<T>(FilterRule rule)
        {
            return FilterHelper.GetExpression<T>(rule);
        }
        #endregion
    }
}
