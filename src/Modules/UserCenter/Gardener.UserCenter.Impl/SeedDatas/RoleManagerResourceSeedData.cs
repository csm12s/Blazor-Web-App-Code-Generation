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

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class RoleManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="角色管理",Key="user_center_role",Remark="",Path="/user_center/role",Icon="user-switch",Order=20,ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),},
                new Resource() {Name="添加角色",Key="user_center_role_add",Remark="",Path="",Icon="",Order=2,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),},
                new Resource() {Name="删除角色",Key="user_center_role_delete",Remark="",Path="",Icon="",Order=1,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),},
                new Resource() {Name="删除选中角色",Key="user_center_role_delete_selected",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),},
                new Resource() {Name="查看角色详情",Key="user_center_role_detail",Remark="查看角色详情",Path="",Icon="",Order=0,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("2c1c895c-6434-4f14-91f2-144e48457101"),},
                new Resource() {Name="编辑角色",Key="user_center_role_edit",Remark="",Path="",Icon="",Order=4,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),},
                new Resource() {Name="锁定角色",Key="user_center_role_lock",Remark="",Path="",Icon="",Order=7,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),},
                new Resource() {Name="刷新角色",Key="user_center_role_refresh",Remark="",Path="",Icon="",Order=3,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),},
                new Resource() {Name="获取种子数据",Key="user_center_role_resource_download_seed_data",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"),},
                new Resource() {Name="查看角色资源",Key="user_center_role_resource_select",Remark="查看角色资源",Path="",Icon="",Order=0,ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Type=Enum.Parse<ResourceType>("View"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a2b68c70-173f-46fa-8442-e19219a9905b"),},
                new Resource() {Name="角色分配资源",Key="user_center_role_set_resource",Remark="",Path="",Icon="",Order=5,ParentId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),},
                new Resource() {Name="保存角色资源",Key="user_center_role_set_resource_save",Remark="保存角色资源",Path="",Icon="",Order=0,ParentId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("f2ca3ab7-40da-4828-ad63-06bc9af9b153"),},
         };
        }
    }

}
