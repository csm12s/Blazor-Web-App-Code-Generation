using Furion;
using Furion.InstantMessaging;
using Gardener.Authentication.Core;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;

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
        /// <param name="systemNotificationService"></param>
        public SystemNotificationHub(IEventBus eventBus, 
            IIdentityService identityService, 
            ISystemNotificationService systemNotificationService)
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
            data.Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4();

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
            var notification = new UserOnlineChangeNotificationData()
            {
                OnlineStatus = UserOnlineStatus.Online
            };
            await systemNotificationService.SendToAllClient(NotificationDataType.UserOnline, notification, identityService.GetIdentity(), Context.GetHttpContext().GetRemoteIpAddressToIPv4());

            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 用户断开连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var notification = new UserOnlineChangeNotificationData()
            {
                OnlineStatus = UserOnlineStatus.Offline
            };
            await systemNotificationService.SendToAllClient(NotificationDataType.UserOnline, notification, identityService.GetIdentity(), Context.GetHttpContext().GetRemoteIpAddressToIPv4());
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options"></param>
        public static void HttpConnectionDispatcherOptionsSettings(HttpConnectionDispatcherOptions options)
        {
            // 配置
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public static void HubEndpointConventionBuilderSettings(HubEndpointConventionBuilder builder)
        {
            string origins = App.Configuration["SignalR:SystemNotificationHub:Origins"];
            // 配置
            builder.RequireCors(cpb =>
            {
                cpb.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(origins)
                    .AllowCredentials()
                    .Build()
                    ;
            });

        }
    }


}
