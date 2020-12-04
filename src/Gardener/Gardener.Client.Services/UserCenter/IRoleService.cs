// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IRoleService: IServiceBase<RoleDto>
    {
        Task<ApiResult<bool>> DeleteResource( int roleId);
        Task<ApiResult<bool>> Resource( int roleId, Guid[] resourceIds);
        Task<ApiResult<PagedList<RoleDto>>> Search( string name,  int pageIndex = 1,  int pageSize = 10);
        /// <summary>
        /// 获取有效的角色
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<RoleDto>>> GetEffective();
        /// <summary>
        /// 获取角色所有资源
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ApiResult<List<ResourceDto>>> GetResource(int roleId);
    }
}