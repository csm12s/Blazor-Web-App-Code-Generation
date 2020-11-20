// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.Dtos;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 角色服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class RoleService : ServiceBase<Role, RoleDto>, IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RoleResource> _roleResourceRepository;
        /// <summary>
        /// 角色服务
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="roleResourceRepository"></param>
        public RoleService(IRepository<Role> roleRepository, IRepository<RoleResource> roleResourceRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
            _roleResourceRepository = roleResourceRepository;
        }

        /// <summary>
        /// 搜索角色
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedList<RoleDto>> Search([FromQuery] string name,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            return await _roleRepository
                .Where(!string.IsNullOrEmpty(name), x => x.Name.Contains(name))
                .Where(x => x.IsDeleted==false)
                .OrderByDescending(x=>x.CreatedTime)
                .Select(x => x.Adapt<RoleDto>())
                .ToPagedListAsync<RoleDto>(pageIndex, pageSize);
        }

        /// <summary>
        /// 为角色分配权限（重置）
        /// </summary>
        public async void Resource([ApiSeat(ApiSeats.ActionStart)] int roleId, int[] resourceIds)
        {
            resourceIds ??= Array.Empty<int>();

            DeleteResource(roleId);

            var list = new List<RoleResource>();
            foreach (var securityId in resourceIds)
            {
                list.Add(new RoleResource { RoleId = roleId, ResourceId = securityId, CreatedTime = DateTimeOffset.Now });
            }
            await _roleResourceRepository.InsertAsync(list);
        }

        /// <summary>
        /// 删除角色的所有资源
        /// </summary>
        /// <param name="roleId"></param>
        public async void DeleteResource([ApiSeat(ApiSeats.ActionStart)] int roleId)
        {
            var entitys = _roleResourceRepository.Where(u => u.RoleId == roleId, false);

            await _roleResourceRepository.DeleteAsync(entitys);
        }
    }
}
