﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EntityFramwork;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 租户服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class TenantService : ServiceBase<Tenant, TenantDto, Guid>, ITenantService
    {
        /// <summary>
        /// 租户服务
        /// </summary>
        /// <param name="repository"></param>
        public TenantService(Furion.DatabaseAccessor.IRepository<Tenant> repository) : base(repository)
        {
        }
    }
}
