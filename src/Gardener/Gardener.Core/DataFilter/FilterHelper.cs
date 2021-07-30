// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using Gardener.Application.Dtos;
using Gardener.Common;
using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapster;

namespace Gardener.Core.DataFilter
{
    /// <summary>
    /// 查询表达式辅助操作类
    /// </summary>
    public static class FilterHelper
    {
        #region 字段

        private static readonly Dictionary<FilterOperate, Func<Expression, Expression, Expression>> ExpressionDict =
            new Dictionary<FilterOperate, Func<Expression, Expression, Expression>>
            {
                {
                    FilterOperate.Equal, Expression.Equal
                },
                {
                    FilterOperate.NotEqual, Expression.NotEqual
                },
                {
                    FilterOperate.Less, Expression.LessThan
                },
                {
                    FilterOperate.Greater, Expression.GreaterThan
                },
                {
                    FilterOperate.LessOrEqual, Expression.LessThanOrEqual
                },
                {
                    FilterOperate.GreaterOrEqual, Expression.GreaterThanOrEqual
                },
                {
                    FilterOperate.StartsWith,
                    (left, right) =>
                    {
                        if (left.Type != typeof(string))
                        {
                            throw new NotSupportedException("“StartsWith”比较方式只支持字符串类型的数据");
                        }
                        return Expression.Call(left,
                            typeof(string).GetMethod("StartsWith", new[] { typeof(string) })
                            ?? throw new InvalidOperationException($"名称为“StartsWith”的方法不存在"),
                            right);
                    }
                },
                {
                    FilterOperate.EndsWith,
                    (left, right) =>
                    {
                        if (left.Type != typeof(string))
                        {
                            throw new NotSupportedException("“EndsWith”比较方式只支持字符串类型的数据");
                        }
                        return Expression.Call(left,
                            typeof(string).GetMethod("EndsWith", new[] { typeof(string) })
                            ?? throw new InvalidOperationException($"名称为“EndsWith”的方法不存在"),
                            right);
                    }
                },
                {
                    FilterOperate.Contains,
                    (left, right) =>
                    {
                        if (left.Type != typeof(string))
                        {
                            throw new NotSupportedException("“Contains”比较方式只支持字符串类型的数据");
                        }
                        return Expression.Call(left,
                            typeof(string).GetMethod("Contains", new[] { typeof(string) })
                            ?? throw new InvalidOperationException($"名称为“Contains”的方法不存在"),
                            right);
                    }
                },
                {
                    FilterOperate.NotContains,
                    (left, right) =>
                    {
                        if (left.Type != typeof(string))
                        {
                            throw new NotSupportedException("“NotContains”比较方式只支持字符串类型的数据");
                        }
                        return Expression.Not(Expression.Call(left,
                            typeof(string).GetMethod("Contains", new[] { typeof(string) })
                            ?? throw new InvalidOperationException($"名称为“Contains”的方法不存在"),
                            right));
                    }
                }
            };

        #endregion

        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="group">查询条件组，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        public static Expression<Func<T, bool>> GetExpression<T>(FilterGroup group)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "m");
            Expression body = GetExpressionBody(param, group);
            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(body, param);
            return expression;
        }

        /// <summary>
        /// 获取指定查询条件的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="rule">查询条件，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        public static Expression<Func<T, bool>> GetExpression<T>(FilterRule rule)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "m");
            Expression body = GetExpressionBody(param, rule);
            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(body, param);
            return expression;
        }

        /// <summary>
        /// 把查询操作的枚举表示转换为操作码
        /// </summary>
        /// <param name="operate">查询操作的枚举表示</param>
        public static string ToOperateCode(this FilterOperate operate)
        {
            return EnumHelper.GetEnumCode(operate);
        }

        /// <summary>
        /// 获取操作码的查询操作枚举表示
        /// </summary>
        /// <param name="code">操作码</param>
        /// <returns></returns>
        public static FilterOperate GetFilterOperate(string code)
        {
            Type type = typeof(FilterOperate);
            MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.Static);
            foreach (MemberInfo member in members)
            {
                FilterOperate operate = (FilterOperate)Enum.Parse(typeof(FilterOperate), member.Name);
                if (operate.ToOperateCode() == code)
                {
                    return operate;
                }
            }
            throw new NotSupportedException("获取操作码的查询操作枚举表示时不支持代码：" + code);
        }

        #region 私有方法

        private static Expression GetExpressionBody(ParameterExpression param, FilterGroup group)
        {

            //如果无条件或条件为空，直接返回 true表达式
            if (group == null || (group.Rules.Count == 0 && group.Groups.Count == 0))
            {
                return Expression.Constant(true);
            }
            List<Expression> bodies = new List<Expression>();
            bodies.AddRange(group.Rules.Select(rule => GetExpressionBody(param, rule)));
            bodies.AddRange(group.Groups.Select(subGroup => GetExpressionBody(param, subGroup)));

            if (group.Operate == FilterOperate.And)
            {
                return bodies.Aggregate(Expression.AndAlso);
            }
            if (group.Operate == FilterOperate.Or)
            {
                return bodies.Aggregate(Expression.OrElse);
            }
            throw Oops.Oh(ExceptionCode.FILTER_GROUP_OPERATE_ERROR);
        }

        private static Expression GetExpressionBody(ParameterExpression param, FilterRule rule)
        {
            // if (rule == null || rule.Value == null || string.IsNullOrEmpty(rule.Value.ToString()))
            if (rule == null)
            {
                return Expression.Constant(true);
            }
            LambdaExpression expression = GetPropertyLambdaExpression(param, rule);
            if (expression == null)
            {
                return Expression.Constant(true);
            }
            Expression constant = ChangeTypeToExpression(rule, expression.Body.Type);
            return ExpressionDict[rule.Operate](expression.Body, constant);
        }

        private static LambdaExpression GetPropertyLambdaExpression(ParameterExpression param, FilterRule rule)
        {
            string[] propertyNames = rule.Field.Split('.');
            Expression propertyAccess = param;
            Type type = param.Type;
            for (var index = 0; index < propertyNames.Length; index++)
            {
                string propertyName = propertyNames[index];
                PropertyInfo property = type.GetProperty(propertyName);
                if (property == null)
                {
                    throw Oops.Oh(ExceptionCode.FIELD_IN_TYPE_NOT_FOUND, rule.Field, type.FullName);
                }

                type = property.PropertyType;
                //验证最后一个属性与属性值是否匹配
                if (index == propertyNames.Length - 1)
                {
                    bool flag = CheckFilterRule(type, rule);
                    if (!flag)
                    {
                        return null;
                    }
                }

                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }
            return Expression.Lambda(propertyAccess, param);
        }

        /// <summary>
        /// 验证最后一个属性与属性值是否匹配 
        /// </summary>
        /// <param name="type">最后一个属性</param>
        /// <param name="rule">条件信息</param>
        /// <returns></returns>
        private static bool CheckFilterRule(Type type, FilterRule rule)
        {
            if (rule.Value == null || rule.Value.ToString() == string.Empty)
            {
                rule.Value = null;
            }

            if (rule.Value == null && (type == typeof(string) || type.IsNullableType()))
            {
                return rule.Operate == FilterOperate.Equal || rule.Operate == FilterOperate.NotEqual;
            }

            if (rule.Value == null)
            {
                return !type.IsValueType;
            }
            return true;
        }

        private static Expression ChangeTypeToExpression(FilterRule rule, Type conversionType)
        {
            object value = rule.Value.Adapt(rule.Value.GetType(),conversionType);
            return Expression.Constant(value, conversionType);
        }

        #endregion
    }
}
