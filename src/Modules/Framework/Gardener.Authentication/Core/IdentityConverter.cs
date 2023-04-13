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

            identity.IdentityType = identityType == null ? IdentityType.Unknown : Enum.Parse<IdentityType>(identityType, true);
            identity.LoginClientType = loginClientType == null ? LoginClientType.Unknown : Enum.Parse<LoginClientType>(loginClientType, true);

            return identity;
        }
    }
}
