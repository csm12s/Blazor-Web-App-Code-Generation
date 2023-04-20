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
using Gardener.Authentication.Enums;

namespace Gardener.Attachment.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class AttachmentServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="附件服务",Summary="生成种子数据",Key="6C10A6FBF3AD17499C371C48E0FEF6D6",Description="根据搜索条叫生成种子数据",Path="/api/attachment/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("542d6de4-1b2c-4820-8f8c-b6fa17c023aa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="上传附件",Key="3BF647BFC6987B8CEA91C97FEE17CC6D",Description="上传单个附件",Path="/api/attachment/upload",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="逻辑删除",Key="4A29177C50844829451B9ABBFA5DAFAC",Description="根据主键逻辑删除",Path="/api/attachment/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("070ae0e4-0193-4ce0-8ba6-b8c344086ced"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="批量逻辑删除",Key="17B55B877E0FB6704577EA356573BBC3",Description="根据多个主键批量逻辑删除",Path="/api/attachment/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("10190ac3-1092-49a9-8ad2-313454b40447"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="根据主键获取",Key="271DFDC5E142CFE1AF0C4200C6DC060A",Description="根据主键查找一条数据",Path="/api/attachment/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("2f820c7f-4f1c-4737-aae6-329585c75d92"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="查询所有可以用的",Key="6B7B11626AE0ABB28C5331DB67DACAA0",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/attachment/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("42b3486a-8ea0-4296-a526-7cd3ef9ea73a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="锁定",Key="ADF53A6D1C062BF2CC40EBDE20D8E841",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/attachment/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("5604fcc2-595f-4cc5-b0b8-c0d75a4c9351"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="搜索",Key="0C58B2617EA08ED81F14B53C00C678D7",Description="搜索数据",Path="/api/attachment/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="删除",Key="085CB1560C82B28FE4C8C5F28EA31A59",Description="根据主键删除",Path="/api/attachment/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("9c6cefe2-d57d-490c-8b0f-70749bc5cdfa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="查询所有",Key="3CBCB4608120758739D941BFCCC09C18",Description="查找到所有数据",Path="/api/attachment/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("a3ea9c9f-da6f-48e1-8255-d250bb3e52d5"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="分页查询",Key="9BE552AEF35878A71ABE8179B80AA036",Description="根据分页参数，分页获取数据",Path="/api/attachment/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("aedc9e9c-f011-4d46-966e-3b14fd5298c2"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="批量删除",Key="33524956F6EC6C08F348500B3E2D9E9C",Description="根据多个主键批量删除",Path="/api/attachment/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ef62671e-4d35-4993-83c4-4dcdf7cbf0d0"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="附件服务",Summary="获取我的某一类型附件数据",Key="A6BE53DBFEC19CB43380056A69B1037F",Description="{attachmentbusinesstype}",Path="/api/attachment/my-attachments/{attachmentbusinesstype}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("be639780-fbd0-4335-95e3-91e099d87850"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:52:36"),},
         };
        }
    }

}
