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

namespace Gardener.UserCenter.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class RoleServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="用户中心服务",Service="角色服务",Summary="生成种子数据",Key="FD2CAFBFF34B435DF026315EF4D89CC5",Description="根据搜索条叫生成种子数据",Path="/api/role/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("0b7a9ed1-86cc-42a6-a260-f7ba33054054"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:53"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="更新",Key="3005F52703299DD4885D51C80CA3B370",Description="更新一条数据",Path="/api/role",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("01944b79-bfe5-4304-ade0-9c66e038d5d4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:51"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="添加",Key="F57997ED31483BE396EB71C98D07B6F5",Description="添加一条数据",Path="/api/role",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("16517409-c055-447b-8e91-7155537c6d15"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:51"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="逻辑删除",Key="BBF7B9CA0FE646DBAE2923B70DA8A7A4",Description="根据主键逻辑删除",Path="/api/role/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("2c3ec3c9-76c7-4d29-953f-e7430f22577b"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:52"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="批量删除",Key="91B03FFD3080A9684592C45A15C826A5",Description="根据多个主键批量删除",Path="/api/role/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:52"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="分配权限",Key="2BBD7196A51542F56FAC25FF3D760D21",Description="分配权限（重置）",Path="/api/role/{roleid}/resource",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("38c69230-1ed0-413e-9ae6-05bc1ef989e0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:50"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="搜索",Key="4B11C588FC856C862E41859F189370C0",Description="搜索数据",Path="/api/role/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:53"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="批量逻辑删除",Key="F19E71A217BEEADDD5EF20B65D93439E",Description="根据多个主键批量逻辑删除",Path="/api/role/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("5e8adf52-8db2-4d56-9ff3-003cae13e0aa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:52"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="查询所有可以用的",Key="D4F99E0AE4263D647F3440B66DB7AC7B",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/role/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("5efd6ab4-a9d3-4742-9a48-fb54a1b1e463"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:53"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="获取种子数据",Key="72B515FB99A1EFE42DEFCFC12954F93D",Description="获取种子数据",Path="/api/role/role-resource-seed-data",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("63d7208e-45d3-406e-a4a1-c87e3afda04d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:51"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="锁定",Key="439ED218846E25C27A388B09904AABC8",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/role/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("868fc0df-7cdf-4b56-873e-16dd3e0aa528"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:53"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="根据角色编号删除所有资源",Key="DECA4ECA67D27FC9932271EE3B0AC5DD",Description="根据角色编号删除所有资源",Path="/api/role/{roleid}/resource",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("94f22c97-ae4a-40e0-95cd-d0a6347eacd7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:50"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="分页查询",Key="5CF48BAB60B771300975D93C49925CA0",Description="根据分页参数，分页获取数据",Path="/api/role/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("9d9233d8-df0a-43b7-929a-65b9bd532c8c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:53"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="删除",Key="1A6C9AC4F4D71B0FC154AD8CE6FE6D29",Description="根据主键删除一条数据",Path="/api/role/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("a4a2536b-1cc6-438c-ba00-054e16fc2c7c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:51"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="查询所有",Key="8D8980AD32B8E49FB140F9DCE14B897C",Description="查找到所有数据",Path="/api/role/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("cb9f6387-5817-4fd6-b9eb-6553dcaf5e87"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:52"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="根据主键获取",Key="CC8DA87E574A106E9B14287FEC850037",Description="根据主键查找一条数据",Path="/api/role/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("cba739f0-9f8a-40c2-afff-d66c3382e096"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:52"),},
                new Function() {Group="用户中心服务",Service="角色服务",Summary="获取角色所有资源",Key="011A2E3F574F9C151E044EFA80A05F29",Description="获取角色所有资源",Path="/api/role/{roleid}/resource",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("cd7db809-50f5-4bf3-a464-89218e24077f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:56:51"),},
         };
        }

    }
}