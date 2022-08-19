// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Domains;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.Audit.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AuditEntityManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="数据审计",Key="system_manager_audit_entity",Remark="数据审计",Path="/system_manager/audit-entity",Icon="",Order=2,ParentId=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),},
                new Resource() {Name="删除数据审计",Key="system_manager_audit_entity_delete",Remark="删除数据审计",Path="",Icon="",Order=3,ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),},
                new Resource() {Name="删除选中数据审计",Key="system_manager_audit_entity_delete_selected",Remark="删除选中数据审计",Path="",Icon="",Order=2,ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),},
                new Resource() {Name="查询数据审计详情",Key="system_manager_audit_entity_detail",Remark="查询数据审计详情",Path="",Icon="",Order=4,ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("3f8d700a-bc26-4d5c-9622-d98bf9359159"),},
                new Resource() {Name="刷新数据审计",Key="system_manager_audit_entity_refresh",Remark="刷新数据审计",Path="",Icon="",Order=1,ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),},
                new Resource() {Name="导出数据审计",Key="system_manager_audit_entity_export",Remark="导出数据审计",Order=0,ParentId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:41:06"),Id=Guid.Parse("4f259695-23ea-4453-a4f1-2b055d135c37"),},
         };
        }
    }
}
