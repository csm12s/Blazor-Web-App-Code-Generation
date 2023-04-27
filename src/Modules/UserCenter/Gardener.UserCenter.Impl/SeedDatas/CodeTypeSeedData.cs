// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class CodeTypeSeedData : IEntitySeedData<CodeType>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<CodeType> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new CodeType() {CodeTypeName="岗位等级",CodeTypeValue="position-level",Remark="",Id=3,IsLocked=false,IsDeleted=false,CreateBy="6",UpdateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-25 09:26:58"),UpdatedTime=DateTimeOffset.Parse("2023-04-25 09:27:06"),},
                new CodeType() {CodeTypeName="爱好",CodeTypeValue="interest",Remark="",Id=2,IsLocked=false,IsDeleted=false,CreateBy="6",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-24 18:10:09"),},
                new CodeType() {CodeTypeName="心情",CodeTypeValue="mood",Remark="",Id=1,IsLocked=false,IsDeleted=false,CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:39:45"),},
         };
        }
    }

}
