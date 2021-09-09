// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace Gardener.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtRefreshTokenSettings : IConfigurableOptions
    {
        /// <summary>
        /// 获取或设置 RefreshToken有效期分钟数
        /// </summary>
        public double RefreshExpireMins { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken是否绝对过期
        /// </summary>
        public bool IsRefreshAbsoluteExpired { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public string IssuerSigningKey { get; set; }
    }
}
