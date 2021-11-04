using Furion.EventBus;
using System;
using System.Threading;

namespace Gardener.EventBus
{
    /// <summary>
    /// 实体变化事件源
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityEventSource<TEntity> : IEventSource
    {
        public EntityEventSource(string action)
        {
            this.action = action;
        }

        private string action;

        public string EventId {
            get {
                Type type = typeof(TEntity);
                return type.Name+":"+action;
            }
        }

        public object Payload { get; set; }

        public DateTime CreatedTime { get; } = DateTime.UtcNow;

        public CancellationToken CancellationToken { get; set; }
}
}
