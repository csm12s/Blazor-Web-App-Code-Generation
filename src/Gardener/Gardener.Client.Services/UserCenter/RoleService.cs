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
    public class RoleService : IRoleService
    {
        private readonly string controller = "role";

        private IApiCaller apiCaller;

        public RoleService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
           return await apiCaller.DeleteAsync<bool>($"{controller}/{id}");
        }

        public async Task<ApiResult<bool>> Deletes(int[] ids)
        {
            return await apiCaller.PostAsync< int[], bool>($"{controller}/deletes",ids);
        }

        public void DeleteResource(int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> FakeDelete(int id)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/fake-delete/{id}");
        }

        public async Task<ApiResult<bool>> FakeDeletes(int[] ids)
        {
            return await apiCaller.PostAsync<int[], bool>($"{controller}/fake-deletes", ids);
        }

        public Task<ApiResult<RoleDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<List<RoleDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<PagedList<RoleDto>>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<RoleDto>> Insert(RoleDto input)
        {
            input.CreatedTime = DateTimeOffset.Now;
           return await apiCaller.PostAsync<RoleDto, RoleDto>(controller, request: input);
        }

        public void Resource(int roleId, int[] resourceIds)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PagedList<RoleDto>>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>() 
            {
                {"name",name },
                {"pageIndex",pageIndex.ToString() },
                {"pageSize",pageSize.ToString() },
            };
            return  await apiCaller.GetAsync<PagedList<RoleDto>>($"{controller}/search", pramas);
        }
        public async Task<ApiResult<bool>> Update(RoleDto input)
        {
            input.UpdatedTime = DateTimeOffset.Now;
            return await apiCaller.PutAsync<RoleDto, bool>(controller, request: input);
        }
    }
}
