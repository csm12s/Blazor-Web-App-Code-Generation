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

namespace Gardener.Attachment.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AttachmentManagerResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="附件管理",Key="system_manager_attachment",Remark="附件管理",Path="/system_manager/attachment",Icon="file",Order=50,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),},
                new Resource() {Name="删除附件",Key="system_manager_attachment_delete",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("f02f906a-7579-478a-9406-3c8fd2c54886"),},
                new Resource() {Name="删除选中附件",Key="system_manager_attachment_delete_selected",Remark="删除选中附件",Path="null",Icon="null",Order=0,ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),},
                new Resource() {Name="查看附件",Key="system_manager_attachment_detail",Remark="查看附件",Path="",Icon="",Order=0,ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"),},
                new Resource() {Name="刷新附件",Key="system_manager_attachment_refresh",Remark="",Path="",Icon="",Order=0,ParentId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreateIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),},
         };
        }

    }
}
