﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gardener.Authentication.Enums;
using Gardener.Base.Enums;
using Gardener.Base.Entity;

namespace Gardener.Audit.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AuditOperationServiceResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="操作审计",Key="system_manager_audit_operation",Remark="操作审计",Path="/system_manager/audit-operation",Icon="",Order=1,ParentId=Guid.Parse("2dd1a78c-f725-461b-8bc6-66112a7e156c"),Type=Enum.Parse<ResourceType>("Menu"),SupportMultiTenant=true,Hide=false,Id=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="删除操作审计",Key="system_manager_audit_operation_delete",Remark="删除操作审计",Path="",Icon="",Order=3,ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Type=Enum.Parse<ResourceType>("Action"),SupportMultiTenant=true,Hide=false,Id=Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="删除选中操作审计",Key="system_manager_audit_operation_delete_selected",Remark="",Path="",Icon="",Order=2,ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Type=Enum.Parse<ResourceType>("Action"),SupportMultiTenant=true,Hide=false,Id=Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="操作审计数据变更详情",Key="system_manager_audit_operation_detail",Remark="操作审计数据变更详情",Path="",Icon="",Order=0,ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Type=Enum.Parse<ResourceType>("Action"),SupportMultiTenant=true,Hide=false,Id=Guid.Parse("24ace337-41fe-429d-b32e-d9f88bd97aaa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
                new Resource() {Name="导出操作审计",Key="system_manager_audit_operation_export",Remark="导出操作审计",Order=0,ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Type=Enum.Parse<ResourceType>("Action"),SupportMultiTenant=true,Hide=false,Id=Guid.Parse("2ac78309-1719-4ea5-ac0f-6974a86f168c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-19 18:40:37"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="查看操作审计参数",Key="system_manager_audit_operation_parameters",Order=0,ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Type=Enum.Parse<ResourceType>("Action"),SupportMultiTenant=true,Hide=false,Id=Guid.Parse("0dc989c3-d60a-4ac3-89be-87f485ca820d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-11-08 15:46:27"),CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-11-08 17:12:54"),UpdateBy="2",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="刷新操作审计",Key="system_manager_audit_operation_refresh",Remark="刷新操作审计",Path="",Icon="",Order=1,ParentId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),Type=Enum.Parse<ResourceType>("Action"),SupportMultiTenant=true,Hide=false,Id=Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),},
         };
        }
    }
}
