// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System.Collections.Generic;
using System.Security.Claims;

namespace Gardener.Core
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public interface IAuthorizationManager
    {
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <typeparam name="TUserId"></typeparam>
        /// <param name="userId"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        SecurityTokenResult CreateToken<TUserId>(TUserId userId, Dictionary<string, object> claims);
        /// <summary>
        /// 获取用户id key 的名称
        /// </summary>
        /// <returns></returns>
        string GetUserIdKeyName()=>"user_id";
        /// 获取用户 Id
        /// </summary>
        /// <returns></returns>
        int GetUserId();
        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        bool CheckSecurity(string resourceId);
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        bool CheckSecurity(HttpMethodType method, string path);
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        bool IsSuperAdministrator();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaims();
    }
}