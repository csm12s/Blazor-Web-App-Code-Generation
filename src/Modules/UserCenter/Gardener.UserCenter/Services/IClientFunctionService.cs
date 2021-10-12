// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.UserCenter.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClientFunctionService
    {
        /// <summary>
        /// 添加客户端与接口关系
        /// </summary>
        /// <param name="clientFunctionDtos"></param>
        /// <returns></returns>
        Task<bool> Add(List<ClientFunctionDto> clientFunctionDtos);

        /// <summary>
        /// 删除客户端与接口关系
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid clientId, Guid functionId);
    }
}
