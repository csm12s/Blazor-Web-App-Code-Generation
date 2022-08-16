// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Domains;
using Microsoft.EntityFrameworkCore;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="资源服务",Summary="生成种子数据",Key="74B1FF2C0E45DDB2D649404A53E7F7E9",Description="根据搜索条叫生成种子数据",Path="/api/resource/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("910d2a4f-85ae-46ff-bddd-b65ffcc6b9e1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="添加资源",Key="56C72854CD92865B84133D0D791DEC22",Description="添加资源",Path="/api/resource",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="根据主键获取",Key="6E9E5AA61727C2BD1E4142F0ED0F9DC5",Description="根据主键查找一条数据",Path="/api/resource/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="搜索",Key="24C5B533C5DDC7D494830FF5E28F6EC2",Description="搜索数据",Path="/api/resource/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("1295aed2-ae71-411f-9542-d50f75432840"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="返回根节点",Key="34CFCB2759472E91321739C5D43B00D0",Description="返回根节点资源",Path="/api/resource/root",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("424fd96a-a889-4ff9-910a-25a59204d2ec"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="更新",Key="CE39C474540DD96EAF373115B164EDC7",Description="更新一条数据",Path="/api/resource",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("45dd0581-3394-4c0a-bb8e-c9e0074d5611"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="查询所有",Key="7971E7E4FDCB5CBA6EE06E7DFE3F199E",Description="查找到所有数据",Path="/api/resource/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("4d51608e-5988-4d3d-8f5e-00e0c0c07b02"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="批量删除",Key="F74128AC93B49FC04CB29781E17E5302",Description="根据多个主键批量删除",Path="/api/resource/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("5eb48cf2-6c45-47c2-a68b-84284a389c69"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="查询所有可以用的",Key="6C03A3540C36BB4BD1BB9F1606F0F550",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/resource/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("7229563b-7311-41b8-947b-f07d58fa6c87"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="批量逻辑删除",Key="9C888321143AC3E991B72D3B32193A35",Description="根据多个主键批量逻辑删除",Path="/api/resource/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="获取所有子资源",Key="C5668FD7C42E9FB532AB9CB2E1480E1F",Description="获取所有子资源",Path="/api/resource/{id}/children",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("814304bb-22fe-4a33-82e1-8ad7c64bab4a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},               
                new Function() {Group="系统基础服务",Service="资源服务",Summary="锁定",Key="BC8D1127FE54019A5476079400388CF3",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/resource/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("cdd3c605-ed1d-4d94-a482-16430b729541"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="查询所有资源",Key="6AFF14D9D209CDEEFFC0E4872E060F42",Description="查询所有资源 按树形结构返回",Path="/api/resource/tree",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="逻辑删除",Key="FEB756C21615385FC3C747ACB240DC2D",Description="根据主键逻辑删除",Path="/api/resource/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("e7e8c401-2ff1-45ee-adfd-cebe90117575"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="删除",Key="8650A7797FF354BBB742C87D0F560844",Description="根据主键删除一条数据",Path="/api/resource/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("f4ba1bf6-c07e-4df2-b7de-93b35fb79bf0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="分页查询",Key="5848947AEE0064BC746DE38E1AC0E3D2",Description="根据分页参数，分页获取数据",Path="/api/resource/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f8ddd5e5-7c20-43c2-a2cf-31ebc3f9971a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="资源服务",Summary="根据资源id获取功能信息",Key="B2A11324BCA0A9070B6160AE6B0EE6F2",Path="/api/resource/{id}/functions",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }


}
