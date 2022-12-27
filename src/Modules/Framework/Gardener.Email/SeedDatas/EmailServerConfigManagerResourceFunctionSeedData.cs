// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Gardener.Base.Entity;

namespace Gardener.Email.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailServerConfigManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<ResourceFunction> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new ResourceFunction() {ResourceId=Guid.Parse("106a3a28-3143-4369-9215-cb223d1b0e45"),FunctionId=Guid.Parse("99546746-70b8-42d6-884d-ea1b79f88c0a"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("1f8605fb-70b3-4929-89eb-4cda69cc305b"),FunctionId=Guid.Parse("26d95428-ebbd-4bf2-9bcc-2eeec4263bd5"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3d93eb77-2a72-4b4f-aa79-4da1fc7943c9"),FunctionId=Guid.Parse("7f36ba4f-ec97-4fa9-953b-fa2f1686c448"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("46b8f9b5-fe41-4b55-b39f-4cb398186d2c"),FunctionId=Guid.Parse("39421a19-9cbf-477b-baea-34f40341357f"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a807706b-ffb3-4f8d-b18d-9a7ee6b88028"),FunctionId=Guid.Parse("1ef3b8a8-6e46-49d7-9a7e-f63137beaade"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d697fda5-28fa-46c3-ba88-a98dd510e09d"),FunctionId=Guid.Parse("9fe5cc45-a851-4d3f-8b44-32dd96130946"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f63a570e-a762-4410-b4b1-764ee5ceb7ae"),FunctionId=Guid.Parse("9d25bf25-5470-4fed-b58c-c4ef4339d533"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }
}

