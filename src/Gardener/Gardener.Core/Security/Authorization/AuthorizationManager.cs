// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Authorization;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Gardener.Core.Entites;
using Gardener.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Gardener.Core
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager, IScoped
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IRepository<User> _userRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor, 
            IRepository<User> userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsSuperAdministrator()
        {
            var value = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.UserIsSuperAdministratorKey);
            if (!string.IsNullOrEmpty(value))
            {
                return bool.Parse(value);
            }
            var userId = GetUserId();
            var user = _userRepository
                .Include(x => x.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false && x.IsSuperAdministrator == true))
                .Where(x => x.IsDeleted == false && x.IsLocked == false && x.Id == userId)
                .FirstOrDefault();
            //用户不存在
            if (user == null) return false;
            //超级管理员
            bool isSuperAdministrator = user.Roles.Any();
            //添加一个超级管理员身份证
            _httpContextAccessor.HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>() { new Claim(AuthKeyConstants.UserIsSuperAdministratorKey, isSuperAdministrator.ToString()) }));
            return isSuperAdministrator;
        }
        
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public bool CheckSecurity(string resourceKey)
        {
            var userId = GetUserId();
            //超级管理员
            if (IsSuperAdministrator()) return true;
            // ========= 以下代码应该缓存起来 ===========
            // 查询用户拥有的权限
            var resources = _userRepository
                .Include(u => u.Roles, false)
                    .ThenInclude(u => u.Resources)
                .Where(u => u.Id == userId && u.IsDeleted==false && u.IsLocked==false)
                .SelectMany(u => u.Roles.Where(x=>x.IsDeleted==false && x.IsLocked==false)
                    .SelectMany(u => u.Resources.Where(x=>x.IsDeleted==false && x.IsLocked==false && x.Key.Equals(resourceKey))))
                .Select(u => u.Id);
            if (!resources.Any()) return false;
            return true;
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckSecurity(HttpMethodType method ,string path)
        {
            var userId = GetUserId();
            //超级管理员
            if (IsSuperAdministrator()) return true;
            // ========= 以下代码应该缓存起来 ===========
            // 查询用户拥有的权限
            var resources = _userRepository
                .Include(u => u.Roles, false)
                    .ThenInclude(u => u.Resources)
                .Where(u => u.Id == userId && u.IsDeleted == false && u.IsLocked == false)
                .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                    .SelectMany(u => u.Resources.Where(x => x.IsDeleted == false && x.IsLocked == false && x.Method.Equals(method) && x.Path.Equals(path))))
                .Select(u => u.Id);
            if (!resources.Any()) return false;
            return true;
        }
    }
}