// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Domain;
using Gardener.Enums;
using System.Threading.Tasks;

namespace Gardener.Authorization
{
    /// <summary>
    /// jwt
    /// </summary>
    public interface IJwtBearerService
    {
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientType"></param>
        /// <returns></returns>
        Task<JsonWebToken> CreateToken(int userId,LoginClientType clientType = LoginClientType.Browser);
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<JsonWebToken> RefreshToken(string refreshToken);
        /// <summary>
        /// 移除当前用户的刷新token
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveCurrentUserRefreshToken();

    }
}