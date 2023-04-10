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

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class CodeManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="字典",Key="system_manager_code",Remark="字典管理",Path="/system_manager/code_list",Order=20,ParentId=Guid.Parse("b99ad8cf-68db-49aa-838f-17d57429d9c5"),Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:21:00"),UpdatedTime=DateTimeOffset.Parse("2023-04-10 13:15:51"),},
                new Resource() {Name="添加字典",Key="system_manager_code_add",Remark="添加字典",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("4b4f7b73-df18-4201-876e-b27e172f3b55"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:23:02"),},
                new Resource() {Name="删除字典",Key="system_manager_code_delete",Remark="删除字典",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("535e5f96-a036-4a40-96af-6c03cecadcd1"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:25:01"),UpdatedTime=DateTimeOffset.Parse("2023-04-07 21:25:09"),},
                new Resource() {Name="删除选中字典",Key="system_manager_code_delete_selected",Remark="删除选中字典",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("bedacea2-80f1-4d4a-b401-c82940f80d4c"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:22:21"),},
                new Resource() {Name="查看字典",Key="system_manager_code_detail",Remark="查看字典",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("36a4434a-f702-42be-a211-862d0b3b5288"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:24:41"),UpdatedTime=DateTimeOffset.Parse("2023-04-07 21:25:18"),},
                new Resource() {Name="生成字典种子数据",Key="system_manager_code_download_seed_data",Remark="生成字典种子数据",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("8c447e9b-1d39-48e5-b9b9-41ee2058b0c7"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:23:41"),},
                new Resource() {Name="编辑字典",Key="system_manager_code_edit",Remark="编辑字典",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("520f5cec-5f33-447c-a18b-59d8db31c5e9"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:24:22"),UpdatedTime=DateTimeOffset.Parse("2023-04-07 21:25:26"),},
                new Resource() {Name="导出字典",Key="system_manager_code_export",Remark="导出字典",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("1f289163-7fb0-49d2-9165-cbb111b6f3ab"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:22:43"),},
                new Resource() {Name="锁定字典",Key="system_manager_code_lock",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("a37b1cd8-98c4-4a93-a73e-436c138639eb"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:24:04"),},
                new Resource() {Name="字典管理",Key="system_manager_code_manager",Icon="tags",Order=90,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("b99ad8cf-68db-49aa-838f-17d57429d9c5"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 09:36:07"),UpdatedTime=DateTimeOffset.Parse("2023-04-09 09:39:55"),},
                new Resource() {Name="刷新字典列表",Key="system_manager_code_refresh",Remark="刷新字典列表",Order=0,ParentId=Guid.Parse("d5e3497b-c624-4fde-96bd-108a33cacc6d"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("d9fc6b89-25bb-458e-936f-d76eea2c680f"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:21:55"),},
                new Resource() {Name="字典类型",Key="system_manager_code_type",Path="/system_manager/code_type",Order=10,ParentId=Guid.Parse("b99ad8cf-68db-49aa-838f-17d57429d9c5"),Type=Enum.Parse<ResourceType>("Menu"),Id=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 09:38:43"),UpdatedTime=DateTimeOffset.Parse("2023-04-10 13:16:01"),},
                new Resource() {Name="添加字典类型",Key="system_manager_code_type_add",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("9800d45c-7ba8-4728-a6a6-a62dbc7b6f59"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:56:15"),},
                new Resource() {Name="管理字典类型下字典",Key="system_manager_code_type_codes_manager",Remark="功能与 字典管理->字典 相同",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("7819fe8f-8d81-4d00-af2b-c53ec010c65b"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:57:43"),UpdatedTime=DateTimeOffset.Parse("2023-04-09 14:02:43"),},
                new Resource() {Name="删除字典类型",Key="system_manager_code_type_delete",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("addfa7a9-7a8c-46ce-90f1-11424f385954"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:58:02"),},
                new Resource() {Name="删除选中字典类型",Key="system_manager_code_type_delete_selected",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("8e913a11-dbbe-4aa4-ad58-f12737039d83"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:55:44"),},
                new Resource() {Name="查看字典类型",Key="system_manager_code_type_detail",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("cd23a5d8-6eab-4e46-a730-56b2808551c6"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:57:23"),},
                new Resource() {Name="生成字典类型种子数据",Key="system_manager_code_type_download_seed_data",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("5676c9b3-2d06-4817-9614-4a34230bb05e"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:56:43"),},
                new Resource() {Name="编辑字典类型",Key="system_manager_code_type_edit",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("e1ab080c-b598-4c1c-9afa-45681f90f1e3"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:57:07"),},
                new Resource() {Name="导出字典类型",Key="system_manager_code_type_export",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("68ebd579-e2c7-4f1c-8f9f-7a06df30bd5f"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:56:01"),},
                new Resource() {Name="刷新字典类型列表",Key="system_manager_code_type_refresh",Order=0,ParentId=Guid.Parse("2eacd369-94ea-4e12-bf9e-744ae355e941"),Type=Enum.Parse<ResourceType>("Action"),Id=Guid.Parse("4e582063-f524-4ce2-9417-ac2cd957332d"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:55:20"),},
         };
        }
    }

}
