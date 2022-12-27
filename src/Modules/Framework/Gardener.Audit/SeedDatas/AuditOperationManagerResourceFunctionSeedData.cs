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

namespace Gardener.Audit.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AuditOperationManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("1c377037-13b4-4ef2-8010-d914a40fdbb3"),FunctionId=Guid.Parse("080dd200-8e8a-489c-86ca-8eb74c417c0b"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("24ace337-41fe-429d-b32e-d9f88bd97aaa"),FunctionId=Guid.Parse("1d994e50-d40a-465b-8445-646041a8131a"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("5591c5b9-9ee0-44ae-a4fa-39234b95afa4"),FunctionId=Guid.Parse("73cfe63f-3338-4bd0-a0b9-1b9cc39951ea"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),FunctionId=Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("cc23917b-930a-4e34-9717-be71b9fd2dd5"),FunctionId=Guid.Parse("db76ae46-851b-47bc-94be-b2e869043636"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("2ac78309-1719-4ea5-ac0f-6974a86f168c"),FunctionId=Guid.Parse("5df6ccb0-8985-4767-9ff2-b306d791194e"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:42:14"),},

         };
        }
    }
}
