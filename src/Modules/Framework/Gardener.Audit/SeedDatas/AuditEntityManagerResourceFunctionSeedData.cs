// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.Audit.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AuditEntityManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("286dc779-f58d-439a-bb9b-1333ff2b111b"),FunctionId=Guid.Parse("7e5577d4-32b2-4f43-a83f-05410b59b195"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("7f772fcb-fe68-4edb-9f7a-6ef520aa25f1"),FunctionId=Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8158e1a6-335d-4a29-9177-0f30e86fa8ec"),FunctionId=Guid.Parse("12dbe1a6-7d23-48a4-bacb-164f0403d0f4"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d1c558a6-6d54-4ba0-872a-c61cd04db9bb"),FunctionId=Guid.Parse("7fa014c4-08db-4f96-8132-2bf3db32b256"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4f259695-23ea-4453-a4f1-2b055d135c37"),FunctionId=Guid.Parse("547d94df-965f-4833-88fb-c3f66244926c"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:42:45"),},
         };
        }
    }

}
