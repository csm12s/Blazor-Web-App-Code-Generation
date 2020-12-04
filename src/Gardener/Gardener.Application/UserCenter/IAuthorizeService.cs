// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application
{
    public interface IAuthorizeService
    {
        List<RoleDto> GetCurrentUserRoles();
        LoginOutput Login(LoginInput input);
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserDto> GetCurrentUser();
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        TokenOutput RefreshToken();
        /// <summary>
        /// 获取指定类型的资源
        /// </summary>
        /// <param name="resourceTypes"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetCurrentUserResources(ResourceType [] resourceTypes);
        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <returns></returns>
        Task<List<ResourceDto>> GetCurrentUserMenus();
    }
}