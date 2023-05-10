// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class TenantManagerFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="用户中心服务",Service="租户服务",Summary="导出",Key="9880EDB9BD13EEB5B77C89EFBEE1CDAB",Description="导出数据",Path="/api/tenant/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("17388acc-2ef7-487d-bcef-fe4c6a708ecd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:47"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="生成种子数据",Key="746BEA6EC665547A8A7166B62497998B",Description="根据搜索条叫生成种子数据",Path="/api/tenant/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("0c1058f6-a425-40fe-bc5e-47fbf91ca7bd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:47"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="搜索",Key="5F3F0C211E6008478BC1B6AB08374C4C",Description="搜索数据",Path="/api/tenant/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("687b278e-aa4e-4d1c-95fd-f148eeb2a658"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:47"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="锁定",Key="6A181466880BA2B3A8B0BC66906ACA90",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/tenant/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("a9ec32a6-286b-4fe4-b69e-79cca6243e00"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:46"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="分页查询",Key="44CA7D257E0C34AD9066BBF6E6619523",Description="根据分页参数，分页获取数据",Path="/api/tenant/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("e5669b2a-a097-46c4-b5ed-aadff087cbae"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:46"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="查询所有可以用的",Key="7759AEBBAB92B29DD030D2D38CC6C2BC",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/tenant/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("cb4fbf67-4747-43d5-a355-7300f007cce3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:46"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="查询所有",Key="98F7C609E0FDCAC25D70E29A5DCFF83B",Description="查找到所有数据",Path="/api/tenant/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("0837b06f-e1d7-4ed4-9204-0c237aac6978"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:46"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="批量逻辑删除",Key="CB0773C970DC23647D09D3D9A51DA5F3",Description="根据多个主键批量逻辑删除",Path="/api/tenant/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("cbdf4c22-b54f-4943-b28e-2bb563720fa2"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:46"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="逻辑删除",Key="03522FF7125C61C2E7C1BD96FEA4E64C",Description="根据主键逻辑删除",Path="/api/tenant/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("a6939609-708f-4231-a726-6da1903ef69a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:46"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="批量删除",Key="B1885DC2864312A61C6427637A0E7FB4",Description="根据多个主键批量删除",Path="/api/tenant/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("189016af-1473-4107-9d91-67d25f5c4dfd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:46"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="根据主键获取",Key="723A65C5AE34149B05DCF8A128CBC74A",Description="根据主键查找一条数据",Path="/api/tenant/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("22f82d98-9176-4391-a950-1d1d01e105fd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:45"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="删除",Key="72760A5FE6E2516D899EF7C88273FEB1",Description="根据主键删除一条数据",Path="/api/tenant/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("82635595-f7c8-45af-9aaa-534e53e53ff5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:45"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="更新",Key="18C3543AD49B3D3CB78C696A636FB1D5",Description="更新一条数据",Path="/api/tenant",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("74a1a6b4-4559-48fc-8987-3287e5a4e056"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:45"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="用户中心服务",Service="租户服务",Summary="添加",Key="6C437FCA7A65CB18677DEAC79A73AC50",Description="添加一条数据",Path="/api/tenant",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("8db56ca8-b0e3-45c6-9c73-15893dd616f6"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-05-10 15:55:45"),CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }
}
