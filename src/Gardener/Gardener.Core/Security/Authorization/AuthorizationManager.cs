using Furion.Authorization;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Gardener.Core.Security
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager, IScoped
    {
        /// <summary>
        /// JWT配置
        /// </summary>
        private JWTSettingsOptions jwtSettings;
        private string userIdKeyName= "UserId";
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
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor
            , IRepository<User> userRepository, IOptions<JWTSettingsOptions> jWTSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            this.jwtSettings = jWTSettings.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUserIdKeyName()
        {
            return userIdKeyName;
        }
        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(userIdKeyName));
        }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsSuperAdministrator()
        {
            var userId = GetUserId();
            var user = _userRepository.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);
            //用户不存在
            if (user == null) return false;
            //超级管理员
            if (user.Roles.Any(x => x.Id == 1)) return true;
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TUserId"></typeparam>
        /// <param name="userId"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public SecurityTokenResult CreateToken<TUserId>(TUserId userId, Dictionary<string, object> claims)
        {
            var datetimeOffset = DateTimeOffset.UtcNow;
            var exp= DateTimeOffset.UtcNow.AddMinutes(jwtSettings.ExpiredTime.Value).ToUnixTimeSeconds();
            claims.TryAdd(userIdKeyName, userId);
            claims.TryAdd(JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds());
            claims.TryAdd(JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds());
            claims.TryAdd(JwtRegisteredClaimNames.Exp, exp);
            claims.TryAdd(JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer);
            claims.TryAdd(JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience);
            var token= JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey, claims);
            return new SecurityTokenResult {
                ExpiresIn= exp,
                AccessToken=token,
                TokenType="jwt"
            };
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public bool CheckSecurity(string resourceId)
        {
            var userId = GetUserId();
            //超级管理员
            if (IsSuperAdministrator()) return true;
            // ========= 以下代码应该缓存起来 ===========
            // 查询用户拥有的权限
            var resources = _userRepository
                .Include(u => u.Roles, false)
                    .ThenInclude(u => u.Resources)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles
                    .SelectMany(u => u.Resources))
                .Select(u => u.ResourceId);
            if (!resources.Contains(resourceId)) return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaims()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }
    }
}