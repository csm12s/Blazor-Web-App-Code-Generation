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

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class FunctionManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="接口管理",Key="system_manager_function",Remark="",Path="/system_manager/function",Icon="api",Order=40,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),},
                new Resource() {Name="删除接口",Key="system_manager_function_delete",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),},
                new Resource() {Name="删除选中接口",Key="system_manager_function_delete_selected",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),},
                new Resource() {Name="查看接口详情",Key="system_manager_function_detail",Remark="查看接口详情",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"),},
                new Resource() {Name="查看接口种子数据",Key="system_manager_function_download_seed_data",Remark="查看接口种子数据",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("cc8a9836-3c4d-4d0b-ae64-a31a6bb36b6f"),},
                new Resource() {Name="编辑接口",Key="system_manager_function_edit",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("50062351-8235-4da1-9f90-4917d0e8abe0"),},
                new Resource() {Name="锁定接口",Key="system_manager_function_enable_audit",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("6e487179-5bb2-4ab5-80e3-58c514c9595f"),},
                new Resource() {Name="导入接口",Key="system_manager_function_import",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),},
                new Resource() {Name="刷新接口",Key="system_manager_function_refresh",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),},
                new Resource() {Name="导出接口",Key="system_manager_function_export",Remark="导出接口",Order=0,ParentId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:31:15"),Id=Guid.Parse("4171f5aa-2ce1-40ad-b69e-59de1cd20416"),},
         };
        }
    }

}
