// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class ResourceService : ServiceBase<ResourceDto,Guid>,IResourceService
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
        public async Task<ApiResult<List<ResourceDto>>> GetChildren(Guid id)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/{id}/Children");
        }

        public async Task<ApiResult<List<ResourceDto>>> GetRoot()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/root");
        }

        public async Task<ApiResult<List<ResourceDto>>> GetTree()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/tree");
        }
    }
}
