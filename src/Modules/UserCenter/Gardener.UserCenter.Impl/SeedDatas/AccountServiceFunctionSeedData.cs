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
    public class AccountServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="刷新Token",Key="1549F5F1C34E25281CBC00CA283BC404",Description="通过刷新token获取新的token",Path="/api/account/refresh-token",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("03ee6f4b-dfea-4803-9515-3a9b2f907c90"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:30"),},
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="移除当前用户token",Key="34925D025D1D97104B7A51EF41C393F3",Description="移除当前用户token",Path="/api/account/current-user-refresh-token",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("1c6dfb26-4149-4fa3-a7de-083ad7ff7d6c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:31"),},
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="获取当前用户信息",Key="2FAAF199BA16D914E7796C0B65B7CD13",Description="获取当前用户信息",Path="/api/account/current-user",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("38545a67-61ff-4e5c-90bb-a555a93fcbea"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:33"),},
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="查看用户角色",Key="7F3E99BDC443556613552A21A56D9B73",Description="查看当前用户角色",Path="/api/account/current-user-roles",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("68ce42ff-acc7-485f-bc91-df471b520be7"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:32"),},
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="获取用户资源",Key="3F2A1F37C00070D6D3EB4F27E24BB687",Path="/api/account/current-user-resources",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("713341f2-47e1-42af-b717-bfa75904d32e"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:34"),},
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="获取用户资源的key",Key="0AC55E6880AE8FACEBACB093AF914C65",Path="/api/account/current-user-resource-keys",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("89a06a4e-1a8e-41aa-a443-fd11bcc8497d"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:35"),},
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="登录",Key="B6792454A69F875EEC82455D02BB3AAA",Description="登录接口",Path="/api/account/login",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("c96dd7f7-f935-4499-8ef5-6d39fe26141a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:29"),},
                new Function() {Group="用户中心服务",Service="用户账户认证授权服务",Summary="获取当前用户的所有菜单",Key="3317F3470BD4CCECEB26F73F6551D9D6",Description="获取当前用户的所有菜单",Path="/api/account/current-user-menus",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("e2bb65e0-5d9e-485e-9059-8148fc236246"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Now,UpdatedTime=DateTimeOffset.Parse("2022-08-05 17:49:36"),},
         };
        }

    }
}