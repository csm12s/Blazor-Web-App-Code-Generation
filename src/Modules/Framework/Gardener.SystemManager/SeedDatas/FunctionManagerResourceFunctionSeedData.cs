// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Domains;
using Microsoft.EntityFrameworkCore;

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class FunctionManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("068f13c5-7830-473b-bcc0-f0c2bcaeb558"),FunctionId=Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("50062351-8235-4da1-9f90-4917d0e8abe0"),FunctionId=Guid.Parse("aeb8b23d-4da3-4ec0-867f-70d2e2ba9550"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("50062351-8235-4da1-9f90-4917d0e8abe0"),FunctionId=Guid.Parse("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("6ac07813-4d10-4b50-9f0c-ecd444041282"),FunctionId=Guid.Parse("416fe54b-6c50-4b1b-bf77-6744cf19fa72"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("6e487179-5bb2-4ab5-80e3-58c514c9595f"),FunctionId=Guid.Parse("8ae9c253-584e-46e4-b805-6ec90281d6dd"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId=Guid.Parse("84256e5b-2cef-4b16-8fd3-79ff8d47c731"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId=Guid.Parse("8d94c826-ddba-47fe-94c9-333880fee187"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId=Guid.Parse("a15ce231-80ae-46c6-ada8-49666e81e328"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId=Guid.Parse("a53a9c89-7968-4598-9c46-dad4e9188bd0"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("749c3a63-6bd8-4755-87ed-c1d455e5b717"),FunctionId=Guid.Parse("c591c0ca-3305-4684-89bb-278218d13c47"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("92da96d7-c59c-4d4b-8c97-80a9f59e8fa2"),FunctionId=Guid.Parse("b952b41e-b3e9-4c53-9a7d-6b561acf4bc4"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("b100a7eb-ef44-4669-bac5-3c5ce52871bb"),FunctionId=Guid.Parse("4b57474a-88b4-4393-bb49-4b59e8c3c41d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("bd7d1a4c-960a-48b2-9c9e-083aa5c5924f"),FunctionId=Guid.Parse("c39030b8-d207-4c22-a3ba-74b0eccaa2fa"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("cc8a9836-3c4d-4d0b-ae64-a31a6bb36b6f"),FunctionId=Guid.Parse("c4cc2526-8403-4e6c-a88b-94e55279eaa3"),CreatedTime=DateTimeOffset.Parse("2022-08-16 17:55:46"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4171f5aa-2ce1-40ad-b69e-59de1cd20416"),FunctionId=Guid.Parse("dca2b115-3363-4f7f-8bba-b051b8d8603a"),CreatedTime=DateTimeOffset.Parse("2022-08-19 18:31:49"),},
         };
        }
    }

}
