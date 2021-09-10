// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// jwt
    /// </summary>
    public interface IJwtBearerService
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

    }
}