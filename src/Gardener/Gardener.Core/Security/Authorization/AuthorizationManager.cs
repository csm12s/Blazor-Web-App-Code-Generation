using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Core.Entites;
using Gardener.Core.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Gardener.Core
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
        private string userIdKeyName= "user_id";
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
        [IfException(1001, ErrorMessage = "非法操作")]
        public string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null || user.Identity?.IsAuthenticated != true) throw Oops.Oh(1001);
            //return ReadToken().GetPayloadValue<object>("UserId");
            return user.Claims.Where(x=>x.Type.Equals(GetUserIdKeyName())).FirstOrDefault().Value;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUserId<T>()
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(GetUserId());
        }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsSuperAdministrator()
        {
            var userId = GetUserId<int>();
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
        public SecurityTokenResult Signin<TUserId>(TUserId userId, Dictionary<string, object> claims)
        {
            var datetimeOffset = DateTimeOffset.UtcNow;
            claims.TryAdd(userIdKeyName, userId);
            claims.TryAdd(JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds());
            claims.TryAdd(JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds());
            claims.TryAdd(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddSeconds(jwtSettings.ExpiredTime.Value * 60).ToUnixTimeSeconds());
            claims.TryAdd(JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer);
            claims.TryAdd(JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience);
            return JWTHelper.BuildJwtToken(jwtSettings, claims);
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public bool CheckSecurity(string resourceId)
        {
            var userId = GetUserId<int>();
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
    }
}