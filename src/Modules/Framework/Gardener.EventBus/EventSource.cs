// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using System;
using System.Threading;

namespace Gardener.EventBus
{
    /// <summary>
    /// 基础事件源
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class EventSource<TBody> : IEventSource
    {
        /// <summary>
        /// 基础事件源
        /// </summary>
        /// <param name="eventId">事件唯一编号</param>
        public EventSource(string eventId)
        {
            this.eventId = eventId;
        }
        /// <summary>
        /// 事件唯一编号
        /// </summary>
        private string eventId;
        /// <summary>
        /// 内容
        /// </summary>
        public TBody? Body { get; set; }

        public DateTime CreatedTime { get; } = DateTime.UtcNow;

        public CancellationToken CancellationToken { get; set; }

        public string EventId { get { return eventId; } }

        public object? Payload { get { return Body; } }

    }
}
