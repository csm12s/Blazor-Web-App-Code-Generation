// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Gardener.Core.Entites;
using Gardener.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gardener.Core
{
    /// <summary>
    /// 当前请求的权限管理 
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
        /// 功能仓储
        /// </summary>
        private readonly IRepository<Function> _functionRepository;

        /// <summary>
        /// 当前登录用户
        /// </summary>
        private readonly User _user;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="functionRepository"></param>
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor,
            IRepository<User> userRepository, 
            IRepository<Function> functionRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                //当前请求的用户
                _user = FindUser();
            }
            _functionRepository = functionRepository;
        }
        private User FindUser() 
        {
           return _userRepository.AsQueryable(false)
                    .Include(x => x.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false))
                    .Where(x => x.IsDeleted == false && x.IsLocked == false && x.Id == GetUserId())
                    .FirstOrDefault();
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
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public User GetUser() 
        {
            return this._user;
        }
        
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsSuperAdministrator()
        {
            //用户不存在
            if (_user == null) return false;
            //超级管理员
            return _user.Roles.Any(x => x.IsSuperAdministrator == true);
        }
        
        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        public async Task<Function> GetContenxtFunction()
        {
            string functionKey = GetContextFunctionKey();
            if (!string.IsNullOrEmpty(functionKey)) return await _functionRepository.FirstOrDefaultAsync(x=>x.Id.Equals(Guid.Parse(functionKey)));
            var (method, path) = GetContextEndpoint();
            return await _functionRepository.FirstOrDefaultAsync(x=>x.Method.Equals(method) && x.Path.Equals(path));
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ChecktContenxtFunction()
        {
            //超级管理员
            if (IsSuperAdministrator()) return true;
            string functionKey = GetContextFunctionKey();
            // 查询用户拥有的权限
            if (!string.IsNullOrEmpty(functionKey) && !await CurrentUserHaveResource(functionKey)) return false;
            var (method, path) = GetContextEndpoint();
            // 查询用户拥有的权限
            if (!await CurrentUserHaveResource(method, path)) return false;
            return true;
        }
        
        #region private
        /// <summary>
        /// 获取功能Key
        /// </summary>
        /// <returns></returns>
        private string GetContextFunctionKey()
        {
            // 获取权限特性
            var securityDefineAttribute = _httpContextAccessor.HttpContext.GetMetadata<SecurityDefineAttribute>();
            if (securityDefineAttribute != null) return securityDefineAttribute.ResourceId;
            return null;
        }
        /// <summary>
        /// 获取节点信息
        /// </summary>
        /// <returns></returns>
        private (HttpMethodType, string) GetContextEndpoint()
        {
            //没有特性的可以通过路由+请求方法查找
            HttpMethodType method = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), _httpContextAccessor.HttpContext.Request.Method.ToUpper());
            string path = ((Microsoft.AspNetCore.Routing.RouteEndpoint)_httpContextAccessor.HttpContext.GetEndpoint()).RoutePattern.RawText;
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            return (method, path);
        }
        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<bool> CurrentUserHaveResource(HttpMethodType method, string path)
        {
            return await _userRepository.AsQueryable(false)
                    .Include(u => u.Roles)
                        .ThenInclude(u => u.Resources)
                            .ThenInclude(u=>u.Functions)
                    .Where(u => u.Id == GetUserId() && u.IsDeleted == false && u.IsLocked == false)
                    .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                        .SelectMany(u => u.Resources.Where(x => x.IsDeleted == false && x.IsLocked == false)
                                .SelectMany(u=>u.Functions.Where(x=>x.IsDeleted==false && x.IsLocked==false && x.Method.Equals(method) && x.Path.Equals(path)))
                            )
                        ).AnyAsync();
        }

        /// <summary>
        /// 判断是否拥有该权限
        /// </summary>
        /// <param name="functionKey"></param>
        /// <returns></returns>
        private async Task<bool> CurrentUserHaveResource(string functionKey)
        {
            return await _userRepository.AsQueryable(false)
                    .Include(u => u.Roles)
                        .ThenInclude(u => u.Resources)
                            .ThenInclude(u => u.Functions)
                    .Where(u => u.Id == GetUserId() && u.IsDeleted == false && u.IsLocked == false)
                    .SelectMany(u => u.Roles.Where(x => x.IsDeleted == false && x.IsLocked == false)
                        .SelectMany(u => u.Resources.Where(x => x.IsDeleted == false && x.IsLocked == false)
                                .SelectMany(u => u.Functions.Where(x => x.IsDeleted == false && x.IsLocked == false && x.Key.Equals(functionKey)))
                            )
                        ).AnyAsync();
        }
        #endregion
    }
}