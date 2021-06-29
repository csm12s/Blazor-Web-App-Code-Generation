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
    public class ResourceService : ApplicationServiceBase<ResourceDto,Guid>,IResourceService
    {
        private readonly static string controller = "resource";
        private readonly IApiCaller apiCaller;

        public ResourceService(IApiCaller apiCaller):base(apiCaller,controller)
        {
            this.apiCaller = apiCaller;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ResourceDto>> GetChildren(Guid id)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/{id}/children");
        }

        public async Task<List<FunctionDto>> GetFunctions(Guid id)
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{controller}/{id}/functions");
        }

        public async Task<List<ResourceDto>> GetRoot()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/root");
        }

        public Task<string> GetSeedData()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResourceDto>> GetTree()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/tree");
        }
    }
}
