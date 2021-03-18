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
    public class ResourceService : LockExtendServiceBase<Resource, ResourceDto,Guid>, IResourceService
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
        /// <remarks>
        /// 返回根节点资源
        /// </remarks>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetRoot()
        {
            var resources = await resourceRepository
                .Where(x => x.ParentId ==null && x.Type.Equals(ResourceType.Root))
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
            List<Resource> resources = await resourceRepository.AsQueryable(false).Where(x=>x.IsDeleted==false).ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var resource in resources)
            {
                sb.Append($"new {nameof(Resource)}()");
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
            return sb.ToString();
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

            var allResources=resourceRepository
                .Where(x => x.IsDeleted == false)
                .OrderBy(x=>x.Order)
                .ToList();

            //var rootResources =allResources
            //    .Where(x =>x.ParentId == null && x.Type.Equals(ResourceType.ROOT))
            //    .OrderBy(x => x.Order).ToList();

            //resourceDtos.AddRange(rootResources);


            //var otherResources= allResources
            //     .Where(x => x.ParentId != null && !x.Type.Equals(ResourceType.ROOT))
            //     .OrderBy(x => x.Order).ToList();

            //foreach (var root in resourceDtos)
            //{
            //  SetChildren(root, otherResources);
            //}

            return allResources.Where(x=>x.Type.Equals(ResourceType.Root)).Select(x => x.Adapt<ResourceDto>()).ToList();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="resource"></param>
        ///// <param name="resources"></param>
        ///// <returns></returns>
        //private async Task SetChildren(ResourceDto resource, List<ResourceDto> resources)
        //{

        //    if (resources.Any(x => x.ParentId == resource.Id))
        //    {
        //        resource.Children =resources.Where(x => x.ParentId == resource.Id).OrderBy(x=>x.Order).ToList();

        //        foreach (var r in resource.Children)
        //        {
        //          SetChildren(r, resources);
        //        }
        //    }
        //}

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
            if (resourceRepository.Any(x => x.Key.Equals(resourceDto.Key), false))
            {
                throw Oops.Oh(ExceptionCode.RESOURCE_KEY_REPEAT);
            }
            return await base.Insert(resourceDto);
        }
    }
}
