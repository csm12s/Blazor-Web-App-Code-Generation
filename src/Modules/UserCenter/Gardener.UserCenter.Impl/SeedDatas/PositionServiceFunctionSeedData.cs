// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gardener.Base.Domains;
using Gardener.Enums;

namespace Gardener.UserCenter.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class PositionServiceFunctionSeedData : IEntitySeedData<Function>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Function> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="生成种子数据",Key="A074E8CFB7457551C240FE7D510618AC",Description="根据搜索条叫生成种子数据",Path="/api/position/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("6b7f0b3c-c2ed-458e-8f26-abe68eb17854"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="更新",Key="47FEFB8B545A5A813AB9ABA70F02BD49",Description="更新一条数据",Path="/api/position",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("05153ee4-dc99-4834-b398-5999f7dc8d01"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="删除",Key="34B7575A20F0D8D6B1B2522F9DD7A7B8",Description="根据主键删除一条数据",Path="/api/position/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("61cc62e4-34da-4a0a-9899-488d3ab399fa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="逻辑删除",Key="BB0B0620A9F5665B13ADC8D8C8B8F98A",Description="根据主键逻辑删除",Path="/api/position/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="查询所有",Key="88BAC4E29D23BD095207644BB397E5EE",Description="查找到所有数据",Path="/api/position/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("69f70da1-fb4e-443f-9efe-e3d12cc95eed"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="根据主键获取",Key="DF0B66D0FC43BB25047A470707E01EF8",Description="根据主键查找一条数据",Path="/api/position/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="添加",Key="1EB184263BA127C79364162F4E75E660",Description="添加一条数据",Path="/api/position",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("7a3399b3-6003-4aae-8e24-2e478992630e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="批量逻辑删除",Key="710C2B0A026A9C3FF0D6235FCD8E0F26",Description="根据多个主键批量逻辑删除",Path="/api/position/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("89954833-64a5-4c87-a717-9c863ca3b263"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="查询所有可以用的",Key="C47AACD68B1EF833AAC0EC90CD878FDD",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/position/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b56c4126-411c-445e-86aa-a91a5ce816d4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="搜索",Key="9A501F3D2F0A3A2D47A17D6F42042CD5",Description="搜索数据",Path="/api/position/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("c715a6d5-cd99-4c94-8760-936817c1e09c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="分页查询",Key="F543F08AB768F7D444481F5D7EB52373",Description="根据分页参数，分页获取数据",Path="/api/position/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("d22007c6-fada-4ef1-bafa-08455b767883"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="批量删除",Key="C9E5F9B494BBF428A85ECEA53B095285",Description="根据多个主键批量删除",Path="/api/position/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ed340c0c-9b63-45f4-942a-c8a14c4491d3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="岗位管理服务",Summary="锁定",Key="9501F9B0B5D4867FF65611B203B43D69",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/position/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("f6843cdf-133d-4eb8-92b2-c36fe63ea9d7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }

    }
}