// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.EventBus;
using Gardener.EventBus;
using Gardener.NotificationSystem.Enums;

namespace Gardener.NotificationSystem.Core.Subscribes
{
    /// <summary>
    /// 用户在线状态变化通知事件
    /// </summary>
    public class UserOnlineChangeNotificationDataEventSubscriber : IEventSubscriber, ISingleton
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 用户在线状态变化通知事件
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public UserOnlineChangeNotificationDataEventSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 用户在线状态变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.SystemNotify) + nameof(NotificationDataType.UserOnlineChange))]
        public async Task Chat(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            UserOnlineChangeNotificationData chatNotification = (UserOnlineChangeNotificationData)eventSource.Payload;
            //收到消息，转发给所有客户端
            await systemNotificationService.SendToAllClient(chatNotification);
        }
    }
}
