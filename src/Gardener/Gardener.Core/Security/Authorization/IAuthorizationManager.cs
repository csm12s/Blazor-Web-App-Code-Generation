﻿using Gardener.Core.Security.Authentication;
using System.Collections.Generic;

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
        SecurityTokenResult Signin<TUserId>(TUserId userId, Dictionary<string, object> claims);
        /// <summary>
        /// 获取用户id key 的名称
        /// </summary>
        /// <returns></returns>
        string GetUserIdKeyName()=>"user_id";
        /// 获取用户 Id
        /// </summary>
        /// <returns></returns>
        string GetUserId();
        /// <summary>
        /// 获取用户 Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUserId<T>();
        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        bool CheckSecurity(string resourceId);
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        bool IsSuperAdministrator();
    }
}