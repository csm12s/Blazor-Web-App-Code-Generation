// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Gardener.Application.UserCenter
{
    public interface IUserService
    {
        List<ResourceDto> GetResources([ApiSeat(ApiSeats.ActionStart)] int userId);
        List<RoleDto> GetRoles([ApiSeat(ApiSeats.ActionStart)] int userId);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<UserDto> Search([FromQuery] string name,  int pageIndex = 1, int pageSize = 10);
    }
}