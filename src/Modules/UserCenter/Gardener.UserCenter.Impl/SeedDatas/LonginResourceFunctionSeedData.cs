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
    public class LonginResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("03ee6f4b-dfea-4803-9515-3a9b2f907c90"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("1c6dfb26-4149-4fa3-a7de-083ad7ff7d6c"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("38545a67-61ff-4e5c-90bb-a555a93fcbea"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("68ce42ff-acc7-485f-bc91-df471b520be7"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("713341f2-47e1-42af-b717-bfa75904d32e"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("89a06a4e-1a8e-41aa-a443-fd11bcc8497d"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("c96dd7f7-f935-4499-8ef5-6d39fe26141a"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("e0b19b01-4b37-426a-a5c4-749e5936b64c"),CreatedTime=DateTimeOffset.Parse("2023-04-13 14:07:02"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fd070704-3d11-4c46-8ca0-7ecd2ac7df74"),FunctionId=Guid.Parse("e2bb65e0-5d9e-485e-9059-8148fc236246"),CreatedTime=DateTimeOffset.Parse("2022-09-05 17:18:08"),},
         };
        }
    }

}
