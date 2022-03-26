// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Client.Base;
using Gardener.Client.Entry.Pages;
using Gardener.EventBus;
using Gardener.NotificationSystem;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Dtos.Notification;
using Gardener.NotificationSystem.Enums;
using Gardener.UserCenter.Dtos;
using System.Threading.Tasks;

namespace Gardener.Client.Entry.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [TransientService]
    public class NotificationEventSubscriber : IEventSubscriber<EventInfo<NotificationData>>
    {
        private readonly IClientNotifier clientNotifier;
        private readonly IAuthenticationStateManager authStateManager;
        private readonly IAuthenticationStateManager authenticationStateManager;

        public NotificationEventSubscriber(IClientNotifier clientNotifier, IAuthenticationStateManager authStateManager, IAuthenticationStateManager authenticationStateManager)
        {
            this.clientNotifier = clientNotifier;
            this.authStateManager = authStateManager;
            this.authenticationStateManager = authenticationStateManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Ignore(EventInfo<NotificationData> e)
        {
            if (e == null)
            {
                return true;
            }
            if (!e.EventType.Equals(EventType.SystemNotify))
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
            if (notificationData.Type.Equals(NotificationDataType.Chat))
            {
                ChatNotificationData chatData = System.Text.Json.JsonSerializer.Deserialize<ChatNotificationData>(notificationData.Data);
                Home.ShowMessages.Invoke(chatData);
            }
            else if (notificationData.Type.Equals(NotificationDataType.UserOnline))
            {
                UserDto user= await authenticationStateManager.GetCurrentUser();
                if (user.Id.ToString().Equals(notificationData.Identity.Id))
                {
                    return;
                }
                UserOnlineChangeNotificationData userOnlineNotification = System.Text.Json.JsonSerializer.Deserialize<UserOnlineChangeNotificationData>(notificationData.Data);
                //用户上下线
                ChatNotificationData chatData = new ChatNotificationData();
                chatData.Avatar = "./assets/logo.png";
                chatData.NickName = "系统";
                if (userOnlineNotification.OnlineStatus.Equals(UserOnlineStatus.Online))
                {
                    chatData.Message = $"{notificationData.Identity.GivenName} 刚刚上线了。IP:[{notificationData.Ip}]";
                }
                else if (userOnlineNotification.OnlineStatus.Equals(UserOnlineStatus.Offline))
                {
                    chatData.Message = $"{notificationData.Identity.GivenName} 刚刚离线了。IP:[{notificationData.Ip}]";
                }
                Home.ShowMessages.Invoke(chatData);
            }
        }
    }
}
