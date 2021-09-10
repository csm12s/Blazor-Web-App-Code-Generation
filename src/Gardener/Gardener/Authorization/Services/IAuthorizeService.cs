// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Authorization.Services
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取当前用户的角色
        /// </summary>
        /// <returns></returns>
        Task<List<RoleDto>> GetCurrentUserRoles();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TokenOutput> Login(LoginInput input);
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        Task<TokenOutput> RefreshToken(RefreshTokenInput input);
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserDto> GetCurrentUser();
        /// <summary>
        /// 获取指定类型的资源
        /// </summary>
        /// <param name="resourceTypes"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType[] resourceTypes);
        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <returns></returns>
        Task<List<ResourceDto>> GetCurrentUserMenus();
        /// <summary>
        /// 移除当前用户token
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveCurrentUserRefreshToken();

    }
}