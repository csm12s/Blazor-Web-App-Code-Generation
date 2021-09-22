// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Authentication.Enums
{
    /// <summary>
    /// 身份类型
    /// </summary>
    public enum IdentityType
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        User,
        /// <summary>
        /// 客户端
        /// </summary>
        [Description("客户端")]
        Client
    }
}
