// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EntityFramwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gardener.EntityFramwork
{
    /// <summary>
    /// 定义过滤表达式功能
    /// </summary>
    public interface IDynamicFilterService
    {
        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="group">查询条件组，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        Expression<Func<T, bool>> GetExpression<T>(List<FilterGroup> groups);
    }
}
