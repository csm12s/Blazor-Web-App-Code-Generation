// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;

namespace Gardener.SystemManager.Client.Services
{
    [ScopedService]
    public class ResourceService : ClientServiceBase<ResourceDto,Guid>,IResourceService
    {
        public ResourceService(IApiCaller apiCaller):base(apiCaller, "resource")
        {
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

        public async Task<List<ResourceDto>> GetTree(bool includLocked = true, string rootKey=null)
        {
            IDictionary<string, object> queryString=new Dictionary<string, object>();
            queryString.Add(nameof(rootKey), rootKey);
            queryString.Add(nameof(includLocked), includLocked);
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/tree", queryString);
        }
    }
}
