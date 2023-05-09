// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using System;
using System.Security.Claims;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// 身份转换器
    /// </summary>
    public class IdentityConverter : IIdentityConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Identity? ClaimsPrincipalToIdentity(ClaimsPrincipal principal)
        {
            Identity identity = new Identity();
            string? id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            string? loginId = principal.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            string? name = principal.FindFirstValue(ClaimTypes.Name);
            //无法解析
            if (id == null || loginId == null || name == null)
            {
                return null;
            }
            identity.Id = id;
            identity.LoginId = loginId;
            identity.Name = name;

            identity.NickName = principal.FindFirstValue(ClaimTypes.GivenName);
            string? loginClientType = principal.FindFirstValue(AuthKeyConstants.ClientTypeKeyName);
            string? identityType = principal.FindFirstValue(AuthKeyConstants.IdentityType);
            string? tenantId = principal.FindFirstValue(AuthKeyConstants.TenantId);
            string? customData = principal.FindFirstValue(AuthKeyConstants.CustomData);

            identity.IdentityType = identityType == null ? IdentityType.Unknown : Enum.Parse<IdentityType>(identityType, true);
            identity.LoginClientType = loginClientType == null ? LoginClientType.Unknown : Enum.Parse<LoginClientType>(loginClientType, true);
            identity.TenantId = string.IsNullOrEmpty(tenantId) ? null : Guid.Parse(tenantId);
            identity.CustomData = customData;
            return identity;
        }
        /// <summary>
        /// identity生成ClaimsIdentity
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public ClaimsIdentity IdentityToClaimsIdentity(Identity identity, JwtTokenType tokenType)
        {
            Claim[] claims =
                {
                new Claim(ClaimTypes.NameIdentifier, identity.Id),
                new Claim(ClaimTypes.GivenName, identity.NickName ?? identity.Name),
                new Claim(ClaimTypes.Name, identity.Name),
                new Claim(AuthKeyConstants.IdentityType, identity.IdentityType.ToString()),
                new Claim(AuthKeyConstants.ClientIdKeyName, identity.LoginId),
                new Claim(AuthKeyConstants.ClientTypeKeyName, identity.LoginClientType.ToString()),
                new Claim(AuthKeyConstants.TenantId, identity.TenantId?.ToString() ?? string.Empty),
                new Claim(AuthKeyConstants.CustomData, identity.CustomData ?? string.Empty),

                new Claim(AuthKeyConstants.TokenTypeKey, tokenType.ToString())
            };
            return new ClaimsIdentity(claims);
        }
    }
}
