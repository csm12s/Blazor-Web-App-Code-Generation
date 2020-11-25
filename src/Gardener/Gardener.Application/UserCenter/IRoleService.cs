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
        void DeleteResource([ApiSeat(ApiSeats.ActionStart)] int roleId);
        void Resource([ApiSeat(ApiSeats.ActionStart)] int roleId, int[] resourceIds);
        Task<PagedList<RoleDto>> Search([FromQuery] string name,  int pageIndex = 1,  int pageSize = 10);
    }
}