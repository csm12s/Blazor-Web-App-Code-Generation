// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Gardener.Enums;
using Furion.FriendlyException;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 资源服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class ResourceService : ServiceBase<Resource, ResourceDto>, IResourceService
    {
        private readonly IRepository<Resource> resourceRepository;
        /// <summary>
        /// 资源服务
        /// </summary>
        /// <param name="resourceRepository"></param>
        public ResourceService(IRepository<Resource> resourceRepository) : base(resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        //// <summary>
        /// 获取所有子资源
        /// </summary>
        /// <param name="id">父id</param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetChildren([ApiSeat(ApiSeats.ActionStart)] int id)
        {
            var resources = await resourceRepository
                .Where(x => x.ParentId == id)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Order)
               .Select(x=>x.Adapt<ResourceDto>()).ToListAsync();
            return resources;
        }


        /// <summary>
        /// 返回根节点
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetRoot()
        {
            var resources = await resourceRepository
                .Where(x => x.ParentId ==null && x.Type.Equals(ResourceType.ROOT))
                .Where(x => x.IsDeleted == false)
                .Select(x => x.Adapt<ResourceDto>()).ToListAsync();
            return resources;
        }
        /// <summary>
        /// 查询所有资源 按树形结构返回
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetTree()
        {

            List<ResourceDto> resourceDtos = new List<ResourceDto>();

            var allResources= await resourceRepository
                .AsQueryable(false)
                .Where(x => x.IsDeleted == false)
                .Select(x => x.Adapt<ResourceDto>()).ToListAsync();

            var rootResources =allResources
                .Where(x =>x.ParentId == null && x.Type.Equals(ResourceType.ROOT))
                .OrderBy(x => x.Order).ToList();

            resourceDtos.AddRange(rootResources);


            var otherResources= allResources
                 .Where(x => x.ParentId != null && !x.Type.Equals(ResourceType.ROOT))
                 .OrderBy(x => x.Order).ToList();

            foreach (var root in resourceDtos)
            {
              SetChildren(root, otherResources);
            }

            return resourceDtos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        private async Task SetChildren(ResourceDto resource, List<ResourceDto> resources)
        {

            if (resources.Any(x => x.ParentId == resource.Id))
            {
                resource.Children =resources.Where(x => x.ParentId == resource.Id).OrderBy(x=>x.Order).ToList();

                foreach (var r in resource.Children)
                {
                  SetChildren(r, resources);
                }
            }
        }
        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <returns></returns>
        public override async Task<ResourceDto> Insert(ResourceDto resourceDto)
        {
            if (resourceRepository.Any(x => x.Key.Equals(resourceDto.Key), false))
            {
                throw Oops.Oh(ExceptionCode.RESOURCE_KEY_REPEAT);
            }
            return await base.Insert(resourceDto);
        }
    }
}
