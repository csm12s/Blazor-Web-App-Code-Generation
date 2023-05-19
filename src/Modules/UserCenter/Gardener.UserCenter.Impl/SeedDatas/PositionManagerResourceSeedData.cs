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
    public class PositionManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {SupportMultiTenant=true,Name="岗位管理",Key="user_center_position",Remark="",Path="/user_center/position",Icon="crown",Order=5,ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),},
                new Resource() {SupportMultiTenant=true,Name="添加岗位",Key="user_center_position_add",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("0fd84267-ee22-47c4-b41c-ce654eba29d9"),},
                new Resource() {SupportMultiTenant=true,Name="删除岗位",Key="user_center_position_delete",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"),},
                new Resource() {SupportMultiTenant=true,Name="删除选中岗位",Key="user_center_position_delete_selected",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"),},
                new Resource() {SupportMultiTenant=true,Name="查看岗位",Key="user_center_position_detail",Remark="查看岗位",Path="",Icon="",Order=0,ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("ba89c7b7-552c-415c-b4be-085262dc76b0"),},
                new Resource() {SupportMultiTenant=true,Name="编辑岗位",Key="user_center_position_edit",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("25535592-81a1-42dd-8a55-509f2c852ff9"),},
                new Resource() {SupportMultiTenant=true,Name="锁定岗位",Key="user_center_position_lock",Remark="锁定岗位",Path="",Icon="",Order=0,ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("94d2c383-03b6-475c-a744-637dd87a5fdc"),},
                new Resource() {SupportMultiTenant=true,Name="刷新岗位",Key="user_center_position_refresh",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),},
         };
        }
    }
}
