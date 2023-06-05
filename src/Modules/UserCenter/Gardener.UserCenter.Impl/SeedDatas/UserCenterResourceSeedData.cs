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
    public class UserCenterResourceSeedData : IEntitySeedData<Resource>
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
                 new Resource() {SupportMultiTenant=true,Name="用户中心",Key="user_center",Remark="用户中心",Path="",Icon="apartment",Order=15,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2022-08-16 07:15:50"),Id=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),},
                 new Resource() {Name="个人中心",Key="account_center",Icon="user",Order=100,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),SupportMultiTenant=true,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-06-02 16:13:29"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-06-02 16:13:49"),UpdateBy="2",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                 new Resource() {Name="个人设置",Key="account_center_settings",Path="/account/settings",Order=0,ParentId=Guid.Parse("ba411ee1-f545-4bf6-8b56-18b8ed6f88fe"),SupportMultiTenant=true,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("74a75b21-3fcf-4c26-b998-aa4f0b658292"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-06-02 16:15:07"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-06-02 16:47:15"),UpdateBy="2",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
            };
        }
    }

}
