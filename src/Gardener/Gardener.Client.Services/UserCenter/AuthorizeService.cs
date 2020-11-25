// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
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
        private IApiCaller apiCaller;

        public AuthorizeService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            return await apiCaller.GetAsync<UserDto>("authorize/current-user");
        }

        public List<ApiResult<ResourceDto>> GetCurrentUserResources()
        {
            throw new NotImplementedException();
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
            var result = await apiCaller.PostAsync<LoginInput, LoginOutput>("authorize/login", input);
            return result;
        }

        public async Task<ApiResult<TokenOutput>> RefreshToken()
        {
            return await apiCaller.PostAsync<object, TokenOutput>("authorize/refresh-token");
        }
    }
}
