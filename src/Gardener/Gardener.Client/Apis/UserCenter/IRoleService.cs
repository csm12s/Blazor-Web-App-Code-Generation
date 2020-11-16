// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System.Threading.Tasks;

namespace Gardener.Client.Apis
{
    public interface IRoleService
    {
        void DeleteResource( int roleId);
        void Resource( int roleId, int[] resourceIds);
        Task<PagedList<RoleDto>> Search( string name,  int pageIndex = 1,  int pageSize = 10);
    }
}