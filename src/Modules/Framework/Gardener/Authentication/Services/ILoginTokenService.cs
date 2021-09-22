// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Authorization.Dtos;
using Gardener.Base;
using System;
using System.Threading.Tasks;

namespace Gardener.Authentication.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    public interface ILoginTokenService : IApplicationServiceBase<LoginTokenDto, Guid>
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<JsonWebToken> Login(Identity identity);
    }
}
