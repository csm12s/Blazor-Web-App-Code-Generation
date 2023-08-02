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

namespace Gardener.EasyJob.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EasyJobResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="定时任务",Key="system_manager_easy_job",Path="",Icon="hourglass",Order=100,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-12 14:06:55"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:53:02"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="任务",Key="system_manager_easy_job_detail",Path="/system_manager/easy_job_detail",Order=10,ParentId=Guid.Parse("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 10:46:55"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:54:18"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="添加",Key="system_manager_easy_job_detail_add",Order=0,ParentId=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d137d256-a643-4e1d-bec2-2489f4f3630c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-18 16:44:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:49:39"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除",Key="system_manager_easy_job_detail_delete",Order=0,ParentId=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("e787f680-8ad9-4154-a036-4978162c8b56"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-18 16:46:07"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:47:18"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除选中",Key="system_manager_easy_job_detail_delete_selected",Order=0,ParentId=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("f38347fd-11a3-4e1c-a1b0-a445510e7d8c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-18 16:44:02"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:50:04"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="查看详情",Key="system_manager_easy_job_detail_detail",Order=0,ParentId=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("ae604973-bb28-4deb-87a5-c3da8b88d6d3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-18 16:45:51"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:47:50"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="编辑",Key="system_manager_easy_job_detail_edit",Order=0,ParentId=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("696c06fd-a230-4472-adca-d378747091a4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-18 16:45:31"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:48:41"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="刷新",Key="system_manager_easy_job_detail_refresh",Order=0,ParentId=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("5e858248-f765-4412-9753-92621f20f611"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-18 16:43:39"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:50:29"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="搜索",Key="system_manager_easy_job_detail_search",Order=0,ParentId=Guid.Parse("4e45db1a-8426-4ae9-8e20-1062c27a8d5f"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("693cd650-3b03-4bdf-8080-14112547329c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-18 16:45:02"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-07-27 10:49:08"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="触发器",Key="system_manager_easy_job_trigger",Path="/system_manager/easy_job_trigger",Order=20,ParentId=Guid.Parse("32a91c2f-451c-4f41-91c7-f648bfcd3fff"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 10:58:28"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="添加",Key="system_manager_easy_job_trigger_add",Order=0,ParentId=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("cb7772c4-6dda-4c0a-aa7b-c506b303da02"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 10:59:38"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除",Key="system_manager_easy_job_trigger_delete",Order=0,ParentId=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d25b3920-8833-4168-bff1-1065cd72c8c7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 11:00:58"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="删除选中",Key="system_manager_easy_job_trigger_delete_selected",Order=0,ParentId=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("aa2109af-a9cd-48fd-b8b4-a872749b14eb"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 10:59:22"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="查看详情",Key="system_manager_easy_job_trigger_detail",Order=0,ParentId=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("9299ac14-8d67-45a0-846e-ab35d15c0fbc"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 11:00:40"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="编辑",Key="system_manager_easy_job_trigger_edit",Order=0,ParentId=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("61f0721c-a1d2-4b11-99e0-2e56533a433c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 11:00:20"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="刷新",Key="system_manager_easy_job_trigger_refresh",Order=0,ParentId=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("95d5c35c-fdb3-4fec-bc6c-92aa5f61680f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 10:59:57"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Resource() {Name="搜索",Key="system_manager_easy_job_trigger_search",Order=0,ParentId=Guid.Parse("8d1e102e-02f6-4735-a46e-918ded1a9a19"),SupportMultiTenant=false,Hide=false,Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d1c1f5c6-907b-47db-9d1f-6c6d87a64494"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-07-27 10:58:58"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }
}
