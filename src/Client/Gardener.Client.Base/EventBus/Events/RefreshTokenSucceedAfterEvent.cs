// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.EventBus;

namespace Gardener.Client.Base.EventBus.Events
{
    /// <summary>
    /// 刷新token成功后
    /// </summary>
    public class RefreshTokenSucceedAfterEvent : EventBase
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        public RefreshTokenSucceedAfterEvent(TokenOutput token) : base(nameof(RefreshTokenSucceedAfterEvent))
        {
            Token = token;
        }
    }
}
