// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using System.Security.Claims;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// 身份转换器
    /// </summary>
    /// <remarks>
    /// 通过转换器将自定义的身份数据转换为系统中的身份数据
    /// </remarks>
    public interface IIdentityConverter
    {
        /// <summary>
        /// 从请求主体信息解析出身份信息
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        Identity? ClaimsPrincipalToIdentity(ClaimsPrincipal principal);
        /// <summary>
        /// identity生成ClaimsIdentity
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        ClaimsIdentity IdentityToClaimsIdentity(Identity identity, JwtTokenType tokenType);
    }
}
