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
    public class UserServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="用户中心服务",Service="用户服务",Summary="生成种子数据",Key="071E85AC46B630CFCC89C5EAF1E23F68",Description="根据搜索条叫生成种子数据",Path="/api/user/generate-seed-data",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("a6db8946-339f-423e-8641-902da36d3d39"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:57"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="逻辑删除",Key="02836036DDDF7900E5F5E9762F5E4229",Description="根据主键逻辑删除",Path="/api/user/fake-delete/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("0b605fe1-c77c-4735-8320-b8f400163ac9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:55"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="更新头像",Key="FEBD6097BE29268FDFDC295C98A9AD9F",Description="更新用户的头像",Path="/api/user/avatar",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("0c6f2138-e984-4fba-ad2a-2890716a7259"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:55"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="设置角色",Key="A843DEF0CDD97A394996DCF7C5E80F5B",Description="给用户设置角色",Path="/api/user/{userid}/role",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("0d2e0194-2238-457b-aab0-9b3259cc4ed9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:55"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="查询所有",Key="9B0AD48E75A6C37EDC7101236F93CF77",Description="查找到所有数据",Path="/api/user/all",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("1562071d-e18c-4d29-a854-12a562961140"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:56"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="删除",Key="FBAC1FD6280B05C7EAFD6BD24F0DE077",Description="根据主键删除一条数据",Path="/api/user/{id}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("2428c3c3-740e-45fc-9047-5a2be3c9cd70"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:55"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="批量删除",Key="5C9E8B48C5C77A0CEB8E6A853D56A808",Description="根据多个主键批量删除",Path="/api/user/deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("3402d3b2-cf24-4634-a65c-534f96e2991a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:55"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="查看用户角色",Key="652940681CC97C52299C95242AB1E858",Description="查看用户角色",Path="/api/user/{userid}/roles",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("3790cc0d-dc3a-4669-acba-3a90812c6386"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:53"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="查看用户权限",Key="FAA3B104E6EBF3B5F16DB92C56836A63",Description="查看用户权限",Path="/api/user/{userid}/resources",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("39ccceae-2cba-4cd2-a44b-fc8fe8a3f2e4"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:54"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="分页查询",Key="5FFF46E52DE5943FA225B0F6E29A338D",Description="根据分页参数，分页获取数据",Path="/api/user/page/{pageindex}/{pagesize}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("3e2f4464-6b69-4a00-acfb-d39184729cdd"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:56"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="锁定",Key="718DFD76BA4C2997D3DDA216BDB98369",Description="根据主键锁定或解锁数据（必须有IsLock才能生效）",Path="/api/user/{id}/lock/{islocked}",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("622c1a11-7dff-4318-9d21-b57fbd1da9ba"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:56"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="批量逻辑删除",Key="3E010DCA7BAD6C3FCCCA32FB77F050F0",Description="根据多个主键批量逻辑删除",Path="/api/user/fake-deletes",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("6aea8a77-edd2-444b-b8be-901d78321a49"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:56"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="更新",Key="8C82B0DF3A0F5EB8DFED7794B16DA9A5",Description="更新用户",Path="/api/user",Method=Enum.Parse<HttpMethod>("PUT"),EnableAudit=true,Id=Guid.Parse("9ebd4172-5191-4931-9b22-4c339be4a816"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:54"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="新增",Key="7CBF6D43C3F9935BF83629FCEED2FFFB",Description="新增用户",Path="/api/user",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("af79d7de-0141-4338-8c52-05216d1b07ff"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:54"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="根据主键获取用户",Key="011AC4559477AB1F24A281BDC1033AAB",Description="根据主键获取用户",Path="/api/user/{id}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:54"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="查询所有可以用的",Key="073E6E78B3A88E41DBDC46DCA32C4837",Description="查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)",Path="/api/user/all-usable",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("bdab8953-956d-4b1a-945b-b1806e9ac749"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:56"),},
                new Function() {Group="用户中心服务",Service="用户服务",Summary="搜索",Key="04608E487B494D4597BBAD83DF59D2FF",Description="搜索用户数据",Path="/api/user/search",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 18:58:54"),},
         };
        }

    }
}