﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Cache;
using Gardener.NotificationSystem.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public class SystemNotificationService : ISystemNotificationService
    {
        private readonly string method = "ReceiveMessage";
        private readonly IHubContext<SystemNotificationHub> hubContext;
        private readonly ICache cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="cache"></param>
        public SystemNotificationService(IHubContext<SystemNotificationHub> hubContext, ICache cache)
        {
            this.hubContext = hubContext;
            this.cache = cache;
        }
        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToAllClient(NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData, notifyData.GetType());
            return hubContext.Clients.All.SendAsync(method, json);
        }
        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToGroup(string groupName, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData, notifyData.GetType());
            return hubContext.Clients.Group(groupName).SendAsync(method, json);
        }
        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupNames"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToGroup(IEnumerable<string> groupNames, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData, notifyData.GetType());
            return hubContext.Clients.Groups(groupNames).SendAsync(method, json);
        }
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToUser(int userId, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData);
            return hubContext.Clients.User(userId.ToString()).SendAsync(method, json);
        }
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToUsers(IEnumerable<int> userIds, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData);
            return hubContext.Clients.Users(userIds.Select(x => x.ToString())).SendAsync(method, json);
        }

        /// <summary>
        /// 设置用户在线状态为在线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task SetUserOnline(Identity identity)
        {
            string key = $"SystemNotification:OnlineState:{identity.IdentityType}:{identity.Id}";
            return cache.SetAsync(key, 1);
        }

        /// <summary>
        /// 设置用户在线状态为离线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task SetUserOffline(Identity identity)
        {
            string key = $"SystemNotification:OnlineState:{identity.IdentityType}:{identity.Id}";
            return cache.SetAsync(key, 0);
        }

        /// <summary>
        /// 判断用户是否在线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<bool> CheckUserIsOnline(Identity identity)
        {
            string key = $"SystemNotification:OnlineState:{identity.IdentityType}:{identity.Id}";
            return cache.GetAsync<bool>(key, () => Task.FromResult(false));
        }
    }
}
