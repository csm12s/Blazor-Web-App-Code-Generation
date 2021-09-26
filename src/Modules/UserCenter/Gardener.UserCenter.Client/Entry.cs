// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.UserCenter.Client
{
    /// <summary>
    /// 入口
    /// </summary>
    public static class Entry
    {
        static Entry()
        {
            Gardener.Client.Base.Entry.Add(typeof(Entry).Assembly);
        }
    }
}
