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
        /// <summary>
        /// 测试token是否可用
        /// </summary>
        /// <param name="flag">标记</param>
        /// <returns></returns>
        /// <remarks>
        /// 不执行任何内容，token无效将响应401
        /// </remarks>
        Task<bool> TestToken(string? flag=null);
        /// <summary>
        /// 更新当前用户基本信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// 更新 <see cref="UserDto.NickName"/>、<see cref="UserDto.Gender"/>、<see cref="UserDto.Avatar"/>
        /// </remarks>
        Task<bool> UpdateCurrentUserBaseInfo(UserDto user);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changePasswordInput"></param>
        /// <returns></returns>
        /// <remarks>
        /// 修改当前用户密码
        /// </remarks>
        Task<bool> ChangePassword(ChangePasswordInput changePasswordInput);
    }
}
