// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Base.Enums;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Services
{
    /// <summary>
    /// 用户账户认证授权服务
    /// </summary>
    public interface IAccountService
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
        /// 获取用户资源的key
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        Task<List<string>> GetCurrentUserResourceKeys(params ResourceType[] resourceTypes);
        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <param name="rootKey"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetCurrentUserMenus(string? rootKey = null);
        /// <summary>
        /// 移除当前用户token
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveCurrentUserRefreshToken();
    }
}
