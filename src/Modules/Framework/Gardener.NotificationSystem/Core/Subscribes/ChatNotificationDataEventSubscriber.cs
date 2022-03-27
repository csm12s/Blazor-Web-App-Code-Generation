// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.EventBus;
using Gardener.Base;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Dtos.Notification;
using Gardener.NotificationSystem.Enums;
using Gardener.NotificationSystem.Services;

namespace Gardener.NotificationSystem.Core.Subscribes
{
    /// <summary>
    /// 系统通知事件订阅
    /// </summary>
    public class ChatNotificationDataEventSubscriber : IEventSubscriber, ISingleton
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public ChatNotificationDataEventSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 聊天数据
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.SystemNotify) + nameof(NotificationDataType.Chat))]
        public async Task Chat(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            EventInfo<NotificationData> eventInfo = (EventInfo<NotificationData>)eventSource.Payload;

            ChatNotificationData chatNotification = System.Text.Json.JsonSerializer.Deserialize<ChatNotificationData>(eventInfo.Data.Data);
            //收到聊天消息，转发给所有客户端
            await systemNotificationService.SendToAllClient(eventInfo.Data);
            ChatDemoService.AddChatMessage(chatNotification);
        }


    }
}
