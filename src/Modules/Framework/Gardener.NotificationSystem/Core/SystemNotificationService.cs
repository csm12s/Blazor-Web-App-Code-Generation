// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Core;
using Gardener.Authentication.Dtos;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;
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
        /// <typeparam name="TData"></typeparam>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        private async Task SendToAllClient<TData>(NotificationData notifyData) where TData : NotificationDataBase
        {
            await hubContext.Clients.All.SendAsync("ReceiveMessage", notifyData);
        }

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task SendToAllClient<TData>(NotificationDataType dataType, TData data, Identity Identity=null,string ip=null) where TData : NotificationDataBase
        {
            NotificationData notifyData = new NotificationData();
            notifyData.Data = System.Text.Json.JsonSerializer.Serialize(data);
            notifyData.Type = dataType;
            notifyData.Identity = Identity;
            notifyData.Ip = ip;
            await SendToAllClient<NotificationData>(notifyData);
        }

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public async Task SendToAllClient(NotificationData notifyData)
        {
            await SendToAllClient<NotificationData>(notifyData);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendToUser<TData>(int userId, NotificationDataType dataType, TData data, Identity Identity, string ip = null) where TData : NotificationDataBase
        {
            NotificationData notifyData = new NotificationData();
            notifyData.Data = System.Text.Json.JsonSerializer.Serialize(data);
            notifyData.Type = dataType;
            notifyData.Identity = Identity;
            await SendToUser<NotificationData>(userId,notifyData);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public async Task SendToUser(int userId, NotificationData notifyData)
        {
            await SendToUser<NotificationData>(userId,notifyData);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        private async Task SendToUser<TData>(int userId, NotificationData notifyData) where TData : NotificationDataBase
        {
            await hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveMessage", notifyData);
        }
    }
}
