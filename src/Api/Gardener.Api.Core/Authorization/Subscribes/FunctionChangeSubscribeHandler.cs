// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.EventBridge;
using Gardener.Authorization.Core;
using Gardener.Cache;
using Gardener.UserCenter.Impl.Domains;
using System;
using System.Threading.Tasks;

namespace Gardener.Api.Core.Authorization.Subscribes
{
    /// <summary>
    /// 功能点变化
    /// </summary>
    [EventHandler(nameof(Function))]
    public class FunctionChangeSubscribeHandler : IEventHandler
    {
        private readonly IRepository<Function> repository;
        private readonly ICache cache;
        private readonly IApiEndpointQueryService apiEndpointQueryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cache"></param>
        /// <param name="apiEndpointQueryService"></param>
        public FunctionChangeSubscribeHandler(IRepository<Function> repository, ICache cache, IApiEndpointQueryService apiEndpointQueryService)
        {
            this.repository = repository;
            this.cache = cache;
            this.apiEndpointQueryService = apiEndpointQueryService;
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="eventMessage"></param>
        [EventMessage]   
        public async Task Delete(EventMessage<Guid> eventMessage)   
        {
            Guid id = eventMessage.Payload;
            //移除IApiEndpointQueryService 的 function的缓存
            Function function = await repository.FindAsync(id);
            await cache.RemoveAsync(apiEndpointQueryService.GetApiEndpointCacheKey(function.Key));
            await cache.RemoveAsync(apiEndpointQueryService.GetApiEndpointCacheKey(function.Path, function.Method));
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="eventMessage"></param>
        [EventMessage]   
        public async Task Deletes(EventMessage<Guid []> eventMessage)   
        {
            Guid [] ids = eventMessage.Payload;
            foreach(Guid id in ids)
            {
                //移除IApiEndpointQueryService 的 function的缓存
                Function function = await repository.FindAsync(id);
                await cache.RemoveAsync(apiEndpointQueryService.GetApiEndpointCacheKey(function.Key));
                await cache.RemoveAsync(apiEndpointQueryService.GetApiEndpointCacheKey(function.Path, function.Method));
            }
            
        }
        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="eventMessage"></param>
        [EventMessage]   
        public async Task Update(EventMessage<Function> eventMessage)   
        {
            Function function = eventMessage.Payload; 
            IApiEndpointQueryService apiEndpointQueryService = App.GetService<IApiEndpointQueryService>();
            await cache.RemoveAsync(apiEndpointQueryService.GetApiEndpointCacheKey(function.Key));
            await cache.RemoveAsync(apiEndpointQueryService.GetApiEndpointCacheKey(function.Path, function.Method));
        }
    }
}
