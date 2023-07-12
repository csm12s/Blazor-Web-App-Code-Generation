// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Microsoft.Extensions.Localization;

namespace Gardener.Base
{
    /// <summary>
    /// 本地化
    /// </summary>
    public static class Lo
    {
        /// <summary>
        /// 根据实体类属性名获取对应的多语言配置
        /// </summary>
        /// <typeparam name="TResource">通常命名为 SharedResource </typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetString<TResource>(string name)
        {
            IStringLocalizer<TResource> localizer = App.GetService<IStringLocalizer<TResource>>(App.RootServices);
            string value = localizer[name];
            if (value.Equals(name))
            {
                value = App.GetService<IStringLocalizer>(App.RootServices)[name];
            }
            return value;
        }

        /// <summary>
        /// 根据实体类属性名获取对应的多语言配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetString(string name)
        {
            return App.GetService<IStringLocalizer>(App.RootServices)[name];
        }
    }
}
