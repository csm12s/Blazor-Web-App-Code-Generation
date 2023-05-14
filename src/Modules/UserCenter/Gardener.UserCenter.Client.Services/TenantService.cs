// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
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
    }
}
