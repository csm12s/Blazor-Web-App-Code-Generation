﻿// -----------------------------------------------------------------------------
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
using System.Text;
using Gardener.SystemManager.Dtos;
using Gardener.Base.Domains;
using Gardener.Base.Enums;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 资源服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class ResourceService : ServiceBase<Resource, ResourceDto, Guid>, IResourceService
    {
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<ResourceFunction> _resourceFunctionRespository;
        /// <summary>
        /// 资源服务
        /// </summary>
        /// <param name="resourceRepository"></param>
        /// <param name="resourceFunctionRespository"></param>
        public ResourceService(IRepository<Resource> resourceRepository, IRepository<ResourceFunction> resourceFunctionRespository) : base(resourceRepository)
        {
            _resourceRepository = resourceRepository;
            _resourceFunctionRespository = resourceFunctionRespository;
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
        /// </remarks>
        /// <param name="rootKey"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetTree([FromQuery]string rootKey=null)
        {

            List<ResourceDto> resourceDtos = new List<ResourceDto>();

            var allResources =await _resourceRepository
                .Where(x => x.IsDeleted == false && x.IsLocked==false)
                .Where(!string.IsNullOrEmpty(rootKey),x=>x.Key.Equals(rootKey))
                .OrderBy(x => x.Order)
                .ToListAsync();

            return allResources.Where(x => x.Type.Equals(ResourceType.Root)).Select(x => x.Adapt<ResourceDto>()).ToList();
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

       
    }
}