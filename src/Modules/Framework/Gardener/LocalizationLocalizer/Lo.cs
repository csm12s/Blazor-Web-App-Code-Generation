// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.LocalizationLocalizer
{
    /// <summary>
    /// 静态本地化器
    /// </summary>
    public static class Lo
    {

        private static Func<Type,ILocalizationLocalizer?>? localizerProvider;

        /// <summary>
        /// 设置服务仓库
        /// </summary>
        /// <param name="localizerProvider"></param>
        public static void Init(Func<Type, ILocalizationLocalizer?> localizerProvider)
        {
            Lo.localizerProvider = localizerProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static ILocalizationLocalizer? GetService<T>()
        {
            if(localizerProvider==null)
            {
                return default;
            }
            ILocalizationLocalizer? service = localizerProvider?.Invoke(typeof(T));
            return service;
        }
        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(params string[] names)
        {
            return Combination(localizerProvider?.Invoke(typeof(ILocalizationLocalizer)), names);
        }

        /// <summary>
        /// 合并多个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination<T>(params string[] names)
        {
            ILocalizationLocalizer? l = GetService<ILocalizationLocalizer<T>>();
            return Combination(l, names);
        }

        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(ILocalizationLocalizer? localizer, params string[] names)
        {
            if (localizer != null)
            {
                return localizer.Combination(names);
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(string name, bool toLower = false)
        {
            return GetValue(localizerProvider?.Invoke(typeof(ILocalizationLocalizer)), name, toLower);
        }



        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue<T>(string name, bool toLower = false)
        {
            ILocalizationLocalizer? l = GetService<ILocalizationLocalizer<T>>();
            return GetValue(l, name, toLower);
        }



        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(ILocalizationLocalizer? localizer, string name, bool toLower = false)
        {
            if (localizer != null && !string.IsNullOrEmpty(name))
            {
                if (toLower)
                {
                    return localizer[name].ToLower();
                }
                return localizer[name];
            }
            return string.Empty;
        }
    }
}
