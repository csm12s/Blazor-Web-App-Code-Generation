using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Gardener.Base.Domains;
using Gardener.Base.Enums;
using Gardener.Authentication.Enums;

namespace Gardener.SysTimer
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="任务调度",Key="system_manager_timer",Remark="配置任务调度模式",Path="/system_manager/systimer",Icon="robot",Order=80,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),UpdatedTime=DateTimeOffset.Parse("2022-08-16 13:59:01"),Id=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),},
                new Resource() {Name="添加任务调度",Key="system_manager_timer_add",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:06:51"),Id=Guid.Parse("3f7f572f-1df2-4d20-b323-489e44196ad0"),},
                new Resource() {Name="删除调度",Key="system_manager_timer_delete",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:00:41"),Id=Guid.Parse("7c711d3c-c755-419b-827a-e8e7087984b8"),},
                new Resource() {Name="删除选中调度",Key="system_manager_timer_delete_selected",Remark="删除选中调度",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:00:04"),Id=Guid.Parse("a8a4b47a-7bdc-4b03-8c46-d3cd240ae8c9"),},
                new Resource() {Name="查看任务调度",Key="system_manager_timer_detail",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:07:35"),Id=Guid.Parse("df132f66-027e-4791-af7a-26e496dc8e5a"),},
                new Resource() {Name="编辑任务调度",Key="system_manager_timer_edit",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:06:22"),Id=Guid.Parse("f884f2f9-f884-4440-8a2c-7a883ac54660"),},
                new Resource() {Name="刷新调度列表",Key="system_manager_timer_refresh",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:04:01"),Id=Guid.Parse("d0d6f112-73a4-44ba-82a4-a3ad8bdb6978"),},
                new Resource() {Name="开启调度",Key="system_manager_timer_start",Remark="开启调度",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:01:50"),Id=Guid.Parse("2b0d5eb7-4626-4273-b124-8816259a2902"),},
                new Resource() {Name="停止调度",Key="system_manager_timer_stop",Order=0,ParentId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-16 14:03:00"),Id=Guid.Parse("51991266-0a62-4f8b-ab7a-3bdc48595ea0"),},
         };
        }
    }
}