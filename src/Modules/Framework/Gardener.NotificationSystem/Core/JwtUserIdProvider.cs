// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.GetHttpContext().User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
