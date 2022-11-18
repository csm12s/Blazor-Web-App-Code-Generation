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
    public class DeptManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="部门管理",Key="user_center_dept",Remark="",Path="/user_center/dept",Icon="team",Order=0,ParentId=Guid.Parse("bd892fb3-47b4-469e-ba14-7c0eb703e164"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),},
                new Resource() {Name="添加部门",Key="user_center_dept_add",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"),},
                new Resource() {Name="添加子级部门",Key="user_center_dept_add_children",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("04c237bb-7670-4d66-bbaa-dcd9624d2d90"),},
                new Resource() {Name="删除部门",Key="user_center_dept_delete",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("de62a886-64b2-4a40-b70a-47eb08f23202"),},
                new Resource() {Name="删除选中部门",Key="user_center_dept_delete_selected",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),},
                new Resource() {Name="查看部门详情",Key="user_center_dept_detail",Remark="查看部门详情",Path="",Icon="",Order=0,ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("b63d694e-205f-44c0-8353-0c9507f44696"),},
                new Resource() {Name="编辑部门",Key="user_center_dept_edit",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("316ecba5-5d89-44ae-908f-a54268723bd1"),},
                new Resource() {Name="刷新部门",Key="user_center_dept_refresh",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("186bca5f-cc2c-427e-a58a-dbb81641a296"),},
         };
        }

    }
}
