// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Core
{
    public static class AuthConstant
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly string DefaultAuthenticatedPolicy = "default";
        /// <summary>
        /// 登录路由地址
        /// </summary>
        public static readonly string LoginUrl = "/auth/login";
        /// <summary>
        /// 刷新token不存在或已过期异常标识
        /// </summary>
        public static readonly string RefreshtokenNoExistOrExpireErrorFlag = "REFRESHTOKEN_NO_EXIST_OR_EXPIRE";
        /// <summary>
        /// 
        /// </summary>
        public static readonly string ClientUIResourcePolicy = "client-ui-resource-policy";
        /// <summary>
        /// 
        /// </summary>
        public static readonly string ClientPageResourcePolicy = "client-page-resource-policy";
    }
}
