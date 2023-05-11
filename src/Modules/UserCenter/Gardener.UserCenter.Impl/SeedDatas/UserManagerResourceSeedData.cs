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
    public class UserManagerResourceSeedData : IEntitySeedData<Resource>
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
               new Resource() {Name="用户管理",Key="user_center_user",Remark="用户管理",Path="/user_center/user",Icon="user",Order=10,ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="添加用户",Key="user_center_user_add",Remark="",Path="",Icon="",Order=2,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="删除用户",Key="user_center_user_delete",Remark="",Path="",Icon="",Order=1,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="删除选中用户",Key="user_center_user_delete_selected",Remark="删除选中",Path="",Icon="",Order=0,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="查看用户",Key="user_center_user_detail",Remark="查看用户",Path="",Icon="",Order=0,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="编辑用户",Key="user_center_user_edit",Remark="",Path="",Icon="",Order=4,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="导出用户",Key="user_center_user_export",Remark="导出用户",Order=0,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("a25da8f5-23d4-4118-b399-0a36f912a370"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-19 18:34:42"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="编辑用户头像-列表中",Key="user_center_user_list_edit_avatar",Remark="编辑用户头像-列表中",Path="",Icon="",Order=8,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="锁定用户",Key="user_center_user_lock",Remark="",Path="",Icon="",Order=7,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="刷新用户",Key="user_center_user_refresh",Remark="",Path="",Icon="",Order=3,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="用户分配角色",Key="user_center_user_role_edit",Remark="",Path="",Icon="",Order=5,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="保存用户角色",Key="user_center_user_role_edit_save",Order=0,ParentId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("490bc05f-499e-4f4c-811d-fde4c10be2ed"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 17:32:00"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-05-10 17:34:32"),UpdateBy="6",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }

}
