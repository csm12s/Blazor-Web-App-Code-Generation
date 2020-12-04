// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeService : IAuthorizeService
    {
        private readonly static string controller = "authorize";
        private IApiCaller apiCaller;

        public AuthorizeService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            return await apiCaller.GetAsync<UserDto>($"{controller}/current-user");
        }

        public async Task<ApiResult<List<ResourceDto>>> GetCurrentUserMenus()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-menus");
        }

        public List<ApiResult<ResourceDto>> GetCurrentUserResources()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<List<ResourceDto>>> GetCurrentUserResources(params ResourceType [] resourceTypes)
        {
            return await apiCaller.PostAsync< ResourceType[],List <ResourceDto>>($"{controller}/current-user-resources", resourceTypes ?? new ResourceType[] { });
        }

        public List<ApiResult<RoleDto>> GetCurrentUserRoles()
        {
            throw new NotImplementedException();
        }

        public bool InitResource()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<LoginOutput>> Login(LoginInput input)
        {
            var result = await apiCaller.PostAsync<LoginInput, LoginOutput>($"{controller}/login", input);
            return result;
        }

        public async Task<ApiResult<TokenOutput>> RefreshToken()
        {
            return await apiCaller.PostAsync<object, TokenOutput>($"{controller}/refresh-token");
        }
    }
}
