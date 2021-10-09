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
        public static IClientLocalizer Localizer;

        public static string Combination(params string[] names)
        {
            if (Localizer != null)
            {
               return Localizer.Combination(names);
            }
            return string.Empty;
        }

        public static string GetValue(string name,bool toLower=false)
        {
            if (Localizer != null)
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
