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
        /// 
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        List<ResourceDto> GetCurrentUserResources(ResourceType resourceType);
    }
}