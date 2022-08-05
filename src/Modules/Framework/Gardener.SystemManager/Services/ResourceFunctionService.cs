﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Gardener.SystemManager.Dtos;
using Gardener.Base.Domains;

namespace Gardener.SystemManager.Services
{

    /// <summary>
    /// 资源与接口关系服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class ResourceFunctionService :  IResourceFunctionService, IDynamicApiController
    {
        private readonly IRepository<ResourceFunction> _resourceFunctionRespository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFunctionRespository"></param>
        public ResourceFunctionService(IRepository<ResourceFunction> resourceFunctionRespository)
        {
            _resourceFunctionRespository = resourceFunctionRespository;
        }

        /// <summary>
        /// 添加资源与接口关系
        /// </summary>
        /// <param name="resourceFunctionDtos"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<ResourceFunctionDto> resourceFunctionDtos)
        {
            await _resourceFunctionRespository.InsertAsync(resourceFunctionDtos.Select(x => x.Adapt<ResourceFunction>()));
            return true;
        }

        /// <summary>
        /// 删除资源与接口关系
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public async Task<bool> Delete([FromRoute] Guid resourceId, [FromRoute] Guid functionId)
        {
            List<ResourceFunction> entitys = await _resourceFunctionRespository.Where(x => x.ResourceId.Equals(resourceId) && x.FunctionId.Equals(functionId)).ToListAsync();

            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }

            await _resourceFunctionRespository.DeleteAsync(entitys);

            return true;
        }
        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetSeedData()
        {
            List<ResourceFunction> list = await _resourceFunctionRespository.AsQueryable(false).ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append($"\r\n new {nameof(ResourceFunction)}()");
                sb.Append("{");
                sb.Append($"{nameof(ResourceFunction.ResourceId)} = Guid.Parse(\"{item.ResourceId}\"),");
                sb.Append($"{nameof(ResourceFunction.FunctionId)} = Guid.Parse(\"{item.FunctionId}\"),");
                sb.Append($"{nameof(ResourceFunction.CreatedTime)}= DateTimeOffset.FromUnixTimeSeconds({DateTimeOffset.Now.ToUnixTimeSeconds()})");
                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
