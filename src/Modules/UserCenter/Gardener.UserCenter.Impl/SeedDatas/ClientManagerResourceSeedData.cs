// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Domains;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ClientManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="客户端管理",Key="system_manager_client",Remark="客户端管理",Path="/system_manager/client",Icon="cloud-server",Order=45,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),},
                new Resource() {Name="添加客户端",Key="system_manager_client_add",Remark="添加客户端",Path="",Icon="",Order=0,ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"),},
                new Resource() {Name="删除客户端",Key="system_manager_client_delete",Remark="删除客户端",Path="",Icon="",Order=0,ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a1260e4c-e67c-4d72-a758-560a13e9c496"),},
                new Resource() {Name="删除选中客户端",Key="system_manager_client_delete_selected",Remark="删除选中客户端",Path="",Icon="",Order=0,ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"),},
                new Resource() {Name="查看客户端",Key="system_manager_client_detail",Remark="查看客户端",Path="",Icon="",Order=0,ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"),},
                new Resource() {Name="编辑客户端",Key="system_manager_client_edit",Remark="编辑客户端",Path="",Icon="",Order=0,ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("92ed8299-ff26-4fae-b852-fe33f0c01a09"),},
                new Resource() {Name="显示可选接口",Key="system_manager_client_function_add_page_show",Remark="显示可选接口",Path="",Icon="",Order=0,ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a02edffb-0a63-4106-bac2-ea66f1f65060"),},
                new Resource() {Name="绑定客户端接口关系",Key="system_manager_client_function_binding",Remark="绑定资源接口关系",Path="",Icon="",Order=0,ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("a7555120-c3e4-4f8d-bdf8-371ac22daa50"),},
                new Resource() {Name="删除选中客户端接口关系",Key="system_manager_client_function_delete_selected",Remark="删除选中客户端接口关系",Path="",Icon="",Order=0,ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"),},
                new Resource() {Name="刷新客户端",Key="system_manager_client_refresh",Remark="刷新客户端",Path="",Icon="",Order=0,ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"),},
                new Resource() {Name="关联客户端接口关系",Key="system_manager_client_show_function",Remark="关联客户端接口关系",Path="",Icon="",Order=0,ParentId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),},
                new Resource() {Name="显示已关联接口",Key="system_manager_client_show_function_1",Remark="显示已关联接口",Path="",Icon="",Order=0,ParentId=Guid.Parse("7f9c7946-edbf-4ff2-9e2b-a3cd635b0e84"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("86a086a1-0770-4df4-ade3-433ff7226399"),},
         };
        }
    }

}
