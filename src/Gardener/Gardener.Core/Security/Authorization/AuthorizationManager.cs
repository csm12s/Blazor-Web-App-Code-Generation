using Furion.Authorization;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;

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
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor
            , IRepository<User> userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public object GetUserId()
        {

            var user=_httpContextAccessor.HttpContext.User;
            //return ReadToken().GetPayloadValue<object>("UserId");

            return 1;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUserId<T>()
        {
            var user = _httpContextAccessor.HttpContext.User;
            //return ReadToken().GetPayloadValue<T>("UserId");

            return default(T);
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

        ///// <summary>
        ///// 检查权限
        ///// </summary>
        ///// <param name="resourceId"></param>
        ///// <returns></returns>
        //public bool CheckSecurity(string resourceId)
        //{
        //    var userId = GetUserId<int>();
        //    //超级管理员
        //    if (IsSuperAdministrator()) return true;
        //    // ========= 以下代码应该缓存起来 ===========
        //    // 查询用户拥有的权限
        //    var resources = _userRepository
        //        .Include(u => u.Roles, false)
        //            .ThenInclude(u => u.Resources)
        //        .Where(u => u.Id == userId)
        //        .SelectMany(u => u.Roles
        //            .SelectMany(u => u.Resources))
        //        .Select(u => u.ResourceId);
        //    if (!resources.Contains(resourceId)) return false;
        //    return true;
        //}
        ///// <summary>
        ///// 解析 Token
        ///// </summary>
        ///// <returns></returns>
        //[IfException(1001, ErrorMessage = "非法操作")]
        //private JsonWebToken ReadToken()
        //{
        //    // 获取 token
        //    var accessToken = _httpContextAccessor.GetJwtToken() ?? throw Oops.Oh(1001);

        //    // 验证token
        //    var (IsValid, Token) = JWTExtensions.Validate(accessToken, _jwtSettings);
        //    if (!IsValid) throw Oops.Oh(1001);

        //    return Token;
        //}
    }
}