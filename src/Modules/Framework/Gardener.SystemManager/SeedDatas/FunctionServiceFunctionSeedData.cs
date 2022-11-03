// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Domains;
using Microsoft.EntityFrameworkCore;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class FunctionServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="功能服务",Summary="生成种子数据",Key="A53340931409D1BB2882CDB88AE6CB5D",Description="根据搜索条叫生成种子数据",Path="/api/function/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("c4cc2526-8403-4e6c-a88b-94e55279eaa3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="启用或禁用",Key="1DC1B5ECD34759A80CE8C468366A378F",Description="启用或禁用功能",Path="/api/function/{id}/enable-audit/{enableaudit}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("056ff2f6-009b-40ff-a1b9-a6983e471967"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="删除",Key="097D7A323BFFCA32788EAA8C6BDB5157",Description="根据主键删除一条数据",Path="/api/function/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("2a670df1-f01c-4cdb-b084-a46fdb339ced"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="搜索",Key="9A316E9E6A41D1F57870A5F0CDDC93EF",Description="搜索数据",Path="/api/function/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="逻辑删除",Key="A401312992835BA902C0CFDC5FEEE1F3",Description="根据主键逻辑删除",Path="/api/function/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("4b57474a-88b4-4393-bb49-4b59e8c3c41d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="分页查询",Key="67C405B4CBCC02144945800F26CC1F4F",Description="根据分页参数，分页获取数据",Path="/api/function/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("590cd04c-025c-4cc1-bdd1-e9cea201bb46"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="添加",Key="3F9869D1A16CD359E268F2C2DBEFD0E2",Description="添加一条数据",Path="/api/function",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("84256e5b-2cef-4b16-8fd3-79ff8d47c731"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="锁定",Key="E7F5596D4D8517C85871566D8EFA0855",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/function/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("8ae9c253-584e-46e4-b805-6ec90281d6dd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="批量删除",Key="717D6057E652BA28D3BF0CE337180E9E",Description="根据多个主键批量删除",Path="/api/function/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("8f1c2eeb-248f-41bb-a083-511664f2fd8e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="查询所有",Key="488BDECFA97ADDE5E940446C32C42693",Description="查找到所有数据",Path="/api/function/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("9caf800a-de55-4d59-a138-675a16924c3c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="判断是否存在",Key="27693C4354A64289D9A1D3EB50E68E7E",Description="根据 HttpMethod 和 path 判断是否存在",Path="/api/function/exists/{method}/{path}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a15ce231-80ae-46c6-ada8-49666e81e328"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="更新",Key="E2248234B183BA3EEA82273CB03F500C",Description="更新一条数据",Path="/api/function",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="根据key获取",Key="CE34F0B4FCB2222CF693F501B370149D",Description="根据key获取 功能点",Path="/api/function/by-key/{key}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b2dfaad3-e44a-4a76-ac91-34a571ba47e8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="查询所有可以用的",Key="1CCC2478B5AC5FDDB537DCD33166ABF7",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/function/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b79d2f63-487c-44c8-b7d3-1e882994789b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="根据主键获取",Key="FDD3EAB18820A6CD5C6DA3B17D40EEB9",Description="根据主键查找一条数据",Path="/api/function/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="批量逻辑删除",Key="4603BCE62CA130E67C2450C127DD7728",Description="根据多个主键批量逻辑删除",Path="/api/function/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="功能服务",Summary="导出",Key="C159134CE2D63BC05680B2AD2BB86E7C",Description="导出数据",Path="/api/function/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:25:20"),Id=Guid.Parse("dca2b115-3363-4f7f-8bba-b051b8d8603a"),},
         };
        }
    }

}
