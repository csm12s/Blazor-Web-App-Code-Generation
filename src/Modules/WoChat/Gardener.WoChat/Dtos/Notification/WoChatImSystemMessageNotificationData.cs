// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;
using Gardener.WoChat.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.WoChat.Dtos.Notification
{
    /// <summary>
    /// WoChat im 系统 消息通知
    /// </summary>
    public class WoChatImSystemMessageNotificationData : NotificationData
    {
        /// <summary>
        /// WoChat im 系统 消息通知
        /// </summary>
        public WoChatImSystemMessageNotificationData() : base(NotificationDataType.WoChatImSystemMessage)
        {

        }
        /// <summary>
        /// 会话编号
        /// </summary>
        public Guid ImSessionId { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public ImSystemMessageType MessageType { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int? UserId { get; set; }
    }
}
