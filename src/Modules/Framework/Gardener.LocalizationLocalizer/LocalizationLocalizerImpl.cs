// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Microsoft.Extensions.Localization;
using System.Globalization;
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
        /// <summary>
        /// 分割符
        /// </summary>
        private static readonly Dictionary<string, string> splitCharacterMap = new Dictionary<string, string>
        {
            {"af",string.Empty},
            {"af-ZA",string.Empty},
            {"sq",string.Empty},
            {"sq-AL",string.Empty},
            {"ar",string.Empty},
            {"ar-DZ",string.Empty},
            {"ar-BH",string.Empty},
            {"ar-EG",string.Empty},
            {"ar-IQ",string.Empty},
            {"ar-JO",string.Empty},
            {"ar-KW",string.Empty},
            {"ar-LB",string.Empty},
            {"ar-LY",string.Empty},
            {"ar-MA",string.Empty},
            {"ar-OM",string.Empty},
            {"ar-QA",string.Empty},
            {"ar-SA",string.Empty},
            {"ar-SY",string.Empty},
            {"ar-TN",string.Empty},
            {"ar-AE",string.Empty},
            {"ar-YE",string.Empty},
            {"hy",string.Empty},
            {"hy-AM",string.Empty},
            {"az",string.Empty},
            {"az-AZ-Cyrl",string.Empty},
            {"az-AZ-Latn",string.Empty},
            {"eu",string.Empty},
            {"eu-ES",string.Empty},
            {"be",string.Empty},
            {"be-BY",string.Empty},
            {"bg",string.Empty},
            {"bg-BG",string.Empty},
            {"ca",string.Empty},
            {"ca-ES",string.Empty},
            {"zh-HK",string.Empty},
            {"zh-MO",string.Empty},
            {"zh-CN",string.Empty},
            {"zh-CHS",string.Empty},
            {"zh-SG",string.Empty},
            {"zh-TW",string.Empty},
            {"zh-CHT",string.Empty},
            {"hr",string.Empty},
            {"hr-HR",string.Empty},
            {"cs",string.Empty},
            {"cs-CZ",string.Empty},
            {"da",string.Empty},
            {"da-DK",string.Empty},
            {"div",string.Empty},
            {"div-MV",string.Empty},
            {"nl",string.Empty},
            {"nl-BE",string.Empty},
            {"nl-NL",string.Empty},
            {"en"," "},
            {"en-AU"," "},
            {"en-BZ"," "},
            {"en-CA"," "},
            {"en-CB"," "},
            {"en-IE"," "},
            {"en-JM"," "},
            {"en-NZ"," "},
            {"en-PH"," "},
            {"en-ZA"," "},
            {"en-TT"," "},
            {"en-GB"," "},
            {"en-US"," "},
            {"en-ZW"," "},
            {"et",string.Empty},
            {"et-EE",string.Empty},
            {"fo",string.Empty},
            {"fo-FO",string.Empty},
            {"fa",string.Empty},
            {"fa-IR",string.Empty},
            {"fi",string.Empty},
            {"fi-FI",string.Empty},
            {"fr",string.Empty},
            {"fr-BE",string.Empty},
            {"fr-CA",string.Empty},
            {"fr-FR",string.Empty},
            {"fr-LU",string.Empty},
            {"fr-MC",string.Empty},
            {"fr-CH",string.Empty},
            {"gl",string.Empty},
            {"gl-ES",string.Empty},
            {"ka",string.Empty},
            {"ka-GE",string.Empty},
            {"de",string.Empty},
            {"de-AT",string.Empty},
            {"de-DE",string.Empty},
            {"de-LI",string.Empty},
            {"de-LU",string.Empty},
            {"de-CH",string.Empty},
            {"el",string.Empty},
            {"el-GR",string.Empty},
            {"gu",string.Empty},
            {"gu-IN",string.Empty},
            {"he",string.Empty},
            {"he-IL",string.Empty},
            {"hi",string.Empty},
            {"hi-IN",string.Empty},
            {"hu",string.Empty},
            {"hu-HU",string.Empty},
            {"is",string.Empty},
            {"is-IS",string.Empty},
            {"id",string.Empty},
            {"id-ID",string.Empty},
            {"it",string.Empty},
            {"it-IT",string.Empty},
            {"it-CH",string.Empty},
            {"ja",string.Empty},
            {"ja-JP",string.Empty},
            {"kn",string.Empty},
            {"kn-IN",string.Empty},
            {"kk",string.Empty},
            {"kk-KZ",string.Empty},
            {"kok",string.Empty},
            {"kok-IN",string.Empty},
            {"ko",string.Empty},
            {"ko-KR",string.Empty},
            {"ky",string.Empty},
            {"ky-KZ",string.Empty},
            {"lv",string.Empty},
            {"lv-LV",string.Empty},
            {"lt",string.Empty},
            {"lt-LT",string.Empty},
            {"mk",string.Empty},
            {"mk-MK",string.Empty},
            {"ms",string.Empty},
            {"ms-BN",string.Empty},
            {"ms-MY",string.Empty},
            {"mr",string.Empty},
            {"mr-IN",string.Empty},
            {"mn",string.Empty},
            {"mn-MN",string.Empty},
            {"no",string.Empty},
            {"nb-NO",string.Empty},
            {"nn-NO",string.Empty},
            {"pl",string.Empty},
            {"pl-PL",string.Empty},
            {"pt",string.Empty},
            {"pt-BR",string.Empty},
            {"pt-PT",string.Empty},
            {"pa",string.Empty},
            {"pa-IN",string.Empty},
            {"ro",string.Empty},
            {"ro-RO",string.Empty},
            {"ru",string.Empty},
            {"ru-RU",string.Empty},
            {"sa",string.Empty},
            {"sa-IN",string.Empty},
            {"sr-SP-Cyrl",string.Empty},
            {"sr-SP-Latn",string.Empty},
            {"sk",string.Empty},
            {"sk-SK",string.Empty},
            {"sl",string.Empty},
            {"sl-SI",string.Empty},
            {"es",string.Empty},
            {"es-AR",string.Empty},
            {"es-BO",string.Empty},
            {"es-CL",string.Empty},
            {"es-CO",string.Empty},
            {"es-CR",string.Empty},
            {"es-DO",string.Empty},
            {"es-EC",string.Empty},
            {"es-SV",string.Empty},
            {"es-GT",string.Empty},
            {"es-HN",string.Empty},
            {"es-MX",string.Empty},
            {"es-NI",string.Empty},
            {"es-PA",string.Empty},
            {"es-PY",string.Empty},
            {"es-PE",string.Empty},
            {"es-PR",string.Empty},
            {"es-ES",string.Empty},
            {"es-UY",string.Empty},
            {"es-VE",string.Empty},
            {"sw",string.Empty},
            {"sw-KE",string.Empty},
            {"sv",string.Empty},
            {"sv-FI",string.Empty},
            {"sv-SE",string.Empty},
            {"syr",string.Empty},
            {"syr-SY",string.Empty},
            {"ta",string.Empty},
            {"ta-IN",string.Empty},
            {"tt",string.Empty},
            {"tt-RU",string.Empty},
            {"te",string.Empty},
            {"te-IN",string.Empty},
            {"th",string.Empty},
            {"th-TH",string.Empty},
            {"tr",string.Empty},
            {"tr-TR",string.Empty},
            {"uk",string.Empty},
            {"uk-UA",string.Empty},
            {"ur",string.Empty},
            {"ur-PK",string.Empty},
            {"uz",string.Empty},
            {"uz-UZ-Cyrl",string.Empty},
            {"uz-UZ-Latn",string.Empty},
            {"vi",string.Empty},
            {"vi-VN",string.Empty}
        };

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
                msg.Append(splitCharacterMap.GetValueOrDefault(CultureInfo.CurrentCulture.Name,string.Empty));
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
    /// <remarks>
    /// 先从指定的资源查找，找不到再从公共资源查找
    /// </remarks>
    public class LocalizationLocalizerCompensationImpl<T> : LocalizationLocalizerImpl<T>, ILocalizationLocalizer<T>
    {
        private readonly ILocalizationLocalizer commonLocalizer;
        public LocalizationLocalizerCompensationImpl(IStringLocalizer<T> localizer, ILocalizationLocalizer commonLocalizer) : base(localizer)
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
