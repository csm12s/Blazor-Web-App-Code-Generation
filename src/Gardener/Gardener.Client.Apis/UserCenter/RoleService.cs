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

namespace Gardener.Client.Apis
{
    public class RoleService : IRoleService, IServiceBase<RoleDto>
    {

        private HttpClient HttpClient;

        public RoleService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
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

        public Task<RoleDto> Insert(RoleDto input)
        {
            throw new NotImplementedException();
        }

        public void Resource(int roleId, int[] resourceIds)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<RoleDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task Update(RoleDto input)
        {
            throw new NotImplementedException();
        }
    }
}
