// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Core.Dtos;
using Gardener.Client.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace Gardener.Client.Apis
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeService : IAuthorizeService
    {
        private HttpClient httpClient;

        public AuthorizeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            //var request = new RestRequest("authorize/current-user");
            //return await httpClient.GetAsync<ApiResult<UserDto>>(request);
            return await httpClient.GetFromJsonAsync<ApiResult<UserDto>>("authorize/current-user");
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

        public async Task<ApiResult<LoginOutput>> Login(LoginInput input)
        {
            //var request = new JsonRequest<LoginInput, ApiResult<LoginOutput>>("authorize/login", input);
            //return httpClient.Post<LoginInput, ApiResult<LoginOutput>>(request).Data;

            return await httpClient.PostFromJsonAsync<LoginInput, ApiResult<LoginOutput>>("authorize/login", input);
        }
        

       
    }
}
