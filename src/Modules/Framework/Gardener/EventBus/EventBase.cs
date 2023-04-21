// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.EventBus
{
    /// <summary>
    /// 事件基类
    /// </summary>
    public class EventBase
    {
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <remarks>
        /// <para>事件类型 默认 <see cref="EventType.SystemNotify"/></para>
        /// <para>此类事件下分组 默认是 <see cref="string.Empty"/></para>
        /// </remarks>
        public EventBase()
        {
        }
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <param name="eventGroup">此类事件下分组</param>
        /// <remarks>
        /// 事件类型 默认 <see cref="EventType.SystemNotify"/>
        /// </remarks>
        public EventBase(string eventGroup)
        {
            EventGroup = eventGroup;
        }
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <remarks>
        /// <para>此类事件下分组 默认是 <see cref="string.Empty"/></para>
        /// </remarks>
        public EventBase(EventType eventType)
        {
            EventType = eventType;
        }
        /// <summary>
        /// 事件基类
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventGroup">此类事件下分组</param>
        public EventBase(EventType eventType, string eventGroup)
        {
            EventType = eventType;
            EventGroup = eventGroup;
        }

        /// <summary>
        /// 唯一编号
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// 事件组
        /// </summary>
        public string EventGroup { get; set; } = string.Empty;

        /// <summary>
        /// 事件类型
        /// </summary>
        public EventType EventType { get; set; } = EventType.SystemNotify;
    }
}
