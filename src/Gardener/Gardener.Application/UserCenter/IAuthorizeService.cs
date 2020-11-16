using Gardener.Application.Dtos;
using Gardener.Core.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application
{
    public interface IAuthorizeService
    {
        List<ResourceDto> GetCurrentUserResources();
        List<RoleDto> GetCurrentUserRoles();
        bool InitResource();
        LoginOutput Login(LoginInput input);
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserDto> GetCurrentUser();
    }
}