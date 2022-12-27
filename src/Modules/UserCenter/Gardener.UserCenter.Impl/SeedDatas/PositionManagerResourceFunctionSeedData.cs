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

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class PositionManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("0fd84267-ee22-47c4-b41c-ce654eba29d9"),FunctionId=Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("0fd84267-ee22-47c4-b41c-ce654eba29d9"),FunctionId=Guid.Parse("7a3399b3-6003-4aae-8e24-2e478992630e"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("25535592-81a1-42dd-8a55-509f2c852ff9"),FunctionId=Guid.Parse("05153ee4-dc99-4834-b398-5999f7dc8d01"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("25535592-81a1-42dd-8a55-509f2c852ff9"),FunctionId=Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("34b187cc-dd6f-4edf-a22c-a339be59d5c3"),FunctionId=Guid.Parse("c715a6d5-cd99-4c94-8760-936817c1e09c"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3d6e9553-2baf-4d9d-8a82-65de1c7d7ece"),FunctionId=Guid.Parse("89954833-64a5-4c87-a717-9c863ca3b263"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4e845d07-33a4-4dc4-ba7f-8568f88b9d68"),FunctionId=Guid.Parse("65a3c1ee-f5cf-48eb-9bf0-3d4db44257e4"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("ba89c7b7-552c-415c-b4be-085262dc76b0"),FunctionId=Guid.Parse("715a2905-da23-405d-98a0-1a1222f7d101"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f4fa035f-27ae-4eee-b006-3cbfac3d2172"),FunctionId=Guid.Parse("c715a6d5-cd99-4c94-8760-936817c1e09c"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }

}
