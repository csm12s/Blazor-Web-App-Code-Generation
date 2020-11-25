// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using Gardener.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class UserService :  ServiceBase<UserDto>,IUserService
    {
        private readonly static string controller = "user";

        private readonly IApiCaller apiCaller;

        public UserService(IApiCaller apiCaller):base(apiCaller,controller)
        {
            this.apiCaller = apiCaller;
        }

        public Task<ApiResult<List<ResourceDto>>> GetResources(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<List<RoleDto>>> GetRoles(int userId)
        {
            throw new NotImplementedException();
        }
       
        public async Task<ApiResult<PagedList<UserDto>>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>()
            {
                {"name",name }
            };
            return await apiCaller.GetAsync<PagedList<UserDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }
    }
}
