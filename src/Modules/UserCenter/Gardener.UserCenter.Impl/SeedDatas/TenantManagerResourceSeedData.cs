﻿// -----------------------------------------------------------------------------
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
    public class TenantManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="租户管理",Key="user_center_tenant",Path="/user_center/tenant",Icon="deployment-unit",Order=0,ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:04:22"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="添加租户",Key="user_center_tenant_add",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d45effb9-67a8-4459-83ac-c3852c8b4f1f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:08:36"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除租户",Key="user_center_tenant_delete",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d92268ec-6b51-4514-9487-52cb3fb0d850"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:22:42"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除选中租户",Key="user_center_tenant_delete_selected",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("efbcc18b-c193-42cc-b315-cde07f51b496"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:06:44"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="查看租户",Key="user_center_tenant_detail",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("07af05b1-6f3e-49fa-9959-463e246346df"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:15:38"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-05-10 16:21:50"),UpdateBy="6",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="编辑租户",Key="user_center_tenant_edit",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("b4072d45-f643-4bdb-a63e-7286cfa9c62b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:14:39"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="锁定租户",Key="user_center_tenant_lock",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("4db9a237-1343-4c4a-91f6-9a40fb9f0e2a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:11:08"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="刷新租户",Key="user_center_tenant_refresh",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:09:32"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-05-10 16:19:09"),UpdateBy="6",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }
}
