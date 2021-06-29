// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
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

        public async Task<UserDto> GetCurrentUser()
        {
            return await apiCaller.GetAsync<UserDto>($"{controller}/current-user");
        }

        public async Task<List<ResourceDto>> GetCurrentUserMenus()
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-menus");
        }

        public List<ResourceDto> GetCurrentUserResources()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType [] resourceTypes)
        {
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-resources");
        }

        public Task<List<RoleDto>> GetCurrentUserRoles()
        {
            throw new NotImplementedException();
        }

        public async Task<TokenOutput> Login(LoginInput input)
        {
            var result = await apiCaller.PostAsync<LoginInput, TokenOutput>($"{controller}/login", input);
            return result;
        }

        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return await apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{controller}/refresh-token", input);
        }

        public async Task<bool> RemoveCurrentUserRefreshToken()
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/current-user-refresh-token");
        }
    }
}
