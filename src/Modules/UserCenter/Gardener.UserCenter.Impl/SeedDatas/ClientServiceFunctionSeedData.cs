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

namespace Gardener.UserCenter.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class ClientServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="生成种子数据",Key="80E175A5D68598258AE3022F6CD323F0",Description="根据搜索条叫生成种子数据",Path="/api/client/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("e81c2cc3-b2cb-4515-a5bb-b5ef3caa5050"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="刷新Token",Key="DF709DE63630893E744DA34D950EC7AE",Description="通过刷新token获取新的token",Path="/api/client/refresh-token",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("63b4ad68-3fc7-46e3-93c3-1a9b87e18a85"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="根据客户端编号获取绑定的接口列表",Key="BB9E3C06F2507147FADEA21712CB70CA",Path="/api/client/{id}/functions",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("3a6f74c2-0165-46b0-8cd5-1846846d97bc"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="逻辑删除",Key="EFC227D985161F0ED01B189C5CCF532F",Description="根据主键逻辑删除",Path="/api/client/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("037d2517-d1fa-4b5f-adba-a8f4aae6c205"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="登录",Key="6050B0AE0242E8D1D8A6B5B0EAFFA1E0",Description="登录接口",Path="/api/client/login",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("20b44c15-481f-4bba-8905-3e5f983927b0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="根据主键获取",Key="8B2F2030F705698FEA9D98536F415ADD",Description="根据主键查找一条数据",Path="/api/client/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("4c1b9201-09e6-421f-95d1-d98d009a3417"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="批量删除",Key="678276BC3559FA79B62455965C7229B8",Description="根据多个主键批量删除",Path="/api/client/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("5d67bd9d-853c-4e16-973d-be0511241fc0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="添加",Key="8ECC90D5D58B7FD57A1D06C0F5C4CECA",Description="添加一条数据",Path="/api/client",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("8172d258-7a75-4ced-b5e2-b0be7350aa1f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="搜索",Key="DA7F00498254B5B31B18D7C877F96FB7",Description="搜索数据",Path="/api/client/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="分页查询",Key="A16E0CAA03E75A172F6A782E8BB86ECC",Description="根据分页参数，分页获取数据",Path="/api/client/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("8f0fb7b6-9087-40c3-a894-8be057ac044e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="删除",Key="E749E69D854D694E0BC4CD4D97142A49",Description="根据主键删除一条数据",Path="/api/client/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("af1f0410-e9cc-4a73-9da7-ea45aadac8b2"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="批量逻辑删除",Key="1DC4817A750A7C248B15EA766BDD53C8",Description="根据多个主键批量逻辑删除",Path="/api/client/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("cc73d556-6ded-4a2a-8b5c-62ea9c897351"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="更新",Key="4326F39D4D047A58AA7887EEB0A5B5A3",Description="更新一条数据",Path="/api/client",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("cecdfb7d-6796-4bd8-a3d7-164c16a7c959"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="查询所有",Key="C75F9424DD51498CD9ADBFCBF2EB4D57",Description="查找到所有数据",Path="/api/client/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f16b30b9-9e03-48d7-83a1-f09ae3e05345"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="查询所有可以用的",Key="6069031816C15D92B60A246C9CAD1287",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/client/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("f72f5e71-46f6-44eb-8a3d-f07082fa33e5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端服务",Summary="锁定",Key="6D9355E642310F188E728A62002A6879",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/client/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("fc90ab49-b7c2-437e-bbdc-4f234cb0f79a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }

    }
}