// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IRoleService: IServiceBase<RoleDto>
    {
        void DeleteResource( int roleId);
        void Resource( int roleId, int[] resourceIds);
        Task<ApiResult<PagedList<RoleDto>>> Search( string name,  int pageIndex = 1,  int pageSize = 10);
        /// <summary>
        /// 获取有效的角色
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<RoleDto>>> GetEffective();
    }
}