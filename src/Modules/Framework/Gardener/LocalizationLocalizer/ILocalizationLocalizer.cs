// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Localization;

namespace Gardener.LocalizationLocalizer
{
    /// <summary>
    /// 本地化器
    /// </summary>
    public interface ILocalizationLocalizer
    {
        /// <summary>
        /// 本地化器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string this[string name]
        {
            get;
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public string Combination(params string[] names);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(string name);

        /// <summary>
        /// 获取LocalizedString
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// 先从指定的资源查找，找不到再从公共资源查找
        /// </remarks>
        public LocalizedString Get(string name);

        /// <summary>
        /// 获取资源完整名称
        /// </summary>
        /// <returns></returns>
        public string GetResourceFullName();
    }
    /// <summary>
    /// 本地化器
    /// </summary>
    /// <typeparam name="T">指定资源文件类</typeparam>
    public interface ILocalizationLocalizer<T> : ILocalizationLocalizer
    {
    }
}
