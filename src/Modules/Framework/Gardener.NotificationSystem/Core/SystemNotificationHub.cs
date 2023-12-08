// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

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
        public Task Send(NotificationData data)
        {
            //收到客户端信息
            if (data == null)
            {
                return Task.CompletedTask;
            }
            //没有身份信息
            Identity? identity = identityService.GetIdentity();
            if (identity == null)
            {
                return Task.CompletedTask;
            }
            data.Identity = identity;
            data.Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4();
            return eventBus.PublishAsync(data);

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
            await systemNotificationService.SetUserOnline(identity, this.Context.ConnectionId);

            //分组器
            IEnumerable<ISystemNotificationHubGrouper> groupers = App.GetServices<ISystemNotificationHubGrouper>();
            List<IEnumerable<string>> groupNameGroups = new();
            foreach (var grouper in groupers)
            {
                groupNameGroups.Add(await grouper.GetGroupName(identity));
            }
            //分组完成
            List<Task> tasks1 = new List<Task>();
            foreach (IEnumerable<string> groups in groupNameGroups)
            {
                if (!groups.Any()) continue;
                foreach (var group in groups)
                {
                    tasks1.Add(base.Groups.AddToGroupAsync(this.Context.ConnectionId, group));
                }
            }
            if (tasks1.Any())
            {
                //入组完成
                await Task.WhenAll(tasks1);
            }
            //用户上线
            await eventBus.PublishAsync(new UserOnlineChangeNotificationData()
            {
                Identity = identity,
                Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4(),
                OnlineStatus = UserOnlineStatus.Online
            });
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
            await systemNotificationService.SetUserOffline(identity, this.Context.ConnectionId);
            //分组器
            IEnumerable<ISystemNotificationHubGrouper> groupers = App.GetServices<ISystemNotificationHubGrouper>();
            List<IEnumerable<string>> groupNameGroups = new();
            foreach (var grouper in groupers)
            {
                groupNameGroups.Add(await grouper.GetGroupName(identity));
            }
            //分组完成
            List<Task> tasks1 = new List<Task>();
            foreach (IEnumerable<string> groups in groupNameGroups)
            {
                if (!groups.Any()) continue;
                foreach (var group in groups)
                {
                    tasks1.Add(base.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, group));
                }
            }
            if (tasks1.Any())
            {
                //出组完成
                await Task.WhenAll(tasks1);
            }
            //用户离线
            await eventBus.PublishAsync(new UserOnlineChangeNotificationData()
            {
                Identity = identity,
                Ip = Context.GetHttpContext().GetRemoteIpAddressToIPv4(),
                OnlineStatus = UserOnlineStatus.Offline
            });
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
            var options = App.GetService<IOptions<SignalROptions>>().Value ?? throw new NullReferenceException($"没有{nameof(SignalROptions)}的配置");
            if (options.SystemNotificationHub == null)
                throw new NullReferenceException($"没有对跨域{nameof(SignalROptions.SystemNotificationHub)}进行配置");

            var origins = options.SystemNotificationHub.Origins;

            if (origins == null || origins.Length == 0)
                throw new NullReferenceException($"请至少配置一个域{nameof(SignalROptions.SystemNotificationHub.Origins)}");

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
