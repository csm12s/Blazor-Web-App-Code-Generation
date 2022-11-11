using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.Common;

//TODO: StringExtension搬到这里

public static partial class Extension
{
    #region Camel - Humanizer
    // https://github.com/Humanizr/Humanizer
    //"some_title for" => "someTitleFor", 小驼峰
    public static string ToCamel(this string input)
    {
        return input.Camelize();
    }
    //"some_title for" => "SomeTitleFor", 大驼峰
    public static string ToUpCamel(this string input)
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

    public static string ReplaceSlash(this string input,
        string newChar = "-")
    {
        input = input.Replace("\\", newChar);
        input = input.Replace(@"/", newChar);

        return input;
    }

    public static bool EqualIgnoreCase(this string input, string str)
    {
        return input.ToLower() == str.ToLower();
    }

    public static int ToInt(this string input)
    {
        try
        {
            return Convert.ToInt32(input);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public static long ToLong(this string input)
    {
        try
        {
            return long.Parse(input);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public static double ToDouble(this string input)
    {
        return Convert.ToDouble(input);
    }

    public static bool ToBool(this string input)
    {
        try
        {
            return Convert.ToBoolean(input);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static List<string> ToStringList(this string input)
    {
        List<string> list = new List<string>();
        list.Add(input);

        return list;
    }

    // End
}
