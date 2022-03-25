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
        private readonly IIdentityService identityService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="identityService"></param>
        public SystemNotificationService(IHubContext<SystemNotificationHub> hubContext, IIdentityService identityService)
        {
            this.hubContext = hubContext;
            this.identityService = identityService;
        }

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        private async Task SendToAllClient<TData>(NotificationData notifyData) where TData : NotificationDataBase
        {
            if (notifyData.Identity == null)
            {
                notifyData.Identity = identityService.GetIdentity();

            }
            await hubContext.Clients.All.SendAsync("ReceiveMessage", notifyData);
        }
        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SendToAllClient<TData>(NotificationDataType dataType, TData data) where TData : NotificationDataBase
        {
            await SendToAllClient(dataType, data,null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <returns></returns>
        public async Task SendToAllClient<TData>(NotificationDataType dataType, TData data, Identity Identity) where TData : NotificationDataBase
        {
            NotificationData notifyData = new NotificationData();
            notifyData.Data = System.Text.Json.JsonSerializer.Serialize(data);
            notifyData.Type = dataType;
            notifyData.Identity = Identity;
            await SendToAllClient<NotificationData>(notifyData);
        }
    }
}
