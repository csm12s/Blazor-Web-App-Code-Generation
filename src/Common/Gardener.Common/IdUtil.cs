// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using IdGen;
using System;

namespace Gardener.Common;

/// <summary>
/// Id生成工具
/// </summary>
public class IdUtil
{
    /// <summary>
    /// 19位雪花ID
    /// </summary>
    /// <param name="generatorId"></param>
    /// <returns></returns>
    public static long GetNextId(int generatorId = 0)
    {
        var generator = new IdGenerator(0);
        var id = generator.CreateId();
        return id;
    }

    public static string GetGuid32(string formatStr = "N")
    {
        return Guid.NewGuid().ToString(formatStr);

        // Format string:

        // N	32 digits:
        // 00000000000000000000000000000000

        // D	32 digits separated by hyphens:
        // 00000000-0000-0000-0000-000000000000

        // B	32 digits separated by hyphens, enclosed in braces:
        // {00000000-0000-0000-0000-000000000000}

        // P	32 digits separated by hyphens, enclosed in parentheses:
        // (00000000-0000-0000-0000-000000000000)

        // X	Four hexadecimal values enclosed in braces,
        // where the fourth value is a subset of eight hexadecimal values that is
        // also enclosed in braces:
        // {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
    }

}
