// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Entites;
using Gardener.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Core
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public interface IAuthorizationManager
    {
        /// <summary>
        /// 获取用户 Id
        /// </summary>
        /// <returns></returns>
        int GetUserId();
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        bool IsSuperAdministrator();
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        User GetUser();
        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        Task<Function> GetContenxtFunction();
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        Task<bool> ChecktContenxtFunction();
    }
}