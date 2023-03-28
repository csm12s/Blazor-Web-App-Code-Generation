﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Base;
using Gardener.Client.Base;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;

namespace Gardener.UserCenter.Client.Services
{
    [ScopedService]
    public class RoleService : ClientServiceBase<RoleDto>,IRoleService
    {
        public RoleService(IApiCaller apiCaller):base(apiCaller, "role")
        {
        }

        public async Task<bool> DeleteResource(int roleId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{roleId}/resource");
        }

        public async Task<List<ResourceDto>> GetResource(int roleId)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/{roleId}/resource");
        }

        public async Task<string> GetRoleResourceSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/role-resource-seed-data");
        }

        public async Task<bool> Resource(int roleId, Guid[] resourceIds)
        {
            return await apiCaller.PostAsync<Guid[], bool>($"{controller}/{roleId}/resource", resourceIds);
        }
        public async Task<PagedList<RoleDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object?> pramas = new Dictionary<string, object?>() 
            {
                {"name",name }
            };
            return  await apiCaller.GetAsync<PagedList<RoleDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

    }
}
