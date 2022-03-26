// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Client.Base;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;

namespace Gardener.NotificationSystem.Client.Core.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [TransientService]
    public class UserOnlineChangeNotificationEventSubscriber : IEventSubscriber<EventInfo<NotificationData>>
    {
        private readonly IClientNotifier clientNotifier;
        private readonly IAuthenticationStateManager authStateManager;

        public UserOnlineChangeNotificationEventSubscriber(IClientNotifier clientNotifier, IAuthenticationStateManager authStateManager)
        {
            this.clientNotifier = clientNotifier;
            this.authStateManager = authStateManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Ignore(EventInfo<NotificationData> e)
        {
            if (e == null || e.Data==null)
            {
                return true;
            }
            if (!e.EventType.Equals(EventType.SystemNotify))
            {
                return true;
            }
            if (!e.Data.Type.Equals(NotificationDataType.UserOnline))
            {
                return true; 
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CallBack(EventInfo<NotificationData> e)
        {
            NotificationData notificationData = e.Data;
            //无数据 或 无身份 或 非user身份
            if (notificationData == null || notificationData.Identity==null || !notificationData.Identity.IdentityType.Equals(IdentityType.User))
            {
                return;
            }
            var user = await authStateManager.GetCurrentUser();
            //自己
            if (user.Id.ToString() == notificationData.Identity.Id) 
            {
                return;
            }

            UserOnlineChangeNotificationData userOnlineNotification=System.Text.Json.JsonSerializer.Deserialize<UserOnlineChangeNotificationData>(notificationData.Data);
            if (userOnlineNotification.OnlineStatus.Equals(UserOnlineStatus.Online))
            { 
                await clientNotifier.Info("用户上线通知",$"{notificationData.Identity.GivenName} 刚刚上线了<br/>IP:[{notificationData.Ip}]");
            }else if (userOnlineNotification.OnlineStatus.Equals(UserOnlineStatus.Offline))
            {
                await clientNotifier.Info("用户离线通知", $"{notificationData.Identity.GivenName} 刚刚离线了<br/>IP:[{notificationData.Ip}]");
            }
        }
    }
}
