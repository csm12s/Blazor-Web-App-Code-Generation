// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Gardener.Core;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 角色服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class RoleService : ServiceBase<Role,RoleDto>
    {
        private readonly IRepository<Role> _roleRepository;
        /// <summary>
        /// 角色服务
        /// </summary>
        /// <param name="roleRepository"></param>
        public RoleService(IRepository<Role> roleRepository):base(roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 搜索角色
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedList<RoleDto>> Search([FromQuery]string name, 
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            return await _roleRepository
                .Where(!string.IsNullOrEmpty(name), x => x.Name.Contains(name))
                .Select(x => x.Adapt<RoleDto>())
                .ToPagedListAsync<RoleDto>(pageIndex, pageSize);
        }
    }
}
