﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using HttpMethod = Gardener.Enums.HttpMethod;
namespace Gardener
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EasyJobFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="获取运行次数统计",Key="252A8700472411400DD3D95A11185B29",Description="获取运行次数统计",Path="/api/sys-job-log/count",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("5c83a810-f270-4602-9cc5-4cae64cc032c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-21 10:43:36"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="获取评价耗时",Key="29ED2AB066CE25E3E47E9559B3208AE9",Description="获取评价耗时",Path="/api/sys-job-log/avg-elapsed-time",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("eea858a2-155b-4934-b4b6-219beaea44f8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-21 10:43:35"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="获取总运行次数统计",Key="5DFACCCADE0F43ADCB6F25998F7A5A23",Description="获取总运行次数统计",Path="/api/sys-job-log/all-count",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("78fc70a3-d21b-4c7f-8e21-9833ece89eef"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-21 10:43:35"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-用户配置服务",Summary="保存我的配置",Key="38E5B2CA0F23B044D942A3DA7B0B6753",Description="save-my-config",Path="/api/sys-job-user-config/save-my-config",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("249577f0-a0e4-42ea-beed-2a035533933f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:41"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-用户配置服务",Summary="获取我的的配置",Key="A541E3B27D6BDF23C3252773DD8CC4AF",Description="my-config",Path="/api/sys-job-user-config/my-config",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("e8e5ebed-ed84-485a-8f51-3ea62ed13af8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:41"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="导出",Key="9CCF4227F72557BC6FE51375513D60C0",Description="导出数据",Path="/api/sys-job-log/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("8d2e5d85-7614-4439-8179-7f68e8df98a6"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:20"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:38"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="搜索",Key="031BF6B2A5FFEE9BFB7DE241462FDA13",Description="搜索数据",Path="/api/sys-job-log/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("0ed7905f-7825-4f7f-8a00-18cd2da1e89b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:38"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="生成种子数据",Key="F1DF6F9C151F1EEE2913A6AE951B6DA4",Description="根据搜索条叫生成种子数据",Path="/api/sys-job-log/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("266ffe8c-5b42-49c0-8766-90b9b873c936"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:38"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="查询所有",Key="E266CA5054C33D6E517A79A0C08439CA",Description="查找到所有数据",Path="/api/sys-job-log/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("447cd9f2-4fce-4773-ac11-3151c0d409ab"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:37"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="查询所有可以用的",Key="2159B1467D9C7C4B4EAD35A9688DD546",Description="查询所有可以用的记录，(实现Gardener.Base.IModelDeletedGardener.Base.IModelLocked时会自动过滤)",Path="/api/sys-job-log/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("6d0bdc59-8a62-4283-99d2-a586a8e1d191"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:37"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="添加",Key="7B13C1DB47BEA797793C821E7CE45C2E",Description="添加一条数据",Path="/api/sys-job-log",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("6dbeb4e1-2261-46d9-bf5a-47402fcfdb10"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:37"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="分页查询",Key="7E2DA42467945755B177CDB67C6D06E1",Description="根据分页参数，分页获取数据",Path="/api/sys-job-log/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("d3aad533-d348-4950-9cf9-942d855cc0ab"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:38"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="更新",Key="90C0582034E9DC3F2CC225E2389B1B12",Description="更新一条数据",Path="/api/sys-job-log",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("fc1aed1d-95f3-45a6-aedc-5e10608d7ad9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:19"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:37"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="删除",Key="294EBF9A693BCEB4275EFC8E42672421",Description="根据主键删除一条数据",Path="/api/sys-job-log/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("0e09e9cd-1cdd-4e61-85cd-51bcc1595ed5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:18"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:36"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="根据主键获取",Key="3F6FDE46F3B4734DFAFECC355C9F0247",Description="根据主键查找一条数据",Path="/api/sys-job-log/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("26afb233-f6cc-47bb-b367-0b0b7ce3fe42"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:18"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:36"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="批量逻辑删除",Key="BBAE20709D963C9CDE52C8D4136501FC",Description="根据多个主键批量逻辑删除",Path="/api/sys-job-log/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("38c2fd8d-2b26-49c4-8628-6ab902b6fbd1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:18"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:37"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="锁定",Key="6669B8BF7342B215BEA3CF99CCEF3021",Description="根据主键锁定或解锁数据（必须实现Gardener.Base.IModelLocked才能生效）",Path="/api/sys-job-log/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("60012cd5-1fed-47f9-bd06-1c0f636ec849"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:18"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:37"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="逻辑删除",Key="C087B68DD57154402DAA02E2860BE46B",Description="根据主键逻辑删除",Path="/api/sys-job-log/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("c59d7c7d-968f-44e7-b272-06dcf4a3af80"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:18"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:36"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-执行日志",Summary="批量删除",Key="A4705E7A8B52476BB6D41EF3EDF72657",Description="根据多个主键批量删除",Path="/api/sys-job-log/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ee691438-4f45-4321-aff0-1ab4f9227d36"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 11:26:18"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:36"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="导出",Key="5DF206D5BE4146D9221F0409813C5DDC",Description="导出数据",Path="/api/sys-job-trigger/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("af387085-414a-4429-b4cb-c33d4f8c8b08"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:47"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:41"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="搜索",Key="44D6877ADF12C7DDD6522B38E10AD12F",Description="搜索数据",Path="/api/sys-job-trigger/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("2f2ac0d9-9544-4d5d-95f7-adbb4171e4ad"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:46"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:40"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="分页查询",Key="3DB691C14996A10C6B1DB22AEC3A3B20",Description="根据分页参数，分页获取数据",Path="/api/sys-job-trigger/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("3f591c5b-d62c-40ae-99a5-a831f162c1d0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:46"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:40"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="查询所有可以用的",Key="19B18F92EDA27D1FB311472D113B3253",Description="查询所有可以用的记录，(实现Gardener.Base.IModelDeletedGardener.Base.IModelLocked时会自动过滤)",Path="/api/sys-job-trigger/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("be18722f-c63a-4e0a-8ac5-5e40affd683a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:46"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:40"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="生成种子数据",Key="64C00B08D3BD0ED31F23FC1A435A40B1",Description="根据搜索条叫生成种子数据",Path="/api/sys-job-trigger/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("beb703df-93b7-476f-bab9-b39e959de7cb"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:46"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:40"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="启动触发器",Key="9C06292396D2C4D79789365C2DA6018F",Description="start",Path="/api/sys-job-trigger/{id}/start",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("193923dc-e968-4e75-9531-b090ffae7eb7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:45"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:39"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="锁定",Key="A971718E3DB3188F657CFB6005EC46B0",Description="根据主键锁定或解锁数据（必须实现Gardener.Base.IModelLocked才能生效）",Path="/api/sys-job-trigger/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("243c2cfc-882c-4ee8-8089-381940ae95b3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:45"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:40"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="暂停触发器",Key="D13F607FA2DD2713DD65DD041955C919",Description="pause",Path="/api/sys-job-trigger/{id}/pause",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("7d53bd2b-b5c2-4a79-91e7-591bd8bb4d22"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:45"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:39"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="查询所有",Key="ED997ECAB452513909149870DBBCD8C9",Description="查找到所有数据",Path="/api/sys-job-trigger/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("fda58b63-876d-4a07-aff2-a8b7b1cccbf1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:45"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:40"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="添加触发器",Key="706122723A90FAEA667B09B6C1674586",Description="sys-job-trigger",Path="/api/sys-job-trigger",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("6f9c2287-753e-42d4-a465-c61dd1820fcf"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:38"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="更新触发器",Key="216856E2C1221C97EE992D99475D634C",Description="sys-job-trigger",Path="/api/sys-job-trigger",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("779b4330-2c14-4733-8161-e1230a9db78c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:39"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="导出",Key="0F0E01B3B7600A2878E8F72385DC8D5B",Description="导出数据",Path="/api/sys-job-detail/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("78658122-281d-49e6-b5df-965f6b5cd928"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:35"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="根据主键获取",Key="D0139AB74A413C492B3F61ADAD7BC3B9",Description="根据主键查找一条数据",Path="/api/sys-job-trigger/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b63017f6-d5a9-469f-a682-b0a2ba622f0b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:39"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-触发器服务",Summary="删除触发器",Key="61F373C2C626E7C8191F2DD51937E120",Description="{id}",Path="/api/sys-job-trigger/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("f1be2be4-b212-4725-9d5b-d99f0d545a64"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:44"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:39"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="分页查询",Key="18E4317FFFF340A907659C39388E5E7F",Description="根据分页参数，分页获取数据",Path="/api/sys-job-detail/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("36b5de47-1593-4bdd-a560-b0dcbafec479"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:43"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:35"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="查询所有可以用的",Key="0C1E3AF5928F44C16B6C450757107BF2",Description="查询所有可以用的记录，(实现Gardener.Base.IModelDeletedGardener.Base.IModelLocked时会自动过滤)",Path="/api/sys-job-detail/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("3eb56a7c-5d33-417f-a25f-d6fb0313e7b9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:43"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:35"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="生成种子数据",Key="82E05320B20B349F0E1D372635ADA5E2",Description="根据搜索条叫生成种子数据",Path="/api/sys-job-detail/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("e90586ea-84da-4317-a838-e8e3606c435a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:43"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:35"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="锁定",Key="2D6347AFACD73B12DF7497D52645A457",Description="根据主键锁定或解锁数据（必须实现Gardener.Base.IModelLocked才能生效）",Path="/api/sys-job-detail/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("1d2e4602-d6a7-458c-9b76-638bcb8e8e41"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:42"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:34"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="强制唤醒作业调度器",Key="8D85FAA54FEC4704B8DFC96869731384",Description="cancel-sleep",Path="/api/sys-job-detail/cancel-sleep",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("25c5b700-f0b1-401a-8e8b-595871ec07d2"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:42"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:34"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="启动所有作业",Key="A2B39105DE1F36B4B4C677C46B9DF054",Description="start-all",Path="/api/sys-job-detail/start-all",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("6701f6a8-4a7f-4227-980b-ee83c0521dfe"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:42"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:34"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="查询所有",Key="5206FB08975DB96432FC50B6B022ADD3",Description="查找到所有数据",Path="/api/sys-job-detail/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a49af65a-b4a7-467b-b990-f3c2f7dc41d0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:42"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:34"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="强制触发所有作业持久化",Key="BA479ECBDB6D8B47F8690F50763178D2",Description="persist-all",Path="/api/sys-job-detail/persist-all",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("f39549ad-0c4b-4afb-9def-d1a12f2e3ac2"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:42"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:34"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="启动作业",Key="EB2C0C58149EDD4DC2FFE0AC73C6622D",Description="start",Path="/api/sys-job-detail/{id}/start",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("2facf11b-2c54-4707-9595-1d9de825896f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:41"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:33"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="获取触发器列表",Key="A70575154F4DA76A127CCCE27A0B9D09",Description="triggers",Path="/api/sys-job-detail/{id}/triggers",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("9ceb36b7-1057-45c5-a0e7-6575799f200e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:41"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:33"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="暂停作业",Key="C09AEAEC7280AAF5A40F1BC1C44EB17F",Description="pause",Path="/api/sys-job-detail/{id}/pause",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("be6df395-460d-4178-aa13-2b3b7d5848fc"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:41"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:33"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="暂停所有作业",Key="70D9DBD0B77FF3151247CA5016C52C43",Description="pause-all",Path="/api/sys-job-detail/pause-all",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("d93c511e-85dc-4ea2-945b-04208cd7209e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:41"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:33"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="删除作业",Key="88C0D3FA08913DAD27E6753C67F4966B",Description="{id}",Path="/api/sys-job-detail/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("2032eead-2715-4f84-9cb1-2e7bcadad2ab"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:40"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:32"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="获取作业分页列表",Key="0BFE4E9CB17E57D7392BB456852BB4A4",Description="获取作业分页列表",Path="/api/sys-job-detail/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("29e9de55-7f46-4ffe-8b16-4b928046a679"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:40"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:32"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="根据主键获取",Key="4A4E169C999D217D282DBD9BBCA336A4",Description="根据主键查找一条数据",Path="/api/sys-job-detail/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a95c3584-f7cd-4641-a4ad-397e5a3ea5f4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:40"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:33"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="添加作业",Key="148C36B57EC8C852F84FC21E844C0163",Description="",Path="/api/sys-job-detail",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ed2911d8-c745-4140-90d6-7d82b5252b34"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:40"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:32"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-任务服务",Summary="更新作业",Key="481C508AB16310CE183E3B581DF4F170",Description="sys-job-detail",Path="/api/sys-job-detail",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("f71b16b5-22d1-4968-aeeb-bbfc3c53aebf"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:40"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:32"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="导出",Key="3AF008ABB9C245E298BB4979FACBBD9A",Description="导出数据",Path="/api/sys-job-cluster/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("7e88962e-c70a-4bf2-9c74-73580272f4c5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:39"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:32"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="生成种子数据",Key="389F6FFE352F3DFAA30FCE75298B9DD9",Description="根据搜索条叫生成种子数据",Path="/api/sys-job-cluster/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("902d0f6c-749e-4e63-acd2-31333c0d714b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:39"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:31"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="搜索",Key="920C2BA08C04B360AB86451C77946428",Description="搜索数据",Path="/api/sys-job-cluster/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ca4b7fd2-e475-43b6-a11f-7f750c5353e5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:39"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:31"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="分页查询",Key="0F4BC78106D98D1E9EE125720D15421B",Description="根据分页参数，分页获取数据",Path="/api/sys-job-cluster/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f1eb5125-d554-4d6d-ade9-63fe946f68a1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:39"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:31"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="查询所有可以用的",Key="9B5272E44335CDF2AC923CE65D7E0554",Description="查询所有可以用的记录，(实现Gardener.Base.IModelDeletedGardener.Base.IModelLocked时会自动过滤)",Path="/api/sys-job-cluster/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("fd4b7828-d39e-43fd-809c-45a8b029da4b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:39"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:31"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="更新",Key="FA7A37CB27B4123695BBECFD6367477F",Description="更新一条数据",Path="/api/sys-job-cluster",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("00553573-3f38-4b36-a14d-8e7b02385227"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:38"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:31"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="锁定",Key="B28B50D83638ACB6736816E840C51258",Description="根据主键锁定或解锁数据（必须实现Gardener.Base.IModelLocked才能生效）",Path="/api/sys-job-cluster/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("010c6209-af3b-42e6-9aac-bf364a430b4a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:38"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:30"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="添加",Key="CA75C125F921B24F0EAEC77C7D8811EA",Description="添加一条数据",Path="/api/sys-job-cluster",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("b50c5d91-c9cc-4ada-a3ad-ff54ceeb9867"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:38"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:30"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="查询所有",Key="0AA28DDDE1B4141C8018EAED7D6FA962",Description="查找到所有数据",Path="/api/sys-job-cluster/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f308aab6-5ec1-49e0-a5ff-7d8628eaec04"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:38"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:31"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="批量删除",Key="E16BDE0C7B55DB96AB16ABEBD1A265A6",Description="根据多个主键批量删除",Path="/api/sys-job-cluster/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("12e93db7-e84c-4778-8320-716cbd007e5c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:37"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:30"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="逻辑删除",Key="14D15BBD93AFE26B4E612AE1B2142FFB",Description="根据主键逻辑删除",Path="/api/sys-job-cluster/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("37bbd6b1-d02e-4a58-9b86-f5040fb64ca6"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:37"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:30"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="批量逻辑删除",Key="E05FA485A9B7F755731843431FEFD138",Description="根据多个主键批量逻辑删除",Path="/api/sys-job-cluster/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("4827dfb4-a7ca-40e7-9f1e-f40d3e06154a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:37"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:30"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="根据主键获取",Key="D9933D6648C8963EB5526C4D69AB9DD3",Description="根据主键查找一条数据",Path="/api/sys-job-cluster/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("539f1ab4-c233-4784-b799-114ca7804abc"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:37"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:29"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="删除",Key="5040DEEE517F25D216E5F9386BBD98F2",Description="根据主键删除一条数据",Path="/api/sys-job-cluster/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("8e3c85a2-f8be-4fc0-9000-76e17ddf02ea"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:37"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:29"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="当前作业调度器宕机",Key="7F04CE6DC826FC46970D7A966A97B030",Description="crash",Path="/api/sys-job-cluster/crash",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("3e647086-ddb3-4805-a398-76fdcda0c38b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:36"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:29"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="当前作业调度器启动通知",Key="D175B36C107DDB70189298950CE76EBB",Description="start",Path="/api/sys-job-cluster/start",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("979397e7-b307-4332-9bb8-8fd1d9a647de"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:36"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:28"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="等待被唤醒",Key="CD221DF0530F7DEC732F6D8D5725EF87",Description="waiting",Path="/api/sys-job-cluster/waiting",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("a03b98e7-d63d-4e24-bfce-bdf7ebef8b43"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:36"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:29"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="定时任务服务",Service="定时任务-集群服务",Summary="当前作业调度器停止通知",Key="592A5AEB36B8888B2E6EA31DB3CC5FF4",Description="stop",Path="/api/sys-job-cluster/stop",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("fe32ce02-5c4a-45f7-9929-1166e3ac4a3f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-03 15:20:36"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-21 10:43:29"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }

}
