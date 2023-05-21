// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    [Description("用户角色信息")]
    public class UserRole : GardenerTenantEntityBaseNoKey<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<UserRole, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<UserRole, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户编号")]
        public int UserId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        [DisplayName("用户")]
        public User? User { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [DisplayName("角色编号")]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        [DisplayName("角色")]
        public Role? Role { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<UserRole> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
        }
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<UserRole> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new UserRole{ UserId = 1, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 2, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 3, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 4, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 5, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 6, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 7, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 8, RoleId = 2, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User },
                new UserRole{ UserId = 10, RoleId = 3, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User,TenantId=Guid.Parse("710148B3-0C80-48A2-8F57-4B863BE9859F") },
                new UserRole{ UserId = 11, RoleId = 4, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User,TenantId=Guid.Parse("F416B514-04C8-40CA-91A4-07C5BBF9C8C6") },
                new UserRole{ UserId = 12, RoleId = 4, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311),CreateBy="1",CreateIdentityType=IdentityType.User,TenantId=Guid.Parse("F416B514-04C8-40CA-91A4-07C5BBF9C8C6") },
            };
        }
    }
}