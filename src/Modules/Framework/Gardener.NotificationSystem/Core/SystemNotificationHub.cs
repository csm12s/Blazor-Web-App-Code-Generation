using Furion.InstantMessaging;
using Gardener.Authentication.Core;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知集线器
    /// </summary>
    [MapHub("/api/ws/system-notification")]
    [Authorize(AuthenticationSchemes = $"{nameof(IdentityType.User)},{nameof(IdentityType.Client)}")]
    public class SystemNotificationHub : Hub
    {
        private readonly IEventBus eventBus;
        private readonly IIdentityService identityService;
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="identityService"></param>
        public SystemNotificationHub(IEventBus eventBus, IIdentityService identityService, ISystemNotificationService systemNotificationService)
        {
            this.eventBus = eventBus;
            this.identityService = identityService;
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 客户端发过来的通知
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task Send(NotificationData data)
        {
            //收到客户端信息
            if (data == null)
            {
                return;
            }
            Identity identity = identityService.GetIdentity();
            data.Identity = identity;

            EventInfo<NotificationData> eventInfo = new EventInfo<NotificationData>(EventType.SystemNotify, data);
            eventInfo.EventGroup = data.Type.ToString();
            await eventBus.Publish(eventInfo);

        }
        /// <summary>
        /// 用户连接成功
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var notification = new UserOnlineChangeNotification()
            {
                OnlineStatus = UserOnlineStatus.Online,
                Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4()
            };
            await systemNotificationService.SendToAllClient(NotificationDataType.UserOnline, notification, identityService.GetIdentity());

            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 用户断开连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var notification = new UserOnlineChangeNotification()
            {
                OnlineStatus = UserOnlineStatus.Offline,
                Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4()
            };
            await systemNotificationService.SendToAllClient(NotificationDataType.UserOnline, notification, identityService.GetIdentity());
            await base.OnDisconnectedAsync(exception);
        }
    }


}
