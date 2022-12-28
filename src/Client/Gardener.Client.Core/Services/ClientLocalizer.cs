// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Client.Base;
using Microsoft.Extensions.Localization;

namespace Gardener.Client.Core.Services
{
    /// <summary>
    /// 本地化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClientLocalizer<T> : IClientLocalizer<T>
    {
        private readonly IStringLocalizer<T> localizer;
        /// <summary>
        /// 公共
        /// </summary>
        private readonly IClientLocalizer sharedLocalizer;

        public ClientLocalizer(IStringLocalizer<T> localizer, IClientLocalizer sharedLocalizer)
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

            LocalizedString localizedString = localizer[name];
            if (localizedString.ResourceNotFound && !GetLocalizerName().Equals(sharedLocalizer.GetLocalizerName())) {
                return sharedLocalizer[name];
            }
            return localizedString.Value;
        }

        public string Combination(params string[] names)
        {
            if (names.Length == 0)
            {
                return string.Empty;
            }
            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            for (int i = 0; i < names.Length; i++)
            {
                msg.Append(GetValue(names[i]));
                msg.Append(' ');
            }
            return msg.ToString().TrimEnd(' ');
        }

        public string GetLocalizerName()
        {
            return $"{nameof(ClientLocalizer<T>)}:{typeof(T).FullName}";
        }
    }
    /// <summary>
    /// 公共共享本地化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClientSharedLocalizer<T> : IClientLocalizer
    {
        private readonly IStringLocalizer<T> localizer;

        public ClientSharedLocalizer(IStringLocalizer<T> localizer)
        {
            this.localizer = localizer;
        }

        public string this[string name] => GetValue(name);

        public string Combination(params string[] names)
        {
            if (names.Length == 0)
            {
                return string.Empty;
            }
            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            for (int i = 0; i < names.Length; i++)
            {
                msg.Append(localizer[names[i]].Value);
                msg.Append(' ');
            }
            return msg.ToString().TrimEnd(' ');
        }

        public string GetLocalizerName()
        {
            return $"{nameof(ClientLocalizer<T>)}:{typeof(T).FullName}";
        }

        public string GetValue(string name)
        {
            return localizer[name].Value;
        }
    }
}
