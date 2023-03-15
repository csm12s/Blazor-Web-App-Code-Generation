// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// token刷新返回结果
    /// </summary>
    public class TokenOutput
    {
        /// <summary>
        /// 获取或设置 用于业务身份认证的AccessToken
        /// </summary>
        public string AccessToken { get; set; } = null!;
        /// <summary>
        /// 获取或设置 AccessToken有效期(时间戳精确到秒)
        /// </summary>
        public long AccessTokenExpires { get; set; }
        /// <summary>
        /// 获取或设置 用于刷新AccessToken的RefreshToken
        /// </summary>
        public string RefreshToken { get; set; } = null!;
        /// <summary>
        /// 获取或设置 RefreshToken有效期(时间戳精确到秒)
        /// </summary>
        public long RefreshTokenExpires { get; set; }
    }
}
