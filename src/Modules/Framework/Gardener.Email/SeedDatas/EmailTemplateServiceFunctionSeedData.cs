// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gardener.Enums;
using Gardener.Base.Entity;

namespace Gardener.Email.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailTemplateServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="生成种子数据",Key="53151648D7858D9061CC0D89B4EA43F5",Description="根据搜索条叫生成种子数据",Path="/api/email-template/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("75abfcbe-a00b-444f-baa6-503ae03b3434"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="搜索",Key="E4979CA111E299FA747D5A547C6E4A99",Description="搜索数据",Path="/api/email-template/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("2bf3ff67-c1a3-4426-8320-11839daa0a81"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="删除",Key="38B9A961DB74BD743A3B5D434B2EB66A",Description="根据主键删除一条数据",Path="/api/email-template/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("2bf807cd-7d48-40bd-839b-fdd71f419711"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="分页查询",Key="1B1FD29D8E0A4A89B600CAA46C82B02F",Description="根据分页参数，分页获取数据",Path="/api/email-template/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("2ea4faea-ec29-4383-833b-b5dedaa1b735"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="批量逻辑删除",Key="CBB8DE4E5D6DD206685DA33E90EF1EE1",Description="根据多个主键批量逻辑删除",Path="/api/email-template/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("31896c5d-2ed7-4e43-a952-4edc076d29d0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="更新",Key="CDA3DE9664774C06E0D86F62F2FCDDE2",Description="更新一条数据",Path="/api/email-template",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("33c2157a-884d-4030-abea-a9aeea51fdf8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="根据主键获取",Key="2E69B891C48D1F7E7974825E470447DC",Description="根据主键查找一条数据",Path="/api/email-template/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("3ac59980-d2df-4363-b8db-a4d043e362e7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="锁定",Key="18B1E82C7D5150FD3EDC3BB52FB3ACF9",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/email-template/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("736fd9b6-b56a-4860-8a1c-9a077be886e3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="逻辑删除",Key="C5F41046D8531C1E77B503560A7E220E",Description="根据主键逻辑删除",Path="/api/email-template/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("841c572c-5098-4e72-a590-2b81706aaa93"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="添加",Key="98410D052E1A609292E627692BFA3375",Description="添加一条数据",Path="/api/email-template",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("9191206c-f35e-4eb7-b19a-5949dc560369"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="查询所有可以用的",Key="5F7047AD7EC090D04B2AF8C4847678A8",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/email-template/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("aad857df-a1e7-43cb-be82-55c60865da86"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="查询所有",Key="B0F7A5E8F1984DD5545B50F04FB3106D",Description="查找到所有数据",Path="/api/email-template/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("ae3a97a9-32fb-4402-a6c7-9a0ffd76ce49"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件模板服务",Summary="批量删除",Key="9C103F0B6C167211465FB472E46EC968",Description="根据多个主键批量删除",Path="/api/email-template/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("b6c1592b-cb4b-4ead-bea1-3dc4a917e4a8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }



}
