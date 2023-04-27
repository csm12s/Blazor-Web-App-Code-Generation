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
namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class CodeManagerFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="根据字典类型值获取字典列表",Key="F1EB36795F9ABEFB53058F8A1B80E294",Description="根据字典类型值获取字典列表",Path="/api/code-type/{codetypevalue}/codes-by-value",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("70dd5b33-585d-4ed4-83b7-4e55f62298d1"),IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-26 14:11:13"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="根据多个字典类型编号获取所有字典的结果",Key="4B5955C39899A574748AEC9472EAD3DF",Description="根据多个字典类型编号获取所有字典的结果",Path="/api/code-type/code-dic-by-values",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("09d15841-71c7-4afc-99f8-dd906a0248d9"),IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-26 14:11:12"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="刷新字典工具缓存",Key="84729CE5CCEDE2B91418572851547D7A",Description="refresh-code-util-cache",Path="/api/code-type/refresh-code-util-cache",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("332d731a-e209-48da-84e9-508288efbf22"),IsLocked=false,IsDeleted=false,CreateBy="6",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-26 14:10:30"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:13"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="根据多个字典类型编号获取所有字典的结果",Key="79DE9C21EF2535EFF7ACDCFAD9E9F2E9",Description="根据多个字典类型编号获取所有字典的结果",Path="/api/code-type/code-dic",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("4e85ec32-8a3c-46e9-ba60-4abd7bee6745"),IsLocked=false,IsDeleted=false,CreateBy="2",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:02:29"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:12"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="根据字典类型编号获取字典列表",Key="2FEBEEAEEB7FDD3630AD1411F97E27D2",Description="根据字典类型编号获取字典列表",Path="/api/code-type/{codetypeid}/codes",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("9a7bf3a2-802b-43b8-817f-f021a75d437f"),IsLocked=false,IsDeleted=false,CreateBy="2",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:02:29"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:12"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="导出",Key="B0112E9E5DE1B2C88091175412D6BF7D",Description="导出数据",Path="/api/code-type/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("7faa6bc4-385b-4cdd-ad8b-4e1673fc893b"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:51"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:16"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="生成种子数据",Key="304CB45DB11EEBFDDE5E493F9BADE678",Description="根据搜索条叫生成种子数据",Path="/api/code-type/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("2fe5043f-fd43-45f7-a60b-e76865778c2a"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:50"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:15"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="搜索",Key="79EB05031A70D307DD782CED65E24E0F",Description="搜索数据",Path="/api/code-type/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("810235c8-01f5-4255-a30c-fd77ffeb6eab"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:50"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:15"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="锁定",Key="67BAAA6E6062B824EFCBADF73BCB1587",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/code-type/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("269fe59d-473c-44c5-aadf-54553cb5e059"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:49"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:15"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="分页查询",Key="0D74D8B03DB1CD915D18B06365A77258",Description="根据分页参数，分页获取数据",Path="/api/code-type/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b85cabd4-f813-4077-833d-152fb9c3c2b7"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:49"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:15"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="查询所有可以用的",Key="C8E56A2E79C8B64D478D2148908166C7",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/code-type/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:49"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:14"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="查询所有",Key="E326F4D7B7330B9E249A6BEBEF7C6E59",Description="查找到所有数据",Path="/api/code-type/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("428421b4-bb3e-49c9-91ce-74da66d3729b"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:48"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:14"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="批量逻辑删除",Key="E08C5E40CD258CEC1CBC279A8A56E26F",Description="根据多个主键批量逻辑删除",Path="/api/code-type/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("56871df3-2984-4d02-8ff7-61101f0abc19"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:48"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:14"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="逻辑删除",Key="730019C5993BDE609E6596851BA232F6",Description="根据主键逻辑删除",Path="/api/code-type/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("e242137a-b4ea-4dd9-9692-63885fce93b8"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:48"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:14"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="根据主键获取",Key="29DF672B679D6B4BACD18812F0527DE4",Description="根据主键查找一条数据",Path="/api/code-type/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a28ac095-5faa-4a34-bf4e-5c3d3ab7cac9"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:47"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:13"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="批量删除",Key="D9A20DEED6CAF38F963F938912CA544C",Description="根据多个主键批量删除",Path="/api/code-type/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("b69cedcd-49c3-4833-9d80-d67282cd015f"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:47"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:14"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="删除",Key="9965590775432EAD1C790DB346C5091A",Description="根据主键删除一条数据",Path="/api/code-type/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("f3ca4074-503c-49a5-b714-03b121f25e9e"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:47"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:13"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="添加",Key="34F97902F9A851C8D126DB33A4C0C0BD",Description="添加一条数据",Path="/api/code-type",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("1f164c01-cd48-4b41-8c87-0cd7e5dc1973"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:46"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:13"),},
                new Function() {Group="系统基础服务",Service="字典类型管理",Summary="更新",Key="4E8341596E72B28095CC42F5E4D4A614",Description="更新一条数据",Path="/api/code-type",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("4c206f6e-5e3a-4c96-b48c-81d6df44c9af"),IsLocked=false,IsDeleted=false,CreateBy="1",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:46"),UpdatedTime=DateTimeOffset.Parse("2023-04-26 14:11:13"),},new Function() {Group="系统基础服务",Service="字典管理",Summary="导出",Key="B802BC3519436726D4D39F9D6C8E0F72",Description="导出数据",Path="/api/code/export",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("0bc4b901-d9ee-4fac-91c7-e6ea6d6fb852"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:46"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="生成种子数据",Key="95D3D2AECB7BA05B1D523003576DBA80",Description="根据搜索条叫生成种子数据",Path="/api/code/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("f9480a7b-3408-44f0-aca9-78ba66a75799"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:45"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="搜索",Key="874CCECCA57B5B0C884223B9B77FB6CA",Description="搜索数据",Path="/api/code/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("213eb6ca-e125-4181-b5bd-3e5666d3e8c9"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:45"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="锁定",Key="298247EAC149319EC01AD1B2C3280CBC",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/code/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("b3f529a4-ad25-4d14-a86b-c1f4cd3f33e0"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:45"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="分页查询",Key="FE1D0FCB38385E82E9DBE4111184F797",Description="根据分页参数，分页获取数据",Path="/api/code/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("0ffffde0-ddba-4632-b083-e191e3f71b36"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:44"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="查询所有可以用的",Key="CD838C782BF38C679F6017D29A5075B1",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/code/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("06b69fae-5d2a-48c4-883a-aff7f1015661"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:44"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="查询所有",Key="64C74ADD81A7CEDA017064A06CDE4792",Description="查找到所有数据",Path="/api/code/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a1805308-f3d5-4aa2-ba20-26015da551bd"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:44"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="批量逻辑删除",Key="5D8485FD1F4D6A5A7924C93928BB3223",Description="根据多个主键批量逻辑删除",Path="/api/code/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("84a87eb4-7ba2-4623-ad0d-ea467e7e5434"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:43"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="逻辑删除",Key="E4E1DDA22B1458D2372BEE9FB3ED124A",Description="根据主键逻辑删除",Path="/api/code/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("595922c1-5099-4c6f-946f-ed3a7af7d7d3"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:43"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="批量删除",Key="7B25BA78214DA9C788A6A0A32E41AEEE",Description="根据多个主键批量删除",Path="/api/code/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("28f6b60c-e528-4fd7-9ce9-b1c87452514b"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:43"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="根据主键获取",Key="B732D3822802617A15E93D2BC1872E8D",Description="根据主键查找一条数据",Path="/api/code/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("483956b8-4651-4dea-8c34-501108220cc1"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:42"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="删除",Key="E0B6AFB00AB714406B8E6EB6A1C54C28",Description="根据主键删除一条数据",Path="/api/code/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("69d12a63-87a3-48bf-b246-86acd30697f2"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:42"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="更新",Key="097F8DBD6CD33EA9DB3976E17A448E17",Description="更新一条数据",Path="/api/code",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("ce5a43ff-7ee1-451b-ae23-8acd04f87f71"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:42"),},
                new Function() {Group="系统基础服务",Service="字典管理",Summary="添加",Key="F53D637DD7CC01612737CC2F878A95AA",Description="添加一条数据",Path="/api/code",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("06eff7a6-4291-43b0-9c34-58a97aa0fc01"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:26:41"),},
         };
        }
    }

}
