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

namespace Gardener.Email.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailServerConfigServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="生成种子数据",Key="7F4170917F4566615005DB297A93C7CE",Description="根据搜索条叫生成种子数据",Path="/api/email-server-config/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("bacd1963-f89e-4afb-862f-584cd9ba4c10"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="查询所有可以用的",Key="4548CF61B82D6B6ED737DE1D568D5E7B",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/email-server-config/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("1d325e63-3e9e-4cbc-b275-00a057c71e63"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="添加",Key="DD71B200E8B3E6E24BD6F9C05E3D666C",Description="添加一条数据",Path="/api/email-server-config",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("1ef3b8a8-6e46-49d7-9a7e-f63137beaade"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="批量逻辑删除",Key="006AC2DA9C0126A631FE4092AAB706C0",Description="根据多个主键批量逻辑删除",Path="/api/email-server-config/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("26d95428-ebbd-4bf2-9bcc-2eeec4263bd5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="逻辑删除",Key="FB316294679817930CABB93BE346C453",Description="根据主键逻辑删除",Path="/api/email-server-config/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("39421a19-9cbf-477b-baea-34f40341357f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="批量删除",Key="4FBBC2EF99F020CC28878731394CF303",Description="根据多个主键批量删除",Path="/api/email-server-config/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("3ed89bcc-7eb1-4b51-86a5-dbe449370e1b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="锁定",Key="C0F3F05AE24A0E8BBA9BAF52852E09D4",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/email-server-config/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("6c9aa43e-921c-44bc-83fb-64a9c451255f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="删除",Key="BA79EB71501051CA1F082DE15FBE73D3",Description="根据主键删除一条数据",Path="/api/email-server-config/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("84247930-2035-443d-bde3-69d4d23bec85"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="更新",Key="D15206E2B2CEFD1CC520AF32A357F56E",Description="更新一条数据",Path="/api/email-server-config",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("99546746-70b8-42d6-884d-ea1b79f88c0a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="根据主键获取",Key="4166A00DD3058EA57C09B869E68927D4",Description="根据主键查找一条数据",Path="/api/email-server-config/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("9d25bf25-5470-4fed-b58c-c4ef4339d533"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="搜索",Key="87C5CB00FB6A44D52C1C4CC5E9312B02",Description="搜索数据",Path="/api/email-server-config/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("9fe5cc45-a851-4d3f-8b44-32dd96130946"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="分页查询",Key="6946DB3F24E403A804F67F2B116C9392",Description="根据分页参数，分页获取数据",Path="/api/email-server-config/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("d5e9621c-ad9f-4bca-aa51-04aa0b55744e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件服务器配置服务",Summary="查询所有",Key="5AE5AD783D5821E6D300DBE1BDD6E631",Description="查找到所有数据",Path="/api/email-server-config/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("d7fa048a-0bfd-4997-94e3-dda3402c3b08"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }


}
