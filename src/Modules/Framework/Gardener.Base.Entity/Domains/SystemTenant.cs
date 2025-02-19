﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 租户
    /// </summary>
    public class SystemTenant : SystemTenantDto, IEntityBase, IEntitySeedData<SystemTenant>
    {
        /// <summary>
        /// 租户资源关系
        /// </summary>
        public List<SystemTenantResource> TenantResources { get; set; } = new List<SystemTenantResource>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<SystemTenant> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new SystemTenant { Id=Guid.Parse("710148B3-0C80-48A2-8F57-4B863BE9859F"),Name = "租户1", Email = "gardener@163.com", Tel = "400-888-8888", Remark = "预设数据。" ,IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)},
                new SystemTenant { Id=Guid.Parse("F416B514-04C8-40CA-91A4-07C5BBF9C8C6"),Name = "租户2", Email = "gardener@163.com", Tel = "400-888-8888", Remark = "预设数据。" ,IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)},
            };
        }
    }
}
