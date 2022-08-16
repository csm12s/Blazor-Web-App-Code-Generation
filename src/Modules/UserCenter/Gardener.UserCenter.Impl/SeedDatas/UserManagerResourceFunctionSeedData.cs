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
    public class UserManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("0aa9b237-dab8-472e-b2e6-af9c0af9f916"),FunctionId=Guid.Parse("9ebd4172-5191-4931-9b22-4c339be4a816"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),FunctionId=Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),FunctionId=Guid.Parse("b38fb0cc-4275-4d1f-8bb7-6f5a962bcc35"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("0f16cfba-bbf5-42c5-83a4-0ac03a1ce5f2"),FunctionId=Guid.Parse("b56c4126-411c-445e-86aa-a91a5ce816d4"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),FunctionId=Guid.Parse("0d2e0194-2238-457b-aab0-9b3259cc4ed9"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("46cad808-0d0b-42bb-a134-3ad6db8ebf54"),FunctionId=Guid.Parse("3790cc0d-dc3a-4669-acba-3a90812c6386"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("476cf96a-0e18-4c30-a760-e8b9c615bb99"),FunctionId=Guid.Parse("6aea8a77-edd2-444b-b8be-901d78321a49"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("87377abe-785d-426c-b052-f706a2c7173d"),FunctionId=Guid.Parse("622c1a11-7dff-4318-9d21-b57fbd1da9ba"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),FunctionId=Guid.Parse("cbc8aff4-6dc0-41f2-b684-caba8e0657ac"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("99b6dcf1-1eae-4653-b30d-423c9c8dc95c"),FunctionId=Guid.Parse("af79d7de-0141-4338-8c52-05216d1b07ff"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d5756ad0-6a8b-4462-907f-1c52a1e11369"),FunctionId=Guid.Parse("0b605fe1-c77c-4735-8320-b8f400163ac9"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("e44bb45d-514c-4217-bfba-452c0bd38f28"),FunctionId=Guid.Parse("3e2f4464-6b69-4a00-acfb-d39184729cdd"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),FunctionId=Guid.Parse("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("ea0fb035-1f06-4f61-9946-8df027a7462d"),FunctionId=Guid.Parse("0c6f2138-e984-4fba-ad2a-2890716a7259"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }
}
