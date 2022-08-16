// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ClientManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("374f7bfd-3c16-40dd-b4dc-a5992a0915cf"),FunctionId=Guid.Parse("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4c96cdb4-efc1-4ccc-8ec6-9ca1bc458d8a"),FunctionId=Guid.Parse("4963631e-6343-469a-a189-10bfce6e3195"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("757fdf0b-0cb9-4f24-92f6-24e18f3defcc"),FunctionId=Guid.Parse("8172d258-7a75-4ced-b5e2-b0be7350aa1f"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("86a086a1-0770-4df4-ade3-433ff7226399"),FunctionId=Guid.Parse("5c0a6241-ac2d-442f-9c6c-028566f18b6a"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8a4e9aee-b116-4822-bd59-b3a98e84b9f3"),FunctionId=Guid.Parse("4c1b9201-09e6-421f-95d1-d98d009a3417"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("92ed8299-ff26-4fae-b852-fe33f0c01a09"),FunctionId=Guid.Parse("cecdfb7d-6796-4bd8-a3d7-164c16a7c959"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a02edffb-0a63-4106-bac2-ea66f1f65060"),FunctionId=Guid.Parse("b79d2f63-487c-44c8-b7d3-1e882994789b"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a0b818e5-f59d-4d3b-b5dc-2f5beca2111f"),FunctionId=Guid.Parse("83cc7cb7-dac6-49f2-85fa-e903039f3d0a"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a1260e4c-e67c-4d72-a758-560a13e9c496"),FunctionId=Guid.Parse("af1f0410-e9cc-4a73-9da7-ea45aadac8b2"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a7555120-c3e4-4f8d-bdf8-371ac22daa50"),FunctionId=Guid.Parse("6e8d08f8-ba2a-4697-8b69-ac5a5bb31bff"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a7a949b0-ca8e-47a1-a5be-ce0fa3c501e6"),FunctionId=Guid.Parse("5d67bd9d-853c-4e16-973d-be0511241fc0"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }

}
