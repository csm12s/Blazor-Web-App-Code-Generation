// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 客户端与接口关系服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class ClientFunctionService : IClientFunctionService, IDynamicApiController
    {

        private readonly IRepository<ClientFunction> repository;
        /// <summary>
        /// 客户端与接口关系服务
        /// </summary>
        /// <param name="repository"></param>
        public ClientFunctionService(IRepository<ClientFunction> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 添加客户端与接口关系
        /// </summary>
        /// <param name="clientFunctionDtos"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<ClientFunctionDto> clientFunctionDtos)
        {
            await repository.InsertAsync(clientFunctionDtos.Select(x => x.Adapt<ClientFunction>()));
            return true;
        }

        /// <summary>
        /// 删除客户端与接口关系
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public async Task<bool> Delete([FromRoute] Guid clientId, [FromRoute] Guid functionId)
        {
            List<ClientFunction> entitys = await repository.Where(x => x.ClientId.Equals(clientId) && x.FunctionId.Equals(functionId)).ToListAsync();

            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }

            await repository.DeleteAsync(entitys);

            return true;
        }
    }
}
