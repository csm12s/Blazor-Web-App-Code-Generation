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

namespace Gardener.Authentication.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class LoginTokenServiceResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("0cbb3d40-de41-483e-a76c-3d85682176af"),FunctionId=Guid.Parse("f59833a1-c9af-4bb2-be4b-d6935513fc99"),CreatedTime=DateTimeOffset.Parse("2022-08-18 15:05:26"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),FunctionId=Guid.Parse("571200a8-bde2-430b-84ea-743db7b282cd"),CreatedTime=DateTimeOffset.Parse("2022-08-18 15:06:17"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3d007d84-d209-49e2-94ca-11ad2a3dd91d"),FunctionId=Guid.Parse("8f114b96-dc3d-4dd4-854a-4c793c121e43"),CreatedTime=DateTimeOffset.Parse("2022-08-18 15:06:17"),},
                new ResourceFunction() {ResourceId=Guid.Parse("c98160ef-ce87-4a1b-bfb3-09fc79d2a34a"),FunctionId=Guid.Parse("e651d9a4-9d6d-44c7-a833-08da6ed19892"),CreatedTime=DateTimeOffset.Parse("2022-08-18 15:06:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f077211f-0e79-44a3-935c-0f704f6a5962"),FunctionId=Guid.Parse("040878a9-1b78-494e-9ee1-b4a7eab118fb"),CreatedTime=DateTimeOffset.Parse("2022-08-18 15:07:18"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f077211f-0e79-44a3-935c-0f704f6a5962"),FunctionId=Guid.Parse("6dc1a088-15f6-43b8-8465-3a95cc495bab"),CreatedTime=DateTimeOffset.Parse("2022-08-18 15:07:18"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fb4f6cc5-8f3a-4885-aba4-23a5a8c70b41"),FunctionId=Guid.Parse("e651d9a4-9d6d-44c7-a833-08da6ed19892"),CreatedTime=DateTimeOffset.Parse("2022-08-18 15:04:21"),},
                new ResourceFunction() {ResourceId=Guid.Parse("bddc6ccc-3f93-4be7-8756-15613cdf76b6"),FunctionId=Guid.Parse("cd1b93ed-2fae-47f2-83b2-e9b0a949f476"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:43:50"),},
         };
        }
    }
}