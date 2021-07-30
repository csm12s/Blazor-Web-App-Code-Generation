// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Application
{
    /// <summary>
    /// 角色服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class RoleService : ApplicationServiceBase<Role, RoleDto>, IRoleService
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
        /// 分配权限
        /// </summary>
        /// <remarks>
        /// 分配权限（重置）
        /// </remarks>
        /// <param name="roleId"></param>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        public async Task<bool> Resource([ApiSeat(ApiSeats.ActionStart)] int roleId,[FromBody] Guid[] resourceIds)
        {
            //先删除所有资源
            await DeleteResource(roleId);
            resourceIds ??= Array.Empty<Guid>();
            var list = new List<RoleResource>();
            foreach (var securityId in resourceIds)
            {
                list.Add(new RoleResource { RoleId = roleId, ResourceId = securityId, CreatedTime = DateTimeOffset.Now });
            }
            await _roleResourceRepository.InsertAsync(list);
            return true;
        }

        /// <summary>
        /// 根据角色编号删除所有资源
        /// </summary>
        /// <remarks>
        /// 根据角色编号删除所有资源
        /// </remarks>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteResource([ApiSeat(ApiSeats.ActionStart)] int roleId)
        {
            var entitys = _roleResourceRepository.Where(u => u.RoleId == roleId, false);

            await _roleResourceRepository.DeleteAsync(entitys);

            return true;
        }
        /// <summary>
        /// 获取可用角色
        /// </summary>
        /// <remarks>
        /// 获取可用角色
        /// </remarks>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetEffective()
        {
            return await _roleRepository.AsQueryable()
                .Where(x => x.IsDeleted == false && x.IsLocked == false)
                .Select(x => x.Adapt<RoleDto>())
                .ToListAsync();
        }
        /// <summary>
        /// 获取角色所有资源
        /// </summary>
        /// <remarks>
        /// 获取角色所有资源
        /// </remarks>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetResource([ApiSeat(ApiSeats.ActionStart)] int roleId)
        {
            var resources= await _roleResourceRepository
                .Include(x => x.Resource)
                .Where(x => x.RoleId == roleId && x.Resource.IsDeleted==false)
                .Select(x => x.Resource)
                .ToListAsync();

            return resources.Select(x=>x.Adapt<ResourceDto>()).ToList();
        }
        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <remarks>
        /// 获取种子数据
        /// </remarks>
        /// <returns></returns>
        public async Task<string> GetRoleResourceSeedData()
        {
            List<RoleResource> roleResources =await _roleResourceRepository.AsQueryable(false).OrderBy(x => x.RoleId).ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var roleResource in roleResources)
            {
                sb.Append($"\r\n new {nameof(RoleResource)}()");
                sb.Append("{");
                sb.Append($"{nameof(RoleResource.RoleId)}={roleResource.RoleId},");
                sb.Append($"{nameof(RoleResource.ResourceId)} = Guid.Parse(\"{roleResource.ResourceId}\"),");
                sb.Append($"{nameof(RoleResource.CreatedTime)}= DateTimeOffset.Now");
                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
