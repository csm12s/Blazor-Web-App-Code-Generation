// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.NotificationSystem.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public interface ISystemNotificationService
    {

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToAllClient(NotificationData notifyData);

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUser">接收用户</param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUser(Identity receiveUser, NotificationData notifyData);

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="receiveUsers"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUsers(IEnumerable<Identity> receiveUsers, NotificationData notifyData);

        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToGroup(string groupName, NotificationData notifyData);

        /// <summary>
        /// 向指定用户组发送信息
        /// </summary>
        /// <param name="groupNames"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToGroups(IEnumerable<string> groupNames, NotificationData notifyData);

        /// <summary>
        /// 设置用户在线状态为在线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task SetUserOnline(Identity identity, string connectionId);

        /// <summary>
        /// 设置用户在线状态为离线
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task SetUserOffline(Identity identity, string connectionId);

        /// <summary>
        /// 判断用户是否在线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<bool> CheckUserIsOnline(Identity identity);

        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或链接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupAdd(string groupName, Identity identity);

        /// <summary>
        /// 设置用户到某个分组
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果不在线或链接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupAdd<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper;

        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果链接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupRemove(string groupName, Identity identity);

        /// <summary>
        /// 移除用户的某个分组
        /// </summary>
        /// <typeparam name="TSystemNotificationHubGrouper"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果链接信息不存在，无法设置
        /// </remarks>
        Task<bool> UserGroupRemove<TSystemNotificationHubGrouper>(Identity identity) where TSystemNotificationHubGrouper : ISystemNotificationHubGrouper;
    }
}
