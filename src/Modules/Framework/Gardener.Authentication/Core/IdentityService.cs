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
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// 当前登录身份
        /// </summary>
        private readonly Identity _identity;
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            if (this._httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                //当前请求的用户
                this._identity = GetIdentityFromContext();

            }
        }

        public Identity GetIdentity()
        {
            return this._identity;
        }


        /// <summary>
        /// 
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
            identity.ClientId = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            string loginClientType = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientTypeKeyName);
            identity.LoginClientType = Enum.Parse<LoginClientType>(loginClientType, true);
            return identity;
        }
    }
}
