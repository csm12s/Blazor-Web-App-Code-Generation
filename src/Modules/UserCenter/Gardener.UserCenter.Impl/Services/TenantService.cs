// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.Base.Entity.Domains;
using Gardener.EntityFramwork;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gardener.Base.Dto;
using Gardener.SystemManager.Dtos;
using Humanizer.Localisation;
using System.Globalization;
using Mapster;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class TenantService : ServiceBase<SystemTenant, SystemTenantDto, Guid>, ITenantService
    {
        private readonly IRepository<SystemTenantResource, GardenerMultiTenantDbContextLocator> _tenantResourceRepository;
        /// <summary>
        /// 租户服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="tenantResourceRepository"></param>
        public TenantService(IRepository<SystemTenant> repository, IRepository<SystemTenantResource, GardenerMultiTenantDbContextLocator> tenantResourceRepository) : base(repository)
        {
            _tenantResourceRepository = tenantResourceRepository;
        }
        /// <summary>
        /// 为租户添加资源
        /// </summary>
        /// <param name="tenantId">租户编号</param>
        /// <param name="resourceIds">资源编号</param>
        /// <returns></returns>
        public async Task<bool> AddResources([ApiSeat(ApiSeats.ActionStart)] Guid tenantId, [FromBody] Guid[] resourceIds)
        {
            List<SystemTenantResource> tenantResources = await _tenantResourceRepository.AsQueryable(false).Where(x => x.TenantId.Equals(tenantId)).ToListAsync();
            //删除
            foreach (SystemTenantResource tenantResource in tenantResources)
            {
                await _tenantResourceRepository.DeleteAsync(tenantResource);
            }
            if (resourceIds.Any())
            {
                //写入
                List<SystemTenantResource> addTenantResources = new List<SystemTenantResource>();
                await _tenantResourceRepository.InsertAsync(resourceIds.Select(x => new SystemTenantResource() { TenantId = tenantId, ResourceId = x }));
            }
            return true;
        }
        /// <summary>
        /// 获取租户资源列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<ResourceDto>> GetResources([ApiSeat(ApiSeats.ActionStart)] Guid tenantId)
        {
            List<Resource> resources= await _tenantResourceRepository
                    .Include(x => x.Resource)
                    .Where(x => x.TenantId.Equals(tenantId))
                    .Select(x => x.Resource)
                    .OrderBy(x => x.Order)
                    .ToListAsync();
            return resources.Select(x => x.Adapt<ResourceDto>());
        }
    }
}
