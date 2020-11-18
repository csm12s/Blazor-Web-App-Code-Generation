// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos;
using Gardener.Client.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task Delete(int id)
        {
            await apiCaller.DeleteAsync(controller, "/{id}", new Dictionary<string, object>() { { "id",id } });
        }

        public void DeleteResource(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<RoleDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<RoleDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<RoleDto> Insert(RoleDto input)
        {
            input.CreatedTime = DateTimeOffset.Now;
           return await apiCaller.PostFromJsonAsyncAsync<RoleDto, RoleDto>(controller, request: input);
        }

        public void Resource(int roleId, int[] resourceIds)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<RoleDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>() 
            {
                {"name",name },
                {"pageIndex",pageIndex.ToString() },
                {"pageSize",pageSize.ToString() },
            };
            return  await apiCaller.GetFromJsonAsync<PagedList<RoleDto>>(controller, "search", pramas);
        }
        public async Task Update(RoleDto input)
        {
            input.UpdatedTime = DateTimeOffset.Now;
            await apiCaller.PutFromJsonAsyncAsync<RoleDto>(controller, request: input);
        }
    }
}
