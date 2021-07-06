// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Gardener.Enums;
using Furion.FriendlyException;
using System;
using Gardener.Application.Interfaces;
using System.Text;

namespace Gardener.Application
{
    /// <summary>
    /// 资源服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class ResourceService : ApplicationServiceBase<Resource, ResourceDto, Guid>, IResourceService
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
        /// 获取种子数据
        /// </summary>
        /// <remarks>
        /// 获取种子数据
        /// </remarks>
        /// <returns></returns>
        public async Task<string> GetSeedData()
        {
            List<Resource> resources = await _resourceRepository.AsQueryable(false).Where(x => x.IsDeleted == false).ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var resource in resources)
            {
                sb.Append($"\r\n new {nameof(Resource)}()");
                sb.Append("{");
                sb.Append($"{nameof(Resource.Id)}=Guid.Parse(\"{resource.Id}\"),");
                if (resource.ParentId != null && resource.ParentId != Guid.Empty)
                {
                    sb.Append($"{nameof(Resource.ParentId)}=Guid.Parse(\"{resource.ParentId}\"),");
                }
                sb.Append($"{nameof(Resource.Name)}=\"{resource.Name}\",");
                sb.Append($"{nameof(Resource.Icon)}=\"{resource.Icon}\",");
                sb.Append($"{nameof(Resource.Remark)}=\"{resource.Remark}\",");
                sb.Append($"{nameof(Resource.Key)}=\"{resource.Key}\",");
                sb.Append($"{nameof(Resource.Path)}=\"{resource.Path}\",");
                sb.Append($"{nameof(Resource.CreatedTime)}=DateTimeOffset.Now,");
                sb.Append($"{nameof(Resource.IsDeleted)}={resource.IsDeleted.ToString().ToLower()},");
                sb.Append($"{nameof(Resource.IsLocked)}={resource.IsLocked.ToString().ToLower()},");
                sb.Append($"{nameof(Resource.Type)}=(ResourceType){((int)resource.Type)},");
                sb.Append($"{nameof(Resource.Order)}={resource.Order}");
                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 查询所有资源
        /// </summary>
        /// <remarks>
        /// 查询所有资源 按树形结构返回
        /// </remarks>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetTree()
        {

            List<ResourceDto> resourceDtos = new List<ResourceDto>();

            var allResources =await _resourceRepository
                .Where(x => x.IsDeleted == false && x.IsLocked==false)
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
