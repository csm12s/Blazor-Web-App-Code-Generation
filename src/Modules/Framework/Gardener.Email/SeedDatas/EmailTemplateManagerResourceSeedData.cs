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

namespace Gardener.Email.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailTemplateManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="邮件模板",Key="system_manager_email_temaplate",Remark="邮件模板",Path="/system_manager/email_temaplate",Icon="copy",Order=20,ParentId=Guid.Parse("6dc2b297-7110-462a-b402-9e9736abf292"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),},
                new Resource() {Name="添加邮件模板",Key="system_manager_email_template_add",Remark="添加邮件模板",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("083fffc4-2600-49bb-87e6-1a92133499ec"),},
                new Resource() {Name="删除邮件模板",Key="system_manager_email_template_delete",Remark="删除邮件模板",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("b5320a70-11fe-4b7a-9c7e-5bb132e72639"),},
                new Resource() {Name="删除选中邮件模板",Key="system_manager_email_template_delete_selected",Remark="删除选中邮件模板",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("145ec764-6a72-4c4f-85d3-7ad889193970"),},
                new Resource() {Name="查看邮件模板",Key="system_manager_email_template_detail",Remark="查看邮件模板",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("7aad6dba-3f13-4982-adfa-525fa94485dd"),},
                new Resource() {Name="编辑邮件模板",Key="system_manager_email_template_edit",Remark="编辑邮件模板",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("08baa5af-4718-4158-9276-1ad1068b9159"),},
                new Resource() {Name="锁定邮件模板",Key="system_manager_email_template_lock",Remark="锁定邮件模板",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("ef15af79-1be1-4055-82b0-83a6aa8fdd35"),},
                new Resource() {Name="刷新邮件模板列表",Key="system_manager_email_template_refresh",Remark="刷新邮件模板列表",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"),},
                new Resource() {Name="发送测试邮件",Key="system_manager_email_template_send",Remark="发送测试邮件",Path="",Icon="",Order=0,ParentId=Guid.Parse("8bad2f7b-15ce-4d64-ad95-4aa9eae857b4"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("af9b9a49-0094-4e1c-97dc-d0580525244f"),},
         };
        }
    }
}