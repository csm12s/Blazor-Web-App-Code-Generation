// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using System.Threading.Tasks;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// 登录token存储服务
    /// </summary>
    public interface ILoginTokenStorageService
    {
        /// <summary>
        /// 保存token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<bool> Save(JsonWebToken token, Identity identity);
        /// <summary>
        /// 更新现有token值和过期时间
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> Update(LoginTokenDto token);
        /// <summary>
        /// 登出token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<bool> LogOut(Identity identity);
        /// <summary>
        /// 验证是否可用
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<bool> Verify(Identity identity);
        /// <summary>
        /// 获取可用token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<LoginTokenDto?> GetAvailableToken(Identity identity);
    }
}
