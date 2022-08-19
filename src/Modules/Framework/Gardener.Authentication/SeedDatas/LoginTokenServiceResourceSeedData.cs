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
using Gardener.Authentication.Enums;
using Gardener.Base.Enums;

namespace Gardener.Authentication.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class LoginTokenServiceResourceSeedData : IEntitySeedData<Resource>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Resource() {Name="登录管理",Key="system_manager_login_token",Remark="",Path="/system_manager/login-token",Icon="idcard",Order=70,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),},
                new Resource() {Name="删除登录Token",Key="system_manager_login_token_delete",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),},
                new Resource() {Name="删除选中登录Token",Key="system_manager_login_token_delete_selected",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("f077211f-0e79-44a3-935c-0f704f6a5962"),},
                new Resource() {Name="锁定登录Token",Key="system_manager_login_token_lock",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("0cbb3d40-de41-483e-a76c-3d85682176af"),},
                new Resource() {Name="刷新登录Token",Key="system_manager_login_token_refresh",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),},
                new Resource() {Name="导出登录数据",Key="system_manager_login_token_export",Remark="导出登录数据",Order=0,ParentId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:43:18"),Id=Guid.Parse("bddc6ccc-3f93-4be7-8756-15613cdf76b6"),},
            };
        }
    }
}