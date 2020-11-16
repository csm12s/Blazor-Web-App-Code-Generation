using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Apis
{
    public interface IAuthorizeService
    {
        List<ResourceDto> GetCurrentUserResources();
        List<RoleDto> GetCurrentUserRoles();
        bool InitResource();
        ApiResult<LoginOutput> Login(LoginInput input);
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<UserDto>> GetCurrentUser();
    }
}