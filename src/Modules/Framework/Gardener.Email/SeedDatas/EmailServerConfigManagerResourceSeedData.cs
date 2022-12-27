// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gardener.Authentication.Enums;
using Gardener.Base.Enums;
using Gardener.Base.Entity;

namespace Gardener.Email.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailServerConfigManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="邮件服务器",Key="system_manager_email_server_config",Remark="邮件服务器配置",Path="/system_manager/email_server_config",Icon="setting",Order=10,ParentId=Guid.Parse("6dc2b297-7110-462a-b402-9e9736abf292"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),},
                new Resource() {Name="添加邮件服务器配置",Key="system_manager_email_server_config_add",Remark="添加邮件服务器配置",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"),},
                new Resource() {Name="删除邮件服务器配置",Key="system_manager_email_server_config_delete",Remark="删除邮件服务器配置",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"),},
                new Resource() {Name="删除选中邮件服务器配置",Key="system_manager_email_server_config_delete_selected",Remark="删除选中邮件服务器配置",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("1f8605fb-70b3-4929-89eb-4cda69cc305b"),},
                new Resource() {Name="查看邮件服务器配置",Key="system_manager_email_server_config_detail",Remark="查看邮件服务器配置",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("f63a570e-a762-4410-b4b1-764ee5ceb7ae"),},
                new Resource() {Name="编辑邮件服务器配置",Key="system_manager_email_server_config_edit",Remark="编辑邮件服务器配置",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("106a3a28-3143-4369-9215-cb223d1b0e45"),},
                new Resource() {Name="锁定邮件服务器配置",Key="system_manager_email_server_config_lock",Remark="锁定邮件服务器配置",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("02337e03-c44f-4029-bbb2-0cc5adf84c29"),},
                new Resource() {Name="刷新邮件服务器配置",Key="system_manager_email_server_config_refresh",Remark="刷新邮件服务器配置",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("d697fda5-28fa-46c3-ba88-a98dd510e09d"),},
                new Resource() {Name="发送测试邮件",Key="system_manager_email_server_config_send",Remark="发送测试邮件",Path="",Icon="",Order=0,ParentId=Guid.Parse("99c74c8b-e343-43bc-86e3-bca825b6a270"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"),},
         };
        }
    }
}
