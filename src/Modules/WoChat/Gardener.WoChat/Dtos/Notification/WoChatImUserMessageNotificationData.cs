// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;

namespace Gardener.WoChat.Dtos.Notification
{
    /// <summary>
    /// WoChat im 用户 消息通知
    /// </summary>
    public class WoChatImUserMessageNotificationData : NotificationData
    {
        /// <summary>
        /// WoChat im 用户 消息通知
        /// </summary>
        public WoChatImUserMessageNotificationData() : base(NotificationDataType.WoChatImUserMessage)
        {

        }
        /// <summary>
        /// 消息
        /// </summary>
        public ImSessionMessageDto ImMessage { get; set; } = null!;
    }
}
