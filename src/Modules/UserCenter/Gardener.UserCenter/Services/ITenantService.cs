// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Dto;
using Gardener.SystemManager.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    public interface ITenantService : IServiceBase<SystemTenantDto, Guid>
    {
        /// <summary>
        /// 为租户绑定资源
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        Task<bool> AddResources(Guid tenantId, Guid[] resourceIds);
        /// <summary>
        /// 获取租户下资源
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<ResourceDto>> GetResources(Guid tenantId);
    }
}
