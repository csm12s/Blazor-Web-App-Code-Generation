// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using Gardener.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IUserService : IServiceBase<UserDto>
    {
        Task<ApiResult<List<ResourceDto>>> GetResources( int userId);
        Task<ApiResult<List<RoleDto>>> GetRoles(int userId);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ApiResult<PagedList<UserDto>>> Search(string name, int pageIndex = 1,  int pageSize = 10);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> SetRoles(int userId, int[] roleIds);
    }
}
