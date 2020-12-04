// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class RoleService : ServiceBase<RoleDto>,IRoleService
    {
        private readonly static string controller = "role";

        private IApiCaller apiCaller;

        public RoleService(IApiCaller apiCaller):base(apiCaller,controller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<ApiResult<bool>> DeleteResource(int roleId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{roleId}/resource");
        }

        public async Task<ApiResult<List<RoleDto>>> GetEffective()
        {
            return await apiCaller.GetAsync<List<RoleDto>>($"{controller}/effective");
        }

        public async Task<ApiResult<List<ResourceDto>>> GetResource(int roleId)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/{roleId}/resource");
        }

        public async Task<ApiResult<bool>> Resource(int roleId, Guid[] resourceIds)
        {
            return await apiCaller.PostAsync<Guid[], bool>($"{controller}/{roleId}/resource", resourceIds);
        }
        public async Task<ApiResult<PagedList<RoleDto>>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>() 
            {
                {"name",name }
            };
            return  await apiCaller.GetAsync<PagedList<RoleDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

    }
}
