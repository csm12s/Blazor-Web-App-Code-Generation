// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.EventBus
{
    /// <summary>
    /// 事件消息
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class EventInfo<TData> : EventBase
    {
        /// <summary>
        /// 事件消息
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="groupName"></param>
        /// <param name="data"></param>
        public EventInfo(EventType eventType, string groupName, TData data) : base(eventType, groupName)
        {
            Data = data;
        }

        /// <summary>
        /// 事件消息
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="data"></param>
        public EventInfo(EventType eventType, TData data) : base(eventType)
        {
            Data = data;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public TData Data { get; set; }

    }
}
