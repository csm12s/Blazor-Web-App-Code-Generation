// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.EventBus;
using Gardener.Enums;
using Gardener.EventBus;
using Gardener.UserCenter.Impl.Domains;
using System;

namespace Gardener.UserCenter.Impl.Core.Subscribes
{
    /// <summary>
    /// 功能点变化
    /// </summary>
    public class FunctionChangeSubscribeHandler : ISubscribeHandler
    {
        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="payload"></param>
        [SubscribeMessage(nameof(Function))]   
        public void Change(string eventId, object payload)   
        {
            if (payload is EntityChangeEvent<Function, Guid> entityChange) 
            {
                Scoped.CreateUow(async (_, scope) => {
                    var services = scope.ServiceProvider;
                    if (EntityOperationType.Delete.Equals(entityChange.Operation))
                    {
                        Guid id = entityChange.GetKey(x => x.Id);
                        //移除与function相关联的关系数据
                        var resourceFunctionRepository = Db.GetRepository<ResourceFunction>(services);
                        await resourceFunctionRepository.DeleteNowAsync(resourceFunctionRepository.Where(x => x.FunctionId.Equals(id)));

                        var clientFunctionRepository = Db.GetRepository<ClientFunction>(services);
                        await clientFunctionRepository.DeleteNowAsync(clientFunctionRepository.Where(x => x.FunctionId.Equals(id)));
                    }
                });
            }
        }
    }
}
