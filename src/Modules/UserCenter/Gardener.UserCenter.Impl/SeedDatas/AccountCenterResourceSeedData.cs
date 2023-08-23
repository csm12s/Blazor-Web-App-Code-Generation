// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AccountCenterResourceSeedData : IEntitySeedData<Resource>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Resource() {Name="个人中心",Key="account_center",Icon="user",Order=100,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),SupportMultiTenant=true,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-06-02 16:13:29"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-06-02 16:13:49"),UpdateBy="2",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="个人设置",Key="account_center_settings",Path="/account/settings",Order=0,ParentId=Guid.Parse("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"),SupportMultiTenant=true,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("74a75b21-3fcf-4c26-b998-aa4f0b658292"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-06-02 16:15:07"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-06-02 16:47:15"),UpdateBy="2",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="基本信息",Key="account_center_settings_base",Order=0,ParentId=Guid.Parse("74a75b21-3fcf-4c26-b998-aa4f0b658292"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("7a983726-92f2-4d47-9ee9-c15e279704d9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-21 17:32:58"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="账号绑定",Key="account_center_settings_binding",Order=0,ParentId=Guid.Parse("74a75b21-3fcf-4c26-b998-aa4f0b658292"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("9d549aeb-35fd-4345-849c-db85e42a103c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-21 17:33:46"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="安全设置",Key="account_center_settings_security",Order=0,ParentId=Guid.Parse("74a75b21-3fcf-4c26-b998-aa4f0b658292"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("98c63bf2-fbc3-46d6-94dd-9c2a939b7ba6"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-21 17:33:32"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
            };
        }
    }

}
