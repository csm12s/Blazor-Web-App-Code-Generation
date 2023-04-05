// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.Authentication.Dtos;
using Gardener.EventBus;
using Gardener.NotificationSystem.Enums;
using System;

namespace Gardener.NotificationSystem.Dtos
{
    /// <summary>
    /// 系统通知数据
    /// </summary>
    public class NotificationData : EventBase
    {
        /// <summary>
        /// 系统通知数据
        /// </summary>
        public NotificationData()
        {
            this.Time = DateTimeOffset.Now;
            this.TypeAssemblyName = this.GetType().AssemblyQualifiedName;
            this.EventType = EventType.SystemNotify;
        }

        /// <summary>
        /// 系统通知数据
        /// </summary>
        /// <param name="type">通知类型</param>
        public NotificationData(NotificationDataType type):this()
        {
            this.NotificationDataType = type;
        }

        /// <summary>
        /// 程序类型
        /// </summary>
        public string? TypeAssemblyName { get; set; }

        /// <summary>
        /// 通知事件类型
        /// </summary>
        private NotificationDataType _notificationDataType;
        /// <summary>
        /// 通知事件类型
        /// </summary>
        public NotificationDataType NotificationDataType
        {
            get
            {
                return this._notificationDataType;
            }
            set
            {
                this._notificationDataType = value;
                this.EventGroup = value.ToString();
            }
        }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// 发送者身份
        /// </summary>
        public Identity Identity { get; set; } = null!;

        /// <summary>
        /// 用户ip
        /// </summary>
        public string? Ip { get; set; }
    }
}
