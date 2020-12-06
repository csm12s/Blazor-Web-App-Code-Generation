// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
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
    }
}
