// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System.Threading.Tasks;

namespace Gardener.Core
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public interface IAuthorizationManager
    {
        /// 获取用户 Id
        /// </summary>
        /// <returns></returns>
        int GetUserId();
        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        Task<bool> CheckSecurity(string resourceId);
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<bool> CheckSecurity(HttpMethodType method, string path);
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        Task<bool> IsSuperAdministrator();
    }
}