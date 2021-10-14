// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.EventBridge;
using Gardener.UserCenter.Impl.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Impl.Core.Subscribes
{
    /// <summary>
    /// 功能点变化
    /// </summary>
    [EventHandler(nameof(Function))]
    public class FunctionChangeSubscribeHandler : IEventHandler
    {
        private readonly IRepository<ResourceFunction> resourceFunctionRepository;
        private readonly IRepository<ClientFunction> clientFunctionRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFunctionRepository"></param>
        /// <param name="clientFunctionRepository"></param>
        public FunctionChangeSubscribeHandler(IRepository<ResourceFunction> resourceFunctionRepository, IRepository<ClientFunction> clientFunctionRepository)
        {
            this.resourceFunctionRepository = resourceFunctionRepository;
            this.clientFunctionRepository = clientFunctionRepository;
        }

        

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="eventMessage"></param>
        [EventMessage]
        public async Task Delete(EventMessage<Guid> eventMessage)
        {
            Guid id = eventMessage.Payload;
            await resourceFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => id.Equals(x.FunctionId)));
            await clientFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => id.Equals(x.FunctionId)));
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="eventMessage"></param>
        [EventMessage]
        public async Task Deletes(EventMessage<Guid[]> eventMessage)
        {
            List<Guid> ids= eventMessage.Payload.ToList();
            await resourceFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => ids.Contains(x.FunctionId)));
            await clientFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => ids.Contains(x.FunctionId)));
        }
        
    }
}
