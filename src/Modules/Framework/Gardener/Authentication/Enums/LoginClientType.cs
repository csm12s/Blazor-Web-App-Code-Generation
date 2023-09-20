// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Authentication.Enums
{
    /// <summary>
    /// 请求登录的客户端类型
    /// </summary>
    public enum LoginClientType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("Unknown")]
        Unknown,
        /// <summary>
        /// 浏览器类型
        /// </summary>
        [Description("Browser")]
        Browser,

        /// <summary>
        /// 桌面客户端
        /// </summary>
        [Description("Desktop")]
        Desktop,

        /// <summary>
        /// 手机客户端
        /// </summary>
        [Description("Mobile")]
        Mobile,

        /// <summary>
        /// 服务端
        /// </summary>
        [Description("Server")]
        Server
    }
}
