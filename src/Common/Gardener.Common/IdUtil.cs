// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using IdGen;

namespace Gardener.Common;

/// <summary>
/// Id生成工具
/// </summary>
public class IdUtil
{
    /// <summary>
    /// 雪花ID
    /// </summary>
    /// <param name="generatorId"></param>
    /// <returns></returns>
    public static long GetNextId(int generatorId = 0)
    {
        var generator = new IdGenerator(0);
        var id = generator.CreateId();
        return id;
    }

}
