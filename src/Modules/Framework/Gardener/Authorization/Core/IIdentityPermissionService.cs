﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Authorization.Dtos;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 身份权限服务
    /// </summary>
    public interface IIdentityPermissionService
    {

        /// <summary>
        /// 检测是否有该资源的使用权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        Task<bool> Check(Identity? identity, string resourceKey);

        /// <summary>
        /// 检测是否有该功能点的使用权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        Task<bool> Check(Identity? identity, ApiEndpoint? api);

        /// <summary>
        /// 获取身份的编号
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        object GetIdentityId(Identity identity);

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para> 用户身份：分配了至少一个超级管理角色就认为是超级管理</para>
        /// <para>其他身份：暂不支持</para>
        /// </remarks>
        Task<bool> IsSuperAdministrator(Identity? identity);
    }
}
