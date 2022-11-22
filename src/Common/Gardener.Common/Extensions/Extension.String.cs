using Humanizer;
using System.Linq;

namespace Gardener.Common;

//TODO: StringExtension搬到这里

public static partial class Extension
{
    #region Camel - Humanizer
    // https://github.com/Humanizr/Humanizer
    //"some_title for" => "someTitleFor", 小驼峰
    public static string ToLowerCamel(this string input)
    {
        return input.Camelize();
    }
    //"some_title for" => "SomeTitleFor", 大驼峰
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


    // End
}
