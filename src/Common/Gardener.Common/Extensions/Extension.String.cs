using Humanizer;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gardener.Common;

public static partial class Extension
{
    #region Camel - Humanizer
    // https://github.com/Humanizr/Humanizer

    /// <summary>
    /// "some_title for" => "someTitleFor", 小驼峰
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToLowerCamel(this string input)
    {
        return input.Camelize();
    }

    /// <summary>
    /// "some_title for" => "SomeTitleFor", 大驼峰
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToUpperCamel(this string input)
    {
        return input.Pascalize();
    }
    #endregion

    public static string FirstToUpper(this string input)
    {
        return input.First().ToString().ToUpper() + input.Substring(1);
    }
    public static string FirstToLower(this string input)
    {
        return input.First().ToString().ToLower() + input.Substring(1);
    }

    /// <summary>
    /// 判断字符串是否是数字
    /// </summary>
    public static bool IsNumber(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return false;
        const string pattern = "^[0-9]*$";
        Regex rx = new Regex(pattern);
        return rx.IsMatch(str);
    }

    // End
}
