// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;

namespace Gardener.Authentication.Constants
{
    /// <summary>
    /// 身份验证方案
    /// </summary>
    public class GardenerAuthenticationSchemes
    {
        /// <summary>
        /// 用戶
        /// </summary>
        public readonly static string User= IdentityType.User.ToString();
        /// <summary>
        /// client
        /// </summary>
        public readonly static string Client= IdentityType.Client.ToString();
    }
}
