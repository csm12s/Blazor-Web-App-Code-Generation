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
    public class LonginResourceSeedData : IEntitySeedData<Resource>
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
                new Resource() {Name="登录",Key="system_login",Remark="登录系统",Path="",Icon="",Order=0,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Type=Enum.Parse<ResourceType>("Action"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),},
         };
        }
    }

}
