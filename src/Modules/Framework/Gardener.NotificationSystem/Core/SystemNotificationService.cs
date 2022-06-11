// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public class SystemNotificationService : ISystemNotificationService
    {
        private readonly IHubContext<SystemNotificationHub> hubContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        public SystemNotificationService(IHubContext<SystemNotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }


        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public async Task SendToAllClient(NotificationData notifyData)
        {
            string json= System.Text.Json.JsonSerializer.Serialize(notifyData,notifyData.GetType());
            await hubContext.Clients.All.SendAsync("ReceiveMessage", json);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public async Task SendToUser(int userId, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData);
            await hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveMessage", json);
        }
    }
}
