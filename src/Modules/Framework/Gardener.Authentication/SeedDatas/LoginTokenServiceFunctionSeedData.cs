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
using Gardener.Authentication.Enums;

namespace Gardener.Authentication.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class LoginTokenServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="生成种子数据",Key="7A0F189854BBC9084AE004012A7870E9",Description="根据搜索条叫生成种子数据",Path="/api/login-token/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("336b98be-e9f1-4f42-824b-a9a3b91350c5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="批量删除",Key="D522DFCA12CBF851EA48D676E7432DF8",Description="根据多个主键批量删除",Path="/api/login-token/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("040878a9-1b78-494e-9ee1-b4a7eab118fb"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="添加",Key="7E91BA4770C4FDF6B865C2D4C7984132",Description="添加一条数据",Path="/api/login-token",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("20b7e3c2-1ab5-4a5e-993e-e5599a583fdd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="查询所有可以用的",Key="33D096038DC823412DC051FA7371FB68",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/login-token/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("5328608a-6b71-4507-a52a-e1beffa7a4ab"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="逻辑删除",Key="A094FB1391CD1E7F7A2C7E8536A491DF",Description="根据主键逻辑删除",Path="/api/login-token/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("571200a8-bde2-430b-84ea-743db7b282cd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="批量逻辑删除",Key="80A4CB99380D0D7A70F7C28604C5B0C7",Description="根据多个主键批量逻辑删除",Path="/api/login-token/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("6dc1a088-15f6-43b8-8465-3a95cc495bab"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="删除",Key="873AFCBA915D056ED9D8EDA9D23F9061",Description="根据主键删除一条数据",Path="/api/login-token/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("8f114b96-dc3d-4dd4-854a-4c793c121e43"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="更新",Key="2909033694587286F3458217843E20D8",Description="更新一条数据",Path="/api/login-token",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("a2e21aa5-c2ff-4893-954f-263822d168c3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="分页查询",Key="4A8F7B6AB1E6A30A9A65FEBE2B31CE4A",Description="根据分页参数，分页获取数据",Path="/api/login-token/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("e298058c-8ec9-4637-bf8b-4ece0bfa5a5b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="搜索",Key="860A62FFC20FAAAE60E760D4305104DF",Description="搜索数据",Path="/api/login-token/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("e651d9a4-9d6d-44c7-a833-08da6ed19892"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="根据主键获取",Key="C01E9238420548B6CA87C312935DD043",Description="根据主键查找一条数据",Path="/api/login-token/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f1267fbc-903b-4439-a7b6-a7290507d207"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="锁定",Key="B37ED1BEEE60098FACB7182C73B5FA3F",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/login-token/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("f59833a1-c9af-4bb2-be4b-d6935513fc99"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="查询所有",Key="8C9ED3E66D288CA942AD438AD9C50DBF",Description="查找到所有数据",Path="/api/login-token/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f6fd9621-f6e4-45ec-b919-6acb73c7b303"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="用户登录TOKEN服务",Summary="导出",Key="91D755377D8744A976836037290BB199",Description="导出数据",Path="/api/login-token/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:25:20"),Id=Guid.Parse("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"),},
         };
        }
    }

}
