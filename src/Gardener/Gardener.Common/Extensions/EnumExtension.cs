// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.Common.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 枚举描述转字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<T, string> EnumToDictionary<T>()
        {
            Dictionary<T, string> dic = new Dictionary<T, string>();
            if (!typeof(T).IsEnum)
            {
                return dic;
            }
            string desc = string.Empty;
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var attrs = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    DescriptionAttribute descAttr = attrs[0] as DescriptionAttribute;
                    desc = descAttr.Description;
                }
                dic.Add((T)item, desc);
            }
            return dic;
        }
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum item)
        {
            var attrs = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                DescriptionAttribute descAttr = attrs[0] as DescriptionAttribute;
                return descAttr.Description;
            }
            return string.Empty;
        }
    }
}
