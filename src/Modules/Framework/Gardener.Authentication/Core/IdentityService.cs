// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// 身份服务
    /// 每次请求都是新的对象
    /// </summary>
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// jwt工具
        /// </summary>
        private readonly IJwtService _jwtService;
        /// <summary>
        /// 当前请求的身份信息
        /// </summary>
        private Identity? _identity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="jwtService"></param>
        public IdentityService(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
        }
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        public Identity? GetIdentity()
        {
            if (_identity != null)
            {
                return _identity;
            }
            _identity = GetIdentityFromContext();
            return _identity;
        }


        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        private Identity? GetIdentityFromContext()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                //非http请求
                return null;
            }
            if (httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }
            //违法使用
            string? tokenTypeKey = httpContext.User.FindFirstValue(AuthKeyConstants.TokenTypeKey);
            if (string.IsNullOrEmpty(tokenTypeKey) || JwtTokenType.RefreshToken.ToString().Equals(tokenTypeKey))
            {
                throw Oops.Oh(ExceptionCode.REFRESHTOKEN_CANNOT_USED_IN_AUTHENTICATION);
            }
            return _jwtService.ClaimsPrincipalToIdentity(httpContext.User);
        }
    }
}
