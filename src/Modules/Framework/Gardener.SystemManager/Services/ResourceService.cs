// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Gardener.Enums;
using Furion.FriendlyException;
using Gardener.SystemManager.Dtos;
using Gardener.Base.Enums;
using Gardener.EntityFramwork;
using Gardener.Base.Entity;
using Furion.DependencyInjection;
using Gardener.Base;
using Swashbuckle.AspNetCore.Annotations;
using Gardener.Authentication.Core;
using Humanizer.Localisation;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 资源服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class ResourceService : ServiceBase<Resource, ResourceDto, Guid>, IResourceService,
        ITransient
    {
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<SystemTenantResource> _tenantResourceRepository;
        private readonly IRepository<ResourceFunction> _resourceFunctionRespository;
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 资源服务
        /// </summary>
        /// <param name="resourceRepository"></param>
        /// <param name="resourceFunctionRespository"></param>
        /// <param name="tenantResourceRepository"></param>
        /// <param name="identityService"></param>
        public ResourceService(IRepository<Resource> resourceRepository, IRepository<ResourceFunction> resourceFunctionRespository, IRepository<SystemTenantResource> tenantResourceRepository, IIdentityService identityService) : base(resourceRepository)
        {
            _resourceRepository = resourceRepository;
            _resourceFunctionRespository = resourceFunctionRespository;
            _tenantResourceRepository = tenantResourceRepository;
            _identityService = identityService;
        }

        /// <summary>
        /// 获取所有子资源
        /// </summary>
        /// <remarks>
        /// 获取所有子资源
        /// </remarks>
        /// <param name="id">父级id</param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetChildren([ApiSeat(ApiSeats.ActionStart)] Guid id)
        {
            var resources = await _resourceRepository
                .Where(x => x.ParentId == id)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Order)
               .Select(x => x.Adapt<ResourceDto>()).ToListAsync();
            return resources;
        }


        /// <summary>
        /// 返回根节点
        /// </summary>
        /// <remarks>
        /// 返回根节点资源
        /// </remarks>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetRoot()
        {
            var resources = await _resourceRepository
                .Where(x => x.ParentId == null && x.Type.Equals(ResourceType.Root))
                .Where(x => x.IsDeleted == false)
                .Select(x => x.Adapt<ResourceDto>()).ToListAsync();
            return resources;
        }

        /// <summary>
        /// 查询所有资源
        /// </summary>
        /// <remarks>
        /// 查询所有资源 按树形结构返回
        /// <para>
        /// 非租户在所有资源中抽取，租户在自己的资源池中抽取
        /// </para>
        /// </remarks>
        /// <param name="includLocked">是否包含锁定的资源</param>
        /// <param name="rootKey"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetTree([FromQuery]bool includLocked=true,[FromQuery]string? rootKey = null, [FromQuery]Guid? tenantId=null)
        {

            List<Resource>? resources = null;
            IModelTenant? identity =_identityService.GetIdentity() as IModelTenant;
            if(tenantId!=null || (identity != null && identity.IsTenant))
            {
                //查询租户
                //租户只能查自己的
                tenantId = (identity != null && identity.IsTenant) ? identity.TenantId : tenantId;
                resources = await _tenantResourceRepository
                    .Include(x=>x.Resource)
                    .Where(tenantId!=null, x=>x.TenantId.Equals(tenantId))
                    .Select(x=>x.Resource)
                    .Where(!string.IsNullOrEmpty(rootKey), x => x.Key.Equals(rootKey))
                    .OrderBy(x => x.Order)
                    .ToListAsync();
            }
            else
            {
                //直接查询
                resources = await _resourceRepository
               .Where(x => x.IsDeleted == false && (includLocked || x.IsLocked == false))
               .Where(!string.IsNullOrEmpty(rootKey), x => x.Key.Equals(rootKey))
               .OrderBy(x => x.Order)
               .ToListAsync();
            }
            return resources.Where(x => x.Type.Equals(ResourceType.Root)).Select(x => x.Adapt<ResourceDto>()).ToList();
        }

        /// <summary>
        /// 添加资源
        /// </summary>
        /// <remarks>
        /// 添加资源
        /// </remarks>
        /// <param name="resourceDto"></param>
        /// <returns></returns>
        public override async Task<ResourceDto> Insert(ResourceDto resourceDto)
        {
            if (_resourceRepository.Any(x => x.Key.Equals(resourceDto.Key) && x.IsDeleted==false && x.IsLocked==false, false))
            {
                throw Oops.Oh(ExceptionCode.RESOURCE_KEY_REPEAT);
            }
            return await base.Insert(resourceDto);
        }
        /// <summary>
        /// 根据资源id获取功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<FunctionDto>> GetFunctions([ApiSeat(ApiSeats.ActionStart)] Guid id)
        {
            return await _resourceFunctionRespository.AsQueryable(false)
                 .Include(x => x.Function)
                 .Where(x => x.ResourceId.Equals(id))
                 .Select(x => x.Function)
                 .Where(x => x.IsDeleted == false && x.IsLocked == false)
                 .Select(x => x.Adapt<FunctionDto>())
                 .ToListAsync();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量删除")]
        public override async Task<bool> Deletes([FromBody] Guid[] ids)
        {
            foreach (Guid id in ids)
            {
                await _repository.DeleteNowAsync(id);

            }
            await EntityEventNotityUtil.NotifyDeletesAsync<ResourceDto, Guid>(ids);
            return true;
        }
    }
}
