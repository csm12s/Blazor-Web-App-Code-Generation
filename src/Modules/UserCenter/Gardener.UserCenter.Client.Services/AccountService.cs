// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.UserCenter.Services;
using Gardener.UserCenter.Dtos;
using Gardener.Authorization.Dtos;
using Gardener.Client.Base;
using Gardener.SystemManager.Dtos;
using Gardener.Base.Enums;

namespace Gardener.UserCenter.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class AccountService : IAccountService
    {
        private readonly static string controller = "account";
        private IApiCaller apiCaller;

        public AccountService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public Task<UserDto> GetCurrentUser()
        {
            return apiCaller.GetAsync<UserDto>($"{controller}/current-user");
        }

        public Task<List<ResourceDto>> GetCurrentUserMenus(string? rootKey = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add(nameof(rootKey), rootKey);
            return apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-menus", queryString);
        }

        public Task<List<string>> GetCurrentUserResourceKeys(params ResourceType[] resourceTypes)
        {
            List<KeyValuePair<string, object?>> paras = new List<KeyValuePair<string, object?>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object?>("resourceTypes", resourceTypes[i]));
            }
            return apiCaller.GetAsync<List<string>>($"{controller}/current-user-resource-keys", paras);
        }

        public Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType [] resourceTypes)
        {
            List<KeyValuePair<string, object?>> paras = new List<KeyValuePair<string, object?>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object?> ("resourceTypes" ,resourceTypes[i]));
            }
            return apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-resources", paras);
        }

        public Task<List<RoleDto>> GetCurrentUserRoles()
        {
            return apiCaller.GetAsync<List<RoleDto>>($"{controller}/current-user-roles");
        }

        public Task<TokenOutput> Login(LoginInput input)
        {
            var result = apiCaller.PostAsync<LoginInput, TokenOutput>($"{controller}/login", input);
            return result;
        }

        public Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{controller}/refresh-token", input);
        }

        public Task<bool> RemoveCurrentUserRefreshToken()
        {
            return apiCaller.DeleteAsync<bool>($"{controller}/current-user-refresh-token");
        }

        public Task<bool> TestToken(string? flag = null)
        {
            return apiCaller.PostAsync<int,bool>($"{controller}/test-token?flag={flag}",0);
        }
    }
}
