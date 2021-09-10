// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    public interface IUserService:IApplicationServiceBase<UserDto, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetResources(int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<RoleDto>> GetRoles(int userId);
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> Role(int userId, int[] roleIds);

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> UpdateAvatar(UserUpdateAvatarInput input);
    }
}