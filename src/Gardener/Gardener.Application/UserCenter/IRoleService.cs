// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Gardener.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gardener.Application.UserCenter
{
    public interface IRoleService
    {
        void DeleteResource([ApiSeat(ApiSeats.ActionStart)] int roleId);
        void Resource([ApiSeat(ApiSeats.ActionStart)] int roleId, int[] resourceIds);
        Task<PagedList<RoleDto>> Search([FromQuery] string name, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10);
    }
}