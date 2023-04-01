// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// 启用自动刷新
        /// </summary>
        /// <remarks>
        /// 目前必须开启，后续可以在api调用时如果401时刷新token，就可以不开启
        /// </remarks>
        public bool EnableAutoRefresh { get; set; }=true;

        /// <summary>
        /// token刷新间隔（单位：秒）
        /// token 过期时间是API 配置 JWTSettings.ExpiredTime
        /// </summary>
        public int RefreshTokenCheckInterval { get; set; } = 240;

        /// <summary>
        /// token刷新过期时间阈值（单位：秒）
        /// </summary>
        public int RefreshTokenTimeThreshold { get; set; } = 70;
        /// <summary>
        /// 登陆页面地址
        /// </summary>
        public string LoginPagePath { get; set; } = "/auth/login";
    }
}
