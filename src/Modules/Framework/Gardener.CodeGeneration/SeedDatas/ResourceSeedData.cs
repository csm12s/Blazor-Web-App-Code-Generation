// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.CodeGeneration.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="代码生成",Key="system_tool_code_gen",Path="/system_tool/code_gen",Icon="code-sandbox",Order=41,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),Id=Guid.Parse("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),CreatedTime=DateTimeOffset.Parse("2022-11-01 14:32:13"),UpdatedTime=DateTimeOffset.Parse("2022-11-01 14:33:10"),},
         };
        }
    }
}
