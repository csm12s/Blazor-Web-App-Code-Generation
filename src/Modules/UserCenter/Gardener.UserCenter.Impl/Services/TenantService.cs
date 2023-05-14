// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Entity.Domains;
using Gardener.EntityFramwork;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class TenantService : ServiceBase<SystemTenant, SystemTenantDto, Guid>, ITenantService
    {
        /// <summary>
        /// 租户服务
        /// </summary>
        /// <param name="repository"></param>
        public TenantService(Furion.DatabaseAccessor.IRepository<SystemTenant> repository) : base(repository)
        {
        }
    }
}
