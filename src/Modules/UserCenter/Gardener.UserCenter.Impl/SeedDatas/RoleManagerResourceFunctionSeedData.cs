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
    public class RoleManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),FunctionId=Guid.Parse("01944b79-bfe5-4304-ade0-9c66e038d5d4"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("13e7d01e-93ca-429c-b412-ff6fa5b6a026"),FunctionId=Guid.Parse("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),FunctionId=Guid.Parse("498638f7-dc92-4d0e-ac5e-26e48cf87a8d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("1efd01cf-42f2-45c7-95f2-84be55e65646"),FunctionId=Guid.Parse("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("2c1c895c-6434-4f14-91f2-144e48457101"),FunctionId=Guid.Parse("cba739f0-9f8a-40c2-afff-d66c3382e096"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("67501fd4-4fbf-48c2-b383-f3a2085268ed"),FunctionId=Guid.Parse("16517409-c055-447b-8e91-7155537c6d15"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),FunctionId=Guid.Parse("cd7db809-50f5-4bf3-a464-89218e24077f"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("799d63fd-48e7-40c2-84e7-a6b36f2c19f3"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a468499c-7115-44f1-ad38-2c5f696891d4"),FunctionId=Guid.Parse("383c5aaf-a3e1-44d1-a1c8-3074abe55f95"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b71bbc5f-83a3-4065-b561-cb4b69b4a507"),FunctionId=Guid.Parse("868fc0df-7cdf-4b56-873e-16dd3e0aa528"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("bf05ffe8-c3ff-402d-bef1-3e95d202fd03"),FunctionId=Guid.Parse("63d7208e-45d3-406e-a4a1-c87e3afda04d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d982a072-4681-45d9-8489-7a14218adb04"),FunctionId=Guid.Parse("2c3ec3c9-76c7-4d29-953f-e7430f22577b"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f2ca3ab7-40da-4828-ad63-06bc9af9b153"),FunctionId=Guid.Parse("38c69230-1ed0-413e-9ae6-05bc1ef989e0"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }

}
