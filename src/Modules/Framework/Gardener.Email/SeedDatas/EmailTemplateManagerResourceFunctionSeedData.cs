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
    public class EmailTemplateManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("083fffc4-2600-49bb-87e6-1a92133499ec"),FunctionId=Guid.Parse("9191206c-f35e-4eb7-b19a-5949dc560369"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("08baa5af-4718-4158-9276-1ad1068b9159"),FunctionId=Guid.Parse("33c2157a-884d-4030-abea-a9aeea51fdf8"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("145ec764-6a72-4c4f-85d3-7ad889193970"),FunctionId=Guid.Parse("31896c5d-2ed7-4e43-a952-4edc076d29d0"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("7aad6dba-3f13-4982-adfa-525fa94485dd"),FunctionId=Guid.Parse("3ac59980-d2df-4363-b8db-a4d043e362e7"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("af9b9a49-0094-4e1c-97dc-d0580525244f"),FunctionId=Guid.Parse("7f36ba4f-ec97-4fa9-953b-fa2f1686c448"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b5320a70-11fe-4b7a-9c7e-5bb132e72639"),FunctionId=Guid.Parse("841c572c-5098-4e72-a590-2b81706aaa93"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b7cdae2b-4f9b-493a-b43b-a3c7ffef3b86"),FunctionId=Guid.Parse("2bf3ff67-c1a3-4426-8320-11839daa0a81"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("ef15af79-1be1-4055-82b0-83a6aa8fdd35"),FunctionId=Guid.Parse("736fd9b6-b56a-4860-8a1c-9a077be886e3"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }
}