// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 静态本地化器
    /// </summary>
    /// <remarks>
    /// <see cref="Gardener.Client.Core.CultureExtension"/>
    /// </remarks>
    public class LocalizerUtil
    {
        /// <summary>
        /// 服务仓库
        /// </summary>
        private static IServiceProvider Services;

        /// <summary>
        /// 设置服务仓库
        /// </summary>
        /// <param name="services"></param>
        public static void SetServices(IServiceProvider services) 
        {
            Services = services;
        }

        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(params string[] names)
        {
            IClientLocalizer l = Services.GetRequiredService<IClientLocalizer>();
            return LocalizerUtil.Combination(l, names);
        }

        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(string name, bool toLower = false)
        {
            IClientLocalizer l = Services.GetRequiredService<IClientLocalizer>();
            return LocalizerUtil.GetValue(l, name, toLower);
        }


        /// <summary>
        /// 合并多个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination<T>(params string[] names)
        {
            IClientLocalizer<T> l = Services.GetRequiredService<IClientLocalizer<T>>();
            return LocalizerUtil.Combination(l, names);
        }

        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue<T>(string name, bool toLower = false)
        {
            IClientLocalizer<T> l = Services.GetRequiredService<IClientLocalizer<T>>();
            return LocalizerUtil.GetValue(l, name, toLower);
        }

        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(IClientLocalizer localizer, params string[] names)
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
        public static string GetValue(IClientLocalizer localizer, string name, bool toLower = false)
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
