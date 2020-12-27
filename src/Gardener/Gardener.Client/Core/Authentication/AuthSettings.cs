// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// token刷新间隔（单位：秒）
        /// </summary>
        public int RefreshTokenCheckInterval { get; set; } = 30;

        /// <summary>
        /// token刷新过期时间阈值（单位：秒）
        /// </summary>
        public int RefreshTokenTimeThreshold { get; set; } = 70;
    }
}
