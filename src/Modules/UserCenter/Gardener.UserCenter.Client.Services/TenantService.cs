// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Client.Base;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Services;

namespace Gardener.UserCenter.Client.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [ScopedService]
    public class TenantService : ClientServiceBase<SystemTenantDto, Guid>, ITenantService
    {
        public TenantService(IApiCaller apiCaller) : base(apiCaller, "tenant")
        {
        }
        /// <summary>
        /// 为租户绑定资源
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        public Task<bool> AddResources(Guid tenantId, Guid[] resourceIds)
        {
            return apiCaller.PostAsync<Guid[], bool>($"{controller}/{tenantId}/resources", resourceIds);
        }
        /// <summary>
        /// 获取租户下资源
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ResourceDto>> GetResources(Guid tenantId)
        {
            return apiCaller.GetAsync<IEnumerable<ResourceDto>>($"{controller}/{tenantId}/resources");
        }
    }
}
