// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Microsoft.Extensions.Localization;
using System.Text;

namespace Gardener.LocalizationLocalizer
{
    /// <summary>
    /// 本地化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LocalizationLocalizerImpl<T> : ILocalizationLocalizer<T>
    {
        private readonly IStringLocalizer<T> localizer;

        public LocalizationLocalizerImpl(IStringLocalizer<T> localizer)
        {
            this.localizer = localizer;
        }

        public string this[string name] => GetValue(name);
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// 先从指定的资源查找，找不到再从公共资源查找
        /// </remarks>
        public string GetValue(string name)
        {
            return Get(name).Value;
        }

        /// <summary>
        /// 获取LocalizedString
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// 先从指定的资源查找，找不到再从公共资源查找
        /// </remarks>
        public virtual LocalizedString Get(string name)
        {
            LocalizedString localizedString = localizer[name];
            return localizedString;
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public string Combination(params string[] names)
        {
            if (names.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder msg = new StringBuilder();
            for (int i = 0; i < names.Length; i++)
            {
                msg.Append(GetValue(names[i]));
                msg.Append(' ');
            }
            return msg.ToString().TrimEnd(' ');
        }

        public string GetResourceFullName()
        {
            return typeof(T).FullName ?? typeof(T).Name;
        }
    }
    /// <summary>
    /// 本地化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LocalizationLocalizerMultipleImpl<T> : LocalizationLocalizerImpl<T>, ILocalizationLocalizer<T>
    {
        private readonly ILocalizationLocalizer commonLocalizer;
        public LocalizationLocalizerMultipleImpl(IStringLocalizer<T> localizer, ILocalizationLocalizer commonLocalizer) : base(localizer)
        {
            this.commonLocalizer = commonLocalizer;
        }

        /// <summary>
        /// 获取LocalizedString
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// 先从指定的资源查找，找不到再从公共资源查找
        /// </remarks>
        public override LocalizedString Get(string name)
        {
            LocalizedString localizedString = base.Get(name);
            //未找到时，并且和公共资源不是同一资源类，就从公共资源找
            if (localizedString.ResourceNotFound && !commonLocalizer.GetResourceFullName().Equals(base.GetResourceFullName()))
            {
                return commonLocalizer.Get(name);
            }
            return localizedString;
        }
    }
}
