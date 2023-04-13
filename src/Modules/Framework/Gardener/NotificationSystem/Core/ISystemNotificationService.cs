// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Cache;
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
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUser(int userId, NotificationData notifyData);

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUsers(IEnumerable<int> userIds, NotificationData notifyData);

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
        Task SendToGroup(IEnumerable<string> groupNames, NotificationData notifyData);

        /// <summary>
        /// 设置用户在线状态为在线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task SetUserOnline(Identity identity);

        /// <summary>
        /// 设置用户在线状态为离线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task SetUserOffline(Identity identity);

        /// <summary>
        /// 判断用户是否在线
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<bool> CheckUserIsOnline(Identity identity);
    }
}
