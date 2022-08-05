// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Gardener.Base.Domains;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.NotificationSystem.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AnnouncementServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="通知系统服务",Service="公告服务",Summary="生成种子数据",Key="DB4E6D22B47A0BBCB5F87643AA5EB527",Description="根据搜索条叫生成种子数据",Path="/api/announcement/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("f7279175-4aa3-448a-ac71-a17004d66788"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="搜索",Key="9AD6BA02957A5D79C763F37FC7350C1F",Description="搜索数据",Path="/api/announcement/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("25bad725-529b-4a67-814a-1a6171a4b6d1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="锁定",Key="F5A7F4A1B3C633F14D21BE37F2D8F7FC",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/announcement/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("7da66506-ed83-40ec-97ad-5323e36af404"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="分页查询",Key="3034444DADCC535B882E3D20DA9E1904",Description="根据分页参数，分页获取数据",Path="/api/announcement/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("7cad69bf-2f23-44e8-b0ef-97bdc57fc6a4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="查询所有可以用的",Key="7DAD544022ECAB407CA07965FBDEC6AB",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/announcement/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("841a3afa-a128-4751-b3b2-b2849da338e1"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="查询所有",Key="54C22639E8EF99A5CEEC744853C5DFCD",Description="查找到所有数据",Path="/api/announcement/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a4e467c5-639c-40bf-a71c-7d3c0d0760e7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="批量逻辑删除",Key="6A55E8C4030728438432973ECACF7433",Description="根据多个主键批量逻辑删除",Path="/api/announcement/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("faa3ff98-22d5-4254-9297-ee976a5842de"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="逻辑删除",Key="9D50105E0F127FF7F9C12C9EC2643787",Description="根据主键逻辑删除",Path="/api/announcement/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("3c68f73b-5a83-4429-9046-4fe33473739f"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="批量删除",Key="FF53BAAC9AC941B38C99A08E032B9443",Description="根据多个主键批量删除",Path="/api/announcement/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ac3ae978-83b7-4fad-9322-d1e223618d7c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="根据主键获取",Key="336B54D6E3393F56F6C35FCA416A3EE5",Description="根据主键查找一条数据",Path="/api/announcement/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("0d899b61-e2ba-4d0d-b2fd-83dad377ed78"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="删除",Key="411B20D0DAA265D807874613A8DCB9F9",Description="根据主键删除一条数据",Path="/api/announcement/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("a2eab26f-f15c-48be-a976-2411c18f42bf"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="更新",Key="5C0F247E2ABB90F962DCB4E0D1F948E1",Description="更新一条数据",Path="/api/announcement",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("ff955e68-22f5-47c2-88f2-2c901cd823e3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
                new Function() {Group="通知系统服务",Service="公告服务",Summary="添加",Key="EDE2920EEFF4D581ED8EFB72359C19F5",Description="添加一条数据",Path="/api/announcement",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("9bda79c9-783c-469c-acda-b72be7391a82"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,},
         };
        }
    }


}
