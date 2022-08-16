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

namespace Gardener.Audit.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AuditEntityServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="生成种子数据",Key="25F7A33EC2479E4589E5A540765C3DA0",Description="根据搜索条叫生成种子数据",Path="/api/audit-entity/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("724e4ba8-59ff-458a-a940-325f973827d0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="查询所有",Key="E46343E17F6F09D2DD0BB1B6C78C81F6",Description="查找到所有数据",Path="/api/audit-entity/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("045945e7-94c4-4727-8392-31fc9d99cd9f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="批量删除",Key="13457B9CA71646A02E6F004CE877A0E6",Description="根据多个主键批量删除",Path="/api/audit-entity/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("0f372dde-1e65-441a-b002-eee8b2e1a1f9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="批量逻辑删除",Key="87F7F066F0A0605D1DB5CE8B7286E0CB",Description="根据多个主键批量逻辑删除",Path="/api/audit-entity/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="查询所有可以用的",Key="280713DC4618277C7BF307117835ED7B",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/audit-entity/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("1fe857c9-c027-4ca3-b8f8-21ec2c1f5cde"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="逻辑删除",Key="30759F98C0CF4A34813C280451C2E4CF",Description="根据主键逻辑删除",Path="/api/audit-entity/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("46aef5bc-9d0f-4a05-b21d-747753b98569"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="添加",Key="44405A33B9DEC6F934920AF5AC6F7111",Description="添加一条数据",Path="/api/audit-entity",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("475207d6-4c0b-4054-a051-7315295694a1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="分页查询",Key="72DE329278C26111EB3F431ACB89B0A4",Description="根据分页参数，分页获取数据",Path="/api/audit-entity/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("4b7e7f68-8925-4b5c-b8d2-8a51df917b0c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="删除",Key="CCD570BA5C66619052354D738927A007",Description="根据主键删除一条数据",Path="/api/audit-entity/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("7e5577d4-32b2-4f43-a83f-05410b59b195"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="搜索",Key="5A7181978F26890284CE44ED28A2F7AA",Description="搜索数据",Path="/api/audit-entity/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="更新",Key="DE9B14A3BC0E0653399F870F27F24CEF",Description="更新一条数据",Path="/api/audit-entity",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("9d26c715-9b8b-40c6-bbf4-9c51df1193da"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="锁定",Key="882FEEBFEAF1F50D83E0189AA69B9ED0",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/audit-entity/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("aed3a535-b700-48a5-a8f5-3657e500e400"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="审计数据服务",Summary="根据主键获取",Key="A46663EE883A6E5BE0A0C8FE0B3D7A4C",Description="根据主键查找一条数据",Path="/api/audit-entity/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b3577dc2-dfea-41be-ba8f-bb8efa389f36"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }

}
