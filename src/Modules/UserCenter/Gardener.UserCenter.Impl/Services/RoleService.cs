// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gardener.UserCenter.Services;
using Gardener.SystemManager.Dtos;
using Gardener.EntityFramwork;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 角色服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
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
            resourceIds ??= Array.Empty<Guid>();

            //所有现有关系
            var entitys =await _roleResourceRepository.Where(u => u.RoleId == roleId, false).ToListAsync();
            //需要删除
            List<RoleResource> needDelete = new List<RoleResource>();
            entitys.ForEach(x => {

                if (!resourceIds.Any(r => r.Equals(x.ResourceId))) {
                    needDelete.Add(x);
                }
            });
            if (needDelete.Any())
            { 
                await _roleResourceRepository.DeleteAsync(needDelete);
            }

            //需要添加
            List<RoleResource> needAdd = new List<RoleResource>();
            resourceIds.ToList().ForEach(id => {
                if (!entitys.Any(r => r.ResourceId.Equals(id))) 
                {
                    needAdd.Add(new RoleResource { RoleId = roleId, ResourceId = id, CreatedTime = DateTimeOffset.Now });
                }
            });
            if (needAdd.Any())
            { 
                await _roleResourceRepository.InsertAsync(needAdd);
            }
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
                .Where(x => x.RoleId == roleId && x.Resource.IsDeleted==false && x.Resource.IsLocked==false)
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
                sb.Append($"{nameof(RoleResource.CreatedTime)}= DateTimeOffset.FromUnixTimeSeconds({DateTimeOffset.Now.ToUnixTimeSeconds()})");
                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
