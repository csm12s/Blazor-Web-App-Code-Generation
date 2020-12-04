// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// token刷新返回结果
    /// </summary>
    public class TokenOutput
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 到期时间 UnixTimeSeconds
        /// </summary>
        public long AccessTokenExpiresIn { get; set; }
    }
}
