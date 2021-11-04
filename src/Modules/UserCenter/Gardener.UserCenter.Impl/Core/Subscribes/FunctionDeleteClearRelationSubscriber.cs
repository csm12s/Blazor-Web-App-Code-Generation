// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.EventBus;
using Gardener.EventBus;
using Gardener.UserCenter.Impl.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Impl.Core.Subscribes
{
    /// <summary>
    /// 功能点变化清除关联关系
    /// </summary>
    public class FunctionDeleteClearRelationSubscriber : IEventSubscriber
    {

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(Function)+ ":Delete")]
        [EventSubscribe(nameof(Function)+ ":FakeDelete")]
        public async Task Delete(EventHandlerExecutingContext context)
        {
            IRepository<ResourceFunction> resourceFunctionRepository= Db.GetRepository<ResourceFunction>();
            IRepository<ClientFunction> clientFunctionRepository= Db.GetRepository<ClientFunction>();
            Guid id = (Guid)context.Source.Payload;
            await resourceFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => id.Equals(x.FunctionId)));
            await clientFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => id.Equals(x.FunctionId)));
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(Function) + ":Deletes")]
        [EventSubscribe(nameof(Function) + ":FakeDeletes")]
        public async Task Deletes(EventHandlerExecutingContext context)
        {
            IRepository<ResourceFunction> resourceFunctionRepository = Db.GetRepository<ResourceFunction>();
            IRepository<ClientFunction> clientFunctionRepository = Db.GetRepository<ClientFunction>();
            IEnumerable<Guid> ids= (IEnumerable<Guid>)context.Source.Payload;
            await resourceFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => ids.Contains(x.FunctionId)));
            await clientFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => ids.Contains(x.FunctionId)));
        }
        
    }
}
