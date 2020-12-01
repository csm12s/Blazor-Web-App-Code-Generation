using Gardener.Application.Dtos;
using Gardener.Client.Models;
using Gardener.Enums;
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
        /// <summary>
        /// 获取指定类型的资源
        /// </summary>
        /// <param name="resourceTypes"></param>
        /// <returns></returns>
        Task<ApiResult<List<ResourceDto>>> GetCurrentUserResources(params ResourceType[] resourceTypes);
        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ResourceDto>>> GetCurrentUserMenus();
    }
}