// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.LocalizationLocalizer;
using Microsoft.Extensions.Localization;
using System.Text;

namespace Gardener.Client.Core.Services
{
    /// <summary>
    /// 本地化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LocalizationLocalizerImpl<T> : ILocalizationLocalizer<T>
    {
        private readonly IStringLocalizer<T> localizer;
        /// <summary>
        /// 公共
        /// </summary>
        private readonly ILocalizationLocalizer sharedLocalizer;

        public LocalizationLocalizerImpl(IStringLocalizer<T> localizer, ILocalizationLocalizer sharedLocalizer)
        {
            this.localizer = localizer;
            this.sharedLocalizer = sharedLocalizer;
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
        public LocalizedString Get(string name)
        {

            LocalizedString localizedString = localizer[name];
            if (localizedString.ResourceNotFound && !GetLocalizerName().Equals(sharedLocalizer.GetLocalizerName())) {
                return sharedLocalizer.Get(name);
            }
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

        public string GetLocalizerName()
        {
            return $"{nameof(LocalizationLocalizerImpl<T>)}:{typeof(T).FullName}";
        }
    }
}
