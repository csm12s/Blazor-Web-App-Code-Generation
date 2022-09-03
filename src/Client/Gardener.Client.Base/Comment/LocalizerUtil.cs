// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// 静态本地化器
    /// </summary>
    public class LocalizerUtil
    {
        /// <summary>
        /// 本地化器
        /// </summary>
        public static IClientLocalizer Localizer;

        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(params string[] names)
        {
            if (Localizer != null)
            {
               return Localizer.Combination(names);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(string name,bool toLower=false)
        {
            if (Localizer != null && !string.IsNullOrEmpty(name))
            {
                if (toLower)
                {
                    return Localizer[name].ToLower();
                }
                return Localizer[name];
            }
            return string.Empty;
        }
    }
}
