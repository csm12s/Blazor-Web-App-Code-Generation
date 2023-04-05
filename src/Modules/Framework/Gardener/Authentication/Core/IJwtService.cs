// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// jwt
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<JsonWebToken> CreateToken(Identity identity);
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="orlRefreshToken"></param>
        /// <returns></returns>
        Task<JsonWebToken> RefreshToken(string orlRefreshToken);
        /// <summary>
        /// 移除当前用户的刷新token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<bool> RemoveRefreshToken(Identity identity);

        /// <summary>
        /// 从请求主体信息解析出身份信息
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        Identity? ClaimsPrincipalToIdentity(ClaimsPrincipal principal);

    }
}