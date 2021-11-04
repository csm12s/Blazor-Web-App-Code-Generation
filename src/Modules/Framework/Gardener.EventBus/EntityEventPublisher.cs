// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.EventBus
{
    public class EntityEventPublisher : IEntityEventPublisher
    {

        private readonly IEventPublisher eventPublisher;

        public EntityEventPublisher(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public async Task PublishAsync<TEntity>(string action, object data, CancellationToken cancellationToken)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>(action);
            eventSource.Payload = data;
            eventSource.CancellationToken = cancellationToken;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task PublishAsync<TEntity>(string action, object data)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>(action);
            eventSource.Payload = data;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task NotifyDeleteAsync<TEntity, TKey>(TKey key)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>("Delete");
            eventSource.Payload = key;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task NotifyDeletesAsync<TEntity, TKey>(IEnumerable<TKey> keys)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>("Deletes");
            eventSource.Payload = keys;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task NotifyFakeDeleteAsync<TEntity, TKey>(TKey key)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>("FakeDelete");
            eventSource.Payload = key;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task NotifyFakeDeletesAsync<TEntity, TKey>(IEnumerable<TKey> keys)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>("FakeDeletes");
            eventSource.Payload = keys;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task NotifyInsertAsync<TEntity>(TEntity entity)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>("Insert");
            eventSource.Payload = entity;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task NotifyUpdateAsync<TEntity>(TEntity entity)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>("Update");
            eventSource.Payload = entity;
            await eventPublisher.PublishAsync(eventSource);
        }
        public async Task NotifyLockAsync<TEntity>(TEntity entity)
        {
            EntityEventSource<TEntity> eventSource = new EntityEventSource<TEntity>("Lock");
            eventSource.Payload = entity;
            await eventPublisher.PublishAsync(eventSource);
        }
    }
}
