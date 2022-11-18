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

namespace Gardener.SysTimer.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class FunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="生成种子数据",Key="8323C7FD5DA09F6C5D7E6DD6BCBEAA3B",Description="根据搜索条叫生成种子数据",Path="/api/sys-timer/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("3a8c73cf-89a2-4606-90c3-51dec0d80e1d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="锁定",Key="7696121FE473CFEED7A7CD1CB4A6B647",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/sys-timer/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("300ef305-2c03-44ad-bd4b-7ffa246530a9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="查询所有可以用的",Key="96C246A2C223E0CE16088CC1FD0D0E0A",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/sys-timer/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("7c10e9a1-d0c0-4930-b49a-8a71190ab42a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="查询所有",Key="AFF0461EE391D477DE158E15F62B6D79",Description="查找到所有数据",Path="/api/sys-timer/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("fee31eb9-c106-4e42-9464-0d2433fd4829"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="批量逻辑删除",Key="66DDF878D5F5ABFEF1EF618447F45A5B",Description="根据多个主键批量逻辑删除",Path="/api/sys-timer/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("99c24403-1417-4c04-b1ef-0c17243215e0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="批量删除",Key="AFD3A8A201452DB60D39E89FC7015C7D",Description="根据多个主键批量删除",Path="/api/sys-timer/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("b2ffcf41-7c74-4815-a367-d55c9a536b22"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="启动任务",Key="BB1ECD48F7FF479DC85870F66A467A38",Path="/api/sys-timer/start",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("588829d2-fae6-40cd-bdfa-c0758e7f89fb"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="停止任务",Key="918B9A40A48CA6481E5C039AB9DF8F28",Path="/api/sys-timer/stop",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("c96a611f-555b-4b96-8ee5-83a87ee03a6e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="查看任务",Key="2D7A312F51B40D39E3E8616B057A74A1",Path="/api/sys-timer/detail",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("94a19350-777d-4d29-8d84-2a9c6e1ae46d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="假删除任务",Key="2A47C77D4A33FEF2778D9729707BA5B1",Path="/api/sys-timer/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("b83c620b-e964-43bb-8590-d8d32277aa00"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="修改任务",Key="4AADC5D969F182119B00D77F9AB4D088",Path="/api/sys-timer",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("433d4ad9-7ae0-48ea-851e-c4e594c8e19a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="增加任务",Key="97761592D436CFF8E47FA6FD3C9DA300",Path="/api/sys-timer",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("4d664ef2-a462-494d-9c5c-453880f44017"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="获取所有本地任务",Key="842AFBBE14BB5C745BD820EF3C4A052B",Path="/api/sys-timer/local-jobs",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("c7aa66f0-6ceb-4cc7-b1cc-8d62163aa957"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="搜索",Key="B45DAF70F5971948EF52E6726269814D",Path="/api/sys-timer/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("a2504e15-4b43-4a6a-bc1a-9c06effa672c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="删除任务",Key="FDC1D135BD7B531A8B5DB65A2462450E",Path="/api/sys-timer/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("31e5a68d-916b-4b74-8e59-da733724b322"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="获取任务信息",Key="7CD8C319088D7195B2E9C236613DE833",Path="/api/sys-timer/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("81b4bb91-1f42-4043-9acb-dac756ce729b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="分页获取任务列表",Key="5F37C08165A82CACCDDE27447DE2D079",Path="/api/sys-timer/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("3fb4ab7d-dcab-482d-af48-3080e2b89d10"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="任务调度服务",Summary="导出",Key="A1BCA09FC8FF82506B93BFD5FCD6FCF5",Description="导出数据",Path="/api/sys-timer/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:25:20"),Id=Guid.Parse("ee8b1345-e0c0-47a7-9e85-cb0bd7ede472"),},
         };
        }
    }

}
