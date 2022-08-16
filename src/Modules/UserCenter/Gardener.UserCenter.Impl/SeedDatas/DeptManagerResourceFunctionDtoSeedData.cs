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
    public class DeptManagerResourceFunctionDtoSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("04c237bb-7670-4d66-bbaa-dcd9624d2d90"),FunctionId=Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("186bca5f-cc2c-427e-a58a-dbb81641a296"),FunctionId=Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("1d2fb341-3b69-4d0b-934d-c4c2cd250401"),FunctionId=Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("316ecba5-5d89-44ae-908f-a54268723bd1"),FunctionId=Guid.Parse("e23b555c-600a-4839-9439-2ee0ad0ae4f8"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("57a8f870-c76f-4ce0-b660-bf6661dc9baf"),FunctionId=Guid.Parse("a96bb19e-794e-4fe0-ad39-f423df44f633"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b63d694e-205f-44c0-8353-0c9507f44696"),FunctionId=Guid.Parse("2502e6ae-879b-4674-a557-cd7b4de891a7"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("de62a886-64b2-4a40-b70a-47eb08f23202"),FunctionId=Guid.Parse("337bae83-a083-4e0e-8ceb-2bb21ae22145"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("defa9a78-229f-43a9-b6b8-95dd6fd8a3c3"),FunctionId=Guid.Parse("f5c318f6-9230-475a-830e-a404e17506b5"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }
}
