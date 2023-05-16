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
                new Resource() {Name="后台租户管理员",Key="system_tenant_administrator",Order=0,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("62e874c8-d286-4b28-831b-90d0c49f0908"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 18:13:46"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-05-11 14:21:38"),UpdateBy="2",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="租户管理",Key="user_center_tenant",Path="/user_center/tenant",Icon="deployment-unit",Order=0,ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:04:22"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="添加租户",Key="user_center_tenant_add",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d45effb9-67a8-4459-83ac-c3852c8b4f1f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:08:36"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除租户",Key="user_center_tenant_delete",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d92268ec-6b51-4514-9487-52cb3fb0d850"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:22:42"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除选中租户",Key="user_center_tenant_delete_selected",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("efbcc18b-c193-42cc-b315-cde07f51b496"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:06:44"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="查看租户",Key="user_center_tenant_detail",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("07af05b1-6f3e-49fa-9959-463e246346df"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:15:38"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-05-10 16:21:50"),UpdateBy="6",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="编辑租户",Key="user_center_tenant_edit",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("b4072d45-f643-4bdb-a63e-7286cfa9c62b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:14:39"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="锁定租户",Key="user_center_tenant_lock",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("4db9a237-1343-4c4a-91f6-9a40fb9f0e2a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:11:08"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="刷新租户",Key="user_center_tenant_refresh",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("8b2007b4-821b-49fc-aa5d-35ebc4dbe3c9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 16:09:32"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-05-10 16:19:09"),UpdateBy="6",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="绑定资源",Key="user_center_tenant_set_resource",Order=0,ParentId=Guid.Parse("fe93e8fb-0b55-43fb-baa7-450cdcca8f6a"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-16 18:04:32"),CreateBy="5",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="绑定资源-保存",Key="user_center_tenant_set_resource_save",Order=0,ParentId=Guid.Parse("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("807029ec-be10-4faa-a332-bcb1021ff966"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-16 18:08:28"),CreateBy="5",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="查看已有资源",Key="user_center_tenant_set_resource_select",Order=0,ParentId=Guid.Parse("3a2c9195-9a5c-42c7-b5dc-7300bbc66e8c"),Type=Enum.Parse<ResourceType>("View"),Id=Guid.Parse("0898c23e-3c3c-4d7f-82ef-9255e11d9af8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-16 18:07:38"),CreateBy="5",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-05-16 18:10:42"),UpdateBy="5",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }
}
