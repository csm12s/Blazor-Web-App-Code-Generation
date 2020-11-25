using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IAuthorizeService
    {
        List<ApiResult<ResourceDto>> GetCurrentUserResources();
        List<ApiResult<RoleDto>> GetCurrentUserRoles();
        bool InitResource();
        Task<ApiResult<LoginOutput>> Login(LoginInput input);
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<UserDto>> GetCurrentUser();
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<TokenOutput>> RefreshToken();
    }
}