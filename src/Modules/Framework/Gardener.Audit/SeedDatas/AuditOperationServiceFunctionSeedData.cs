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
    public class AuditOperationServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="生成种子数据",Key="2F1D00EDA3F9BA770FC2D6E15892FBB4",Description="根据搜索条叫生成种子数据",Path="/api/audit-operation/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ca62cf90-fcfd-40aa-bd06-30afc7c6dd9f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:19"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="分页查询",Key="778BD549C3ACEF321ECEDF39C80241D0",Description="根据分页参数，分页获取数据",Path="/api/audit-operation/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("03c9956e-b832-4202-9c47-55ba3793f606"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:16"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="逻辑删除",Key="A20264B6A44D74DBF0C7990CF3FE6DC1",Description="根据主键逻辑删除",Path="/api/audit-operation/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("080dd200-8e8a-489c-86ca-8eb74c417c0b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:12"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="根据主键获取",Key="DA5651C09F319A1358B9948735712DCF",Description="根据主键查找一条数据",Path="/api/audit-operation/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("08d002b9-d320-4410-b9f3-7986ed87ece4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:10"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="根据操作审计ID获取数据审计",Key="26806445F59D861F9FDB9F91B164A1CD",Description="根据操作审计ID获取数据审计",Path="/api/audit-operation/{operationid}/audit-entity",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("1d994e50-d40a-465b-8445-646041a8131a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:06"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="查询所有可以用的",Key="A99DB9777E1C5C11D2FA6A8957F696E8",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/audit-operation/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("4a127124-6348-4db1-aa38-5f3af2c8efdf"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:15"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="批量逻辑删除",Key="C53E746377386D224D0941DB8F4CB539",Description="根据多个主键批量逻辑删除",Path="/api/audit-operation/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:13"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="查询所有",Key="D403D5F25D7ACA97A10BEF07B2A816F4",Description="查找到所有数据",Path="/api/audit-operation/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("7bb514a5-d62d-4ba1-a9b9-9e7756eaae2d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:14"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="添加",Key="07BC6868FFAD4A5B26193E2372B9821C",Description="添加一条数据",Path="/api/audit-operation",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("9513e5e1-37ab-4937-94f1-1f6b99a385f7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:07"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="更新",Key="7037CCD6F97FA35692ED560CE1756F86",Description="更新一条数据",Path="/api/audit-operation",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("a75bd9a7-e3f0-4736-9c27-8763a3d3768b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:08"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="批量删除",Key="1C8C95EA831A3D031460A1390DF26E83",Description="根据多个主键批量删除",Path="/api/audit-operation/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("a8211f75-bf19-459a-bf66-9c31c6f334aa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:11"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="搜索",Key="704D356B44E6DEA692BA099781A321DD",Description="搜索数据",Path="/api/audit-operation/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:18"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="锁定",Key="6999F8BBB5F9BA97658BB99113A381F5",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/audit-operation/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("eefdb20f-b508-415a-b798-1aa9420a5b62"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:17"),},
                new Function() {Group="系统基础服务",Service="审计操作服务",Summary="删除",Key="AD48018AF04E0A4573815675E555E98D",Description="根据主键删除一条数据",Path="/api/audit-operation/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("ffbd98b8-8945-4068-b70c-ea58b487bd25"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:00:09"),},
         };
        }
    }

}
