// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Constants
{
    public static class AuthConstant
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultAuthenticatedPolicy = "default";
        /// <summary>
        /// 
        /// </summary>
        public static readonly string ClientUIResourcePolicy = "client-ui-resource-policy";
        /// <summary>
        /// 
        /// </summary>
        public static readonly string ClientPageResourcePolicy = "client-page-resource-policy";

        /// <summary>
        /// token刷新间隔（单位：秒）
        /// </summary>
        public readonly static int RefreshTokenCheckInterval = 30;

        /// <summary>
        /// token刷新过期时间阈值（单位：秒）
        /// </summary>
        public readonly static int RefreshTokenTimeThreshold = 70;
    }
}
