// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Base;
using Gardener.Client.Base;
using Gardener.Common;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;

namespace Gardener.UserCenter.Client.Services
{
    [ScopedService]
    public class UserService : ClientServiceBase<UserDto>,IUserService
    {
        public UserService(IApiCaller apiCaller):base(apiCaller, "user")
        {
        }

        public Task<List<ResourceDto>> GetResources(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleDto>> GetRoles(int userId)
        {
            return await apiCaller.GetAsync<List<RoleDto>>($"{controller}/{userId}/roles");
        }
       
        public async Task<PagedList<UserDto>> Search(int[] deptIds, int pageIndex = 1, int pageSize = 10)
        {
            List<KeyValuePair<string, object?>> pramas = deptIds.ConvertToQueryParameters("deptIds");

            return await apiCaller.GetAsync<PagedList<UserDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

        public async Task<bool> Role(int userId, int[] roleIds)
        {
            return await apiCaller.PostAsync<int[],bool>($"{controller}/{userId}/role", roleIds);
        }

        public async Task<bool> UpdateAvatar(UserUpdateAvatarInput input)
        {
            return await apiCaller.PutAsync<UserUpdateAvatarInput, bool>($"{controller}/avatar", input);
        }

        public async Task<string> GetCurrentUserId()
        {
            return await apiCaller.GetAsync<string>($"{controller}/current-user-id");
        }

        public Task<List<UserDto>> GetUsers(IEnumerable<int> userIds)
        {
            List<KeyValuePair<string, object?>> values = new List<KeyValuePair<string, object?>>();
            foreach (int userId in userIds)
            {
                values.Add(new KeyValuePair<string, object?>("userIds", userId));
            }
            return apiCaller.GetAsync<List<UserDto>>($"{controller}/users", values);
        }
    }
}
