// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.UserCenter.Dtos;
using Gardener.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.SystemManager.Dtos;

namespace Gardener.UserCenter.Services
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService:IServiceBase<UserDto, int>
    {
        /// <summary>
        /// 获取当前用户编号
        /// </summary>
        /// <returns></returns>
        Task<string> GetCurrentUserId();

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

        /// <summary>
        /// 根据用户编号集合获取多个用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<UserDto>> GetUsers(IEnumerable<int> userIds);
    }
}