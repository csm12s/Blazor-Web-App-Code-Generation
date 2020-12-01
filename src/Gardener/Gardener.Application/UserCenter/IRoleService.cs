// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application.UserCenter
{
    public interface IRoleService
    {
        Task<bool> DeleteResource([ApiSeat(ApiSeats.ActionStart)] int roleId);
        Task<bool> Resource([ApiSeat(ApiSeats.ActionStart)] int roleId, int[] resourceIds);
        Task<PagedList<RoleDto>> Search([FromQuery] string name,  int pageIndex = 1,  int pageSize = 10);
        /// <summary>
        /// 获取有效的角色
        /// </summary>
        /// <returns></returns>
        Task<List<RoleDto>> GetEffective();
        /// <summary>
        /// 获取角色所有资源
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetResource(int roleId);
    }
}