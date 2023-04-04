#nullable enable

using Furion;
using Furion.InstantMessaging;
using Gardener.Authentication.Core;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;
using Gardener.NotificationSystem.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

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
            //没有身份信息
            Identity? identity = identityService.GetIdentity();
            if(identity == null)
            {
                return;
            }
            data.Identity = identity;
            data.Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4();
            await eventBus.Publish(data);

        }
        /// <summary>
        /// 用户连接成功
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            //没有身份信息
            Identity? identity = identityService.GetIdentity();
            if (identity == null)
            {
                return;
            }
            var notification = new UserOnlineChangeNotificationData()
            {
                Identity= identity,
                Ip= Context.GetHttpContext().GetRemoteIpAddressToIPv4(),
                OnlineStatus = UserOnlineStatus.Online
            };
            await systemNotificationService.SendToAllClient(notification);
            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 用户断开连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Identity? identity = identityService.GetIdentity();
            if (identity == null)
            {
                return;
            }
            var notification = new UserOnlineChangeNotificationData()
            {
                Identity = identity,
                Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4(),
                OnlineStatus = UserOnlineStatus.Offline
            };
            await systemNotificationService.SendToAllClient(notification);
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
            // 配置
            var options = App.GetService<IOptions<SignalROptions>>().Value;
            if (options == null)
                throw new ArgumentNullException("没有signalr的配置");

            if (options.SystemNotificationHub == null)
                throw new ArgumentNullException("没有对跨域进行配置");

            var origins = options.SystemNotificationHub.Origins;

            if (origins == null || origins.Count() == 0)
                throw new ArgumentNullException("请至少配置一个域");

            builder.RequireCors(cpb =>
            {
                cpb.WithOrigins(origins)
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
                   .Build();
            });

        }
    }
}
