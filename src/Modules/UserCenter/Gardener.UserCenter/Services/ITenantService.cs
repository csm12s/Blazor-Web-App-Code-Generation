// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.UserCenter.Dtos;
using System;

namespace Gardener.UserCenter.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    public interface ITenantService : IServiceBase<SystemTenantDto, Guid>
    {
    }
}
