// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum HttpMethodType
    {
        [Description("GET")]
        GET=0,
        [Description("POST")]
        POST=1,
        [Description("PUT")]
        PUT=2,
        [Description("DELETE")]
        DELETE=3,
        [Description("PATCH")]
        PATCH=4
    }
}
