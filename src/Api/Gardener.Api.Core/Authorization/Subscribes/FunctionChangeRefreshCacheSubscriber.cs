// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.EventBus;
using Gardener.Authorization.Core;
using Gardener.Authorization.Dtos;
using Gardener.Cache;
using Gardener.UserCenter.Impl.Domains;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Api.Core.Authorization.Subscribes
{
    /// <summary>
    /// 功能点变化刷新接口点缓存
    /// </summary>
    public class FunctionChangeRefreshCacheSubscriber : IEventSubscriber
    {
        private readonly ICache cache;
        private readonly IApiEndpointQueryService apiEndpointQueryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="apiEndpointQueryService"></param>
        public FunctionChangeRefreshCacheSubscriber( ICache cache, IApiEndpointQueryService apiEndpointQueryService)
        {
            this.cache = cache;
            this.apiEndpointQueryService = apiEndpointQueryService;
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(Function) + ":Delete")]
        [EventSubscribe(nameof(Function) + ":FakeDelete")]
        public async Task Delete(EventHandlerExecutingContext context)   
        {
            IRepository<Function> repository = Db.GetRepository<Function>();
            Guid id = (Guid)context.Source.Payload;
            //移除IApiEndpointQueryService 的 function的缓存
            Function function = await repository.FindAsync(id);
            await apiEndpointQueryService.ClearApiEndpointCacheKey(function.Adapt<ApiEndpoint>());
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(Function) + ":Deletes")]
        [EventSubscribe(nameof(Function) + ":FakeDeletes")]
        public async Task Deletes(EventHandlerExecutingContext context)   
        {
            IRepository<Function> repository = Db.GetRepository<Function>();
            IEnumerable<Guid> ids = (IEnumerable<Guid>)context.Source.Payload;
            foreach (Guid id in ids)
            {
                //移除IApiEndpointQueryService 的 function的缓存
                Function function = await repository.FindAsync(id);
                await apiEndpointQueryService.ClearApiEndpointCacheKey(function.Adapt<ApiEndpoint>());
            }
            
        }
        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(Function) + ":Update")]
        public async Task Update(EventHandlerExecutingContext context)   
        {
            Function function = (Function)context.Source.Payload;
            IApiEndpointQueryService apiEndpointQueryService = App.GetService<IApiEndpointQueryService>();
            await apiEndpointQueryService.ClearApiEndpointCacheKey(function.Adapt<ApiEndpoint>());
        }
    }
}
