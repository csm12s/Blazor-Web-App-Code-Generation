// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceFunctionService : IResourceFunctionService
    {
        private readonly static string controller = "resource-function";
        private readonly IApiCaller apiCaller;
        public ResourceFunctionService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<bool> Add(List<ResourceFunctionDto> resourceFunctionDtos)
        {
            return await apiCaller.PostAsync<List<ResourceFunctionDto>,bool>($"{controller}", resourceFunctionDtos);
        }

        public async Task<bool> Delete(Guid resourceId, Guid functionId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{resourceId}/{functionId}");
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }
    }
}
