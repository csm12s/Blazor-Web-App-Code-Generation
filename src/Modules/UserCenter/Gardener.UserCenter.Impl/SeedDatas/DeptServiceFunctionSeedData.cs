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
using Gardener.Authentication.Enums;

namespace Gardener.UserCenter.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class DeptServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="用户中心服务",Service="部门服务",Summary="生成种子数据",Key="298FD49C905F1B1B812B226B95307CE0",Description="根据搜索条叫生成种子数据",Path="/api/dept/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("c2784668-075f-4b7e-a563-b6b92b072542"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="锁定",Key="8AED5C0B53588415D98E97119880AC6A",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/dept/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("0d2df690-6aa7-466b-b1e4-73fa4fda1b5d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="根据主键获取",Key="213D1BBDB567A74636ACE841D780F663",Description="根据主键查找一条数据",Path="/api/dept/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("2502e6ae-879b-4674-a557-cd7b4de891a7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="删除",Key="EA96F9C3B67BB0EB8E3D5337D3482162",Description="根据主键删除一条数据",Path="/api/dept/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("333edf31-c542-4fa1-baca-b770d558a4d7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="批量逻辑删除",Key="32ABBEA6610DE2420AC7B5E7FDAA315E",Description="根据多个主键批量逻辑删除",Path="/api/dept/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="查询所有",Key="0730ED2F37C050E4994609C45BE0C4A4",Description="查找到所有数据",Path="/api/dept/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("4e1a2966-bdfd-485a-b0cf-52004e40f6a7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="逻辑删除",Key="A5DA0BB6BEA388B99626E5A34BDE68F4",Description="根据主键逻辑删除",Path="/api/dept/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("5c8381ec-7e8a-4060-9c04-83032d18872c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="分页查询",Key="0BBAF9866F200FEDE526AB75E03319CC",Description="根据分页参数，分页获取数据",Path="/api/dept/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("7cb8921d-0a0c-4e80-8895-604c05480c43"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="搜索",Key="E6BAA5C7F35ED0CBD3902A30349A992B",Description="搜索数据",Path="/api/dept/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("85f94b4c-e897-4f3c-b80a-c7ddb8ebf1b5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="查询所有可以用的",Key="3D033D8178E68247D2C34E53F00D468F",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/dept/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("973edc2c-42e1-473e-9656-a43890663d8a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="获取所有部门数据，以树形结构返回",Key="6A85EF9D6FBD3B330E1827AB0949D7E4",Path="/api/dept/tree",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="更新",Key="248BF161E6BEB662D259298A8E564433",Description="更新一条数据",Path="/api/dept",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("e23b555c-600a-4839-9439-2ee0ad0ae4f8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="添加",Key="3AB1D0424907EC010DC69F029B4FBD06",Description="添加一条数据",Path="/api/dept",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="批量删除",Key="951D030BDA5FAE619E5A7BB9EFB43F33",Description="根据多个主键批量删除",Path="/api/dept/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("fcebd316-c2f3-4f8e-97fc-498dd3a33d4e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="获取种子数据",Key="EFFE9D726D7792B023DF91E15AA48C89",Path="/api/dept/seed-data",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("ff8621c9-1b88-4e6d-be00-34615c48c69f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="部门服务",Summary="获取所有部门数据，以树形结构返回",Key="98B6A43EC42A6B9F1D8257EE1D05E9BB",Path="/api/dept/tree/{includelocked}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:42:02"),Id=Guid.Parse("6e2ff941-1e79-4e2a-bbe9-ec68318e5d3a"),},

         };
        }

    }
}