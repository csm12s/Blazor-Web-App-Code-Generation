// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Dto;
using Gardener.Base.Entity;
using Gardener.EntityFramwork;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 租户资源服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class TenantResourceService : ServiceBaseNoKey<SystemTenantResource, SystemTenantResourceDto>, ITenantResourceService
    {
        /// <summary>
        /// 租户资源服务
        /// </summary>
        /// <param name="repository"></param>
        public TenantResourceService(IRepository<SystemTenantResource, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
}
