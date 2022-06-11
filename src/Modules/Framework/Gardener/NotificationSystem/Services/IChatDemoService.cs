// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Dtos.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.NotificationSystem.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    public interface IChatDemoService
    {
        /// <summary>
        /// 获取历史聊天记录
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChatDemoNotificationData >> GetHistory();
    }
}
