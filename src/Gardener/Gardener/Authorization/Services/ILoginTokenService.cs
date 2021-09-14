// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Base;
using System;

namespace Gardener.Authorization.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    public interface ILoginTokenService : IApplicationServiceBase<LoginTokenDto, Guid>
    {
    }
}
