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
    public enum HttpMethod
    {
        /// <summary>
        /// GET
        /// </summary>
        [Description("GET")]
        GET=0,
        /// <summary>
        /// POST
        /// </summary>
        [Description("POST")]
        POST=1,
        /// <summary>
        /// PUT
        /// </summary>
        [Description("PUT")]
        PUT=2,
        /// <summary>
        /// DELETE
        /// </summary>
        [Description("DELETE")]
        DELETE=3,
        /// <summary>
        /// PATCH
        /// </summary>
        [Description("PATCH")]
        PATCH=4
    }
}
