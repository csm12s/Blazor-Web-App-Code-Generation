using Gardener.Core.Dtos;
using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IAuthorizeService
    {
        List<ResourceDto> GetCurrentUserResources();
        List<RoleDto> GetCurrentUserRoles();
        bool InitResource();
        Task<ApiResult<LoginOutput>> Login(LoginInput input);
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<UserDto>> GetCurrentUser();
    }
}