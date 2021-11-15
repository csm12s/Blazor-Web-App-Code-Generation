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
    /// 
    /// </summary>
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Identity _identity;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        public Identity GetIdentity()
        {
            if (_identity != null)
            {
                return _identity;
            }
            _identity= GetIdentityFromContext();
            return _identity;
        }


        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        private Identity GetIdentityFromContext()
        {
            string tokenTypeKey = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.TokenTypeKey);
            if (JwtTokenType.RefreshToken.ToString().Equals(tokenTypeKey))
            {
                throw Oops.Oh(ExceptionCode.REFRESHTOKEN_CANNOT_USED_IN_AUTHENTICATION);
            }
            Identity identity = new Identity();
            identity.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            identity.Name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            identity.GivenName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            string identityType = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.IdentityType);
            identity.IdentityType = Enum.Parse<IdentityType>(identityType, true);
            identity.LoginId = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            string loginClientType = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientTypeKeyName);
            identity.LoginClientType = Enum.Parse<LoginClientType>(loginClientType, true);
            return identity;
        }
    }
}
