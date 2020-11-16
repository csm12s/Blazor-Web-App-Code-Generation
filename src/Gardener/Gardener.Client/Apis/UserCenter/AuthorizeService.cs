// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Apis
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeService : IAuthorizeService
    {
        private RestClient restClient;

        public AuthorizeService(RestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            var request = new RestRequest("authorize/current-user");
            return await restClient.GetAsync<ApiResult<UserDto>>(request);
        }

        public List<ResourceDto> GetCurrentUserResources()
        {
            throw new NotImplementedException();
        }

        public List<RoleDto> GetCurrentUserRoles()
        {
            throw new NotImplementedException();
        }

        public bool InitResource()
        {
            throw new NotImplementedException();
        }

        public ApiResult<LoginOutput> Login(LoginInput input)
        {
            var request = new JsonRequest<LoginInput, ApiResult<LoginOutput>>("authorize/login", input);
            return restClient.Post<LoginInput, ApiResult<LoginOutput>>(request).Data;
        }
        

       
    }
}
