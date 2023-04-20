﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.LinqBuilder;
using Gardener.Authentication.Dtos;
using Gardener.Cache;
using Gardener.Common;
using Gardener.NotificationSystem.Dtos;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public Task SendToGroups(IEnumerable<string> groupNames, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData, notifyData.GetType());
            return hubContext.Clients.Groups(groupNames).SendAsync(method, json);
        }
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUser">接收用户</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToUser(Identity receiveUser, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData);
            return hubContext.Clients.User(GetUserId(receiveUser)).SendAsync(method, json);
        }
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUsers">接收用户集合</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public Task SendToUsers(IEnumerable<Identity> receiveUsers, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData);
            return hubContext.Clients.Users(receiveUsers.Select(x => GetUserId(x))).SendAsync(method, json);
        }
        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private string GetUserId(Identity identity)
        {
            return $"{identity.IdentityType}_{identity.Id}";
        }
        /// <summary>
        /// 设置用户在线状态为在线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task SetUserOnline(Identity identity, string connectionId)
        {
            string key1 = $"SystemNotification:ConnectionIds:{identity.IdentityType}:{identity.Id}";
            string key2 = $"SystemNotification:OnlineState:{identity.IdentityType}:{identity.Id}";

            List<string>? connectionIds = null;
            //已在线
            if (await CheckUserIsOnline(identity))
            {
                //已有链接
                connectionIds = await cache.GetAsync<List<string>>(key1);
            }
            if (connectionIds != null)
            {
                //新链接加入
                connectionIds.Add(connectionId);
            }
            else
            {
                //只有本次链接
                connectionIds = new List<string> { connectionId };
            }
            await Task.WhenAll(cache.SetAsync(key2, 1), cache.SetAsync(key1, connectionIds));
        }

        /// <summary>
        /// 设置用户在线状态为离线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task SetUserOffline(Identity identity, string connectionId)
        {
            string key1 = $"SystemNotification:ConnectionIds:{identity.IdentityType}:{identity.Id}";
            string key2 = $"SystemNotification:OnlineState:{identity.IdentityType}:{identity.Id}";
            List<string>? connectionIds = await cache.GetAsync<List<string>>(key1);
            if (connectionIds == null)
            {
                //没有链接
                await cache.SetAsync(key2, 0);
            }
            else if (connectionIds.Count == 1 && connectionIds.First().Equals(connectionId))
            {
                //只有自己一个链接
                await cache.SetAsync(key2, 0);
                await cache.RemoveAsync(key1);

            }
            else
            {
                //多个链接
                connectionIds.Remove(connectionId);
                await cache.SetAsync(key1, connectionIds);
            }
        }

        /// <summary>
        /// 判断用户是否在线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserIsOnline(Identity identity)
        {
            string key = $"SystemNotification:OnlineState:{identity.IdentityType}:{identity.Id}";
            return await cache.GetAsync<int>(key, () => Task.FromResult(0))==1;
        }
        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或链接信息不存在，无法设置
        /// </remarks>
        public async Task<bool> UserGroupAdd(string groupName, Identity identity)
        {
            if (!await CheckUserIsOnline(identity))
            {
                return false;
            }
            string key1 = $"SystemNotification:ConnectionIds:{identity.IdentityType}:{identity.Id}";
            List<string>? connectionIds = await cache.GetAsync<List<string>>(key1);
            if (connectionIds == null)
            {
                return false;
            }
            await connectionIds.ForEachAsync(connectionId =>
            {
                return hubContext.Groups.AddToGroupAsync(connectionId, groupName);
            });
            return true;
        }
        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果链接信息不存在，无法设置
        /// </remarks>
        public async Task<bool> UserGroupRemove(string groupName, Identity identity)
        {
            string key1 = $"SystemNotification:ConnectionIds:{identity.IdentityType}:{identity.Id}";
            List<string>? connectionIds = await cache.GetAsync<List<string>>(key1);
            if (connectionIds == null || !connectionIds.Any())
            {
                return false;
            }
            await connectionIds.ForEachAsync(connectionId =>
            {
                return hubContext.Groups.RemoveFromGroupAsync(connectionId, groupName);
            });
            return true;
        }
    }
}
