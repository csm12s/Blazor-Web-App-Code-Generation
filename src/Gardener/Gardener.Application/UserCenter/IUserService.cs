// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application.UserCenter
{
    public interface IUserService
    {
        List<ResourceDto> GetResources(int userId);
        List<RoleDto> GetRoles(int userId);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<UserDto> Search(string name,  int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> SetRoles(int userId, int[] roleIds);
    }
}