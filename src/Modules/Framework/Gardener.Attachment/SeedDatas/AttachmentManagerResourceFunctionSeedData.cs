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

namespace Gardener.Attachment.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class AttachmentManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("67ad5c3a-8611-4183-ad9e-63cb4c9760fa"),FunctionId=Guid.Parse("2f820c7f-4f1c-4737-aae6-329585c75d92"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("925c3162-155c-4644-8ca2-075f9fc76235"),FunctionId=Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d998802f-776e-4137-bc63-d8d818464f98"),FunctionId=Guid.Parse("10190ac3-1092-49a9-8ad2-313454b40447"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f02f906a-7579-478a-9406-3c8fd2c54886"),FunctionId=Guid.Parse("070ae0e4-0193-4ce0-8ba6-b8c344086ced"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("f1649263-ef9a-4f42-85ac-16009283efff"),FunctionId=Guid.Parse("6a9763c9-c40f-44f3-a248-a3b1e3d1f586"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }

}
