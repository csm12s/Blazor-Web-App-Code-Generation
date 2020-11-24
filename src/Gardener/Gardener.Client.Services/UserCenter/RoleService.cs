// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos;
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

        public void DeleteResource(int roleId)
        {
            throw new NotImplementedException();
        }
        public void Resource(int roleId, int[] resourceIds)
        {
            throw new NotImplementedException();
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
