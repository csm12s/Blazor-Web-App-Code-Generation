// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Gardener.Common
{
    /// <summary>
    /// 枚举操作类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 枚举转list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> EnumToList<T>()
        {
            List<T> list = new List<T>();
            if (!typeof(T).IsEnum)
            {
                return list;
            }
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                list.Add((T)item);
            }
            return list;
        }
        /// <summary>
        /// 枚举转字典
        /// key 枚举 value 描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>
        /// 获取<see cref="DescriptionAttribute"/>的值，如果没有该属性，返回Name,如果标记<see cref="IgnoreOnConvertToMapAttribute"/>该项不返回
        /// </remarks>
        public static Dictionary<T, string> EnumToDictionary<T>() where T : Enum
        {
            Dictionary<T, string> dic = new Dictionary<T, string>();
            if (!typeof(T).IsEnum)
            {
                return dic;
            }
            string? desc = string.Empty;
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                //未设置Description取英文名
                desc = item.ToString() ;
                if (string.IsNullOrEmpty(desc))
                {
                    continue;
                }
                //需要忽略的
                var igAttrs = item.GetType().GetField(desc)?.GetCustomAttributes(typeof(IgnoreOnConvertToMapAttribute), true);
                if (igAttrs != null && igAttrs.Length > 0) continue;
                //枚举的Description
                var attrs = item.GetType().GetField(desc)?.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    DescriptionAttribute? descAttr = attrs[0] as DescriptionAttribute;
                    desc = descAttr?.Description;
                }
                dic.Add((T)item, desc??string.Empty);
            }
            return dic;
        }
        /// <summary>
        /// 枚举转字典
        /// key 枚举值(数值) value 描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<object, string?> EnumToDictionary(Type type)
        {
            Dictionary<object, string?> dic = new Dictionary<object, string?>();
            type = type.GetNonNullableType();
            if (!type.IsEnum)
            {
                return dic;
            }
            Type underlyingType = type.GetEnumUnderlyingType();
            string? desc = string.Empty;
            foreach (var item in Enum.GetValues(type))
            {
                string? itemStr= item.ToString();
                if (itemStr == null) continue;
                //需要忽略的
                FieldInfo? field = item.GetType().GetField(itemStr);
                if (field == null) { continue; }
                var igAttrs = field.GetCustomAttributes(typeof(IgnoreOnConvertToMapAttribute), true);
                if (igAttrs != null && igAttrs.Length > 0) continue;
                //枚举的Description
                var attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    DescriptionAttribute? descAttr = attrs[0] as DescriptionAttribute;
                    desc = descAttr?.Description;
                }
                object? value= null;
                if (underlyingType.Equals(typeof(byte)))
                { 
                    value = (byte)item;
                }if (underlyingType.Equals(typeof(short)))
                { 
                    value = (short)item;
                }else if (underlyingType.Equals(typeof(int)))
                {
                    value = (int)item;
                }else if (underlyingType.Equals(typeof(long)))
                {
                    value = (long)item;
                }
                if(value!=null)
                {
                    dic.Add(value, desc);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取枚举特性
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TAttribute? GetEnumAttribute<TAttribute>(Enum item) where TAttribute: Attribute
        {
            var itemStr = item.ToString();
            if (itemStr == null) return null;
            var attrs = item.GetType().GetField(itemStr)?.GetCustomAttributes(typeof(TAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                TAttribute? descAttr = attrs[0] as TAttribute;
                return descAttr;
            }
            return null;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string? GetEnumDescription(Enum item)
        {
            return GetEnumAttribute<DescriptionAttribute>(item)?.Description;
        }

        /// <summary>
        /// 获取枚举描述,如果没有描述返回名字
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetEnumDescriptionOrName(Enum item)
        {
            return GetEnumAttribute<DescriptionAttribute>(item)?.Description ?? item.ToString();
        }

        /// <summary>
        /// 获取枚举描述
        /// Get Enum CodeAttribute Value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string? GetEnumCode(Enum item)
        {
            var itemStr = item.ToString();
            if (itemStr == null) return null;
            var attrs = item.GetType().GetField(itemStr)?.GetCustomAttributes(typeof(CodeAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                CodeAttribute? descAttr = attrs[0] as CodeAttribute;
                return descAttr?.Code;
            }
            return null;
        }
    }
}
