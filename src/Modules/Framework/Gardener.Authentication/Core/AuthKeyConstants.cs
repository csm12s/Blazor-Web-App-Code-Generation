// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthKeyConstants
    {
        /// <summary>
        /// 登录编号
        /// </summary>
        public static readonly string ClientIdKeyName = "loginId";
        /// <summary>
        /// 客户端类型
        /// </summary>
        public static readonly string ClientTypeKeyName = "clientType";
        /// <summary>
        /// token 类型
        /// </summary>
        public static readonly string TokenTypeKey = "tokenTypeKey";
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public static readonly string UserIsSuperAdministratorKey = "IsSuperAdministrator";
        /// <summary>
        /// 身份类型
        /// </summary>
        public static readonly string IdentityType = "identityType";
        /// <summary>
        /// 租户编号
        /// </summary>
        public static readonly string TenantId = "tenantId";
        /// <summary>
        /// 自定义数据
        /// </summary>
        public static readonly string CustomData = "customData";
    }
}
