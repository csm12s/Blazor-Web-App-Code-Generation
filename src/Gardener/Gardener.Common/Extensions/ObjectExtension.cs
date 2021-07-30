// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Gardener.Common
{
    /// <summary>
    /// Object拓展方法，.Net4.0以上
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 根据属性名获取属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TValue">属性类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <returns>属性的值</returns>
        public static TValue GetPropertyValue<T,TValue>(this T t, string name)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null) return default(TValue);
            
            var param_obj = Expression.Parameter(typeof(T));
            var param_val = Expression.Parameter(typeof(TValue));

            //转成真实类型，防止Dynamic类型转换成object
            var body_obj = Expression.Convert(param_obj, type);

            var body = Expression.Property(body_obj, p);
            var getValue = Expression.Lambda<Func<T, TValue>>(body, param_obj).Compile();
            return getValue(t);
        }

        /// <summary>
        /// 根据属性名称设置属性的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TValue">属性类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性的值</param>
        public static bool SetPropertyValue<T, TValue>(this T t, string name, TValue value)
        {
            Type type = t.GetType();
            PropertyInfo p = type.GetProperty(name);
            if (p == null) return false;
            
            var param_obj = Expression.Parameter(type);
            var param_val = Expression.Parameter(typeof(TValue));
            var body_obj = Expression.Convert(param_obj, type);
            var body_val = Expression.Convert(param_val, p.PropertyType);

            //获取设置属性的值的方法
            var setMethod = p.GetSetMethod(true);

            //如果只是只读,则setMethod==null
            if (setMethod != null)
            {
                var body = Expression.Call(param_obj, p.GetSetMethod(), body_val);
                var setValue = Expression.Lambda<Action<T, TValue>>(body, param_obj, param_val).Compile();
                setValue(t, value);

                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断当前值是否介于指定范围内
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 动态类型对象 </param>
        /// <param name="start"> 范围起点 </param>
        /// <param name="end"> 范围终点 </param>
        /// <param name="leftEqual"> 是否可等于上限（默认等于） </param>
        /// <param name="rightEqual"> 是否可等于下限（默认等于） </param>
        /// <returns> 是否介于 </returns>
        public static bool IsBetween<T>(this IComparable<T> value, T start, T end, bool leftEqual = true, bool rightEqual = true) where T : IComparable
        {
            bool flag = leftEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
            return flag && (rightEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0);
        }

        /// <summary>
        /// 判断当前值是否介于指定范围内
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 动态类型对象 </param>
        /// <param name="min">范围小值</param>
        /// <param name="max">范围大值</param>
        /// <param name="minEqual">是否可等于小值（默认等于）</param>
        /// <param name="maxEqual">是否可等于大值（默认等于）</param>
        public static bool IsInRange<T>(this IComparable<T> value, T min, T max, bool minEqual = true, bool maxEqual = true) where T : IComparable
        {
            bool flag = minEqual ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0;
            return flag && (maxEqual ? value.CompareTo(max) <= 0 : value.CompareTo(max) < 0);
        }


        #region 对象转成字典
        /// <summary>
        /// 对象转换为字典
        /// </summary>
        /// <param name="obj">待转化的对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToMap(this object obj)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            Type t = obj.GetType(); // 获取对象对应的类， 对应的类型

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性

            foreach (PropertyInfo p in pi)
            {
                MethodInfo m = p.GetGetMethod();

                if (m != null && m.IsPublic)
                {
                    // 进行判NULL处理
                    if (m.Invoke(obj, new object[] { }) != null)
                    {
                        map.Add(p.Name, m.Invoke(obj, new object[] { }).ToString()); // 向字典添加元素
                    }
                }
            }
            return map;
        }
        #endregion

    }
}
