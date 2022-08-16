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
    public class ResourceManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("08ae2764-e551-45d2-9da7-49648481a8e0"),FunctionId=Guid.Parse("7f0d7abb-06a4-4a35-b4e3-7798b21e37fa"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("14636a9b-e6d6-436f-a0aa-0170eed08d99"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("365fc5c4-404e-408a-88dc-7614dffad91b"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4af87acd-64b4-4d53-8043-cd7ab6b03c77"),FunctionId=Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4f943ed1-997a-485f-9b54-9824b4ac285c"),FunctionId=Guid.Parse("ffef6a8e-3f80-4a39-97c6-5b2b81582830"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("64346edf-1390-4a90-bc63-93f322ed6c8f"),FunctionId=Guid.Parse("c56d6a82-abc8-4b17-bc28-27b1904116c9"),CreatedTime=DateTimeOffset.Parse("2022-08-16 17:59:58"),},
                new ResourceFunction() {ResourceId=Guid.Parse("859aa714-67c7-4414-bc96-9de5b7aec2c4"),FunctionId=Guid.Parse("910d2a4f-85ae-46ff-bddd-b65ffcc6b9e1"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId=Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId=Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8ab307de-ad4b-462f-b61d-7f1d53b82f3d"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("97a7d440-b7fe-4af6-a8a1-18846c48828b"),FunctionId=Guid.Parse("5eb48cf2-6c45-47c2-a68b-84284a389c69"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a1958e51-06d4-4b29-9533-eae9d86c41d1"),FunctionId=Guid.Parse("cdd3c605-ed1d-4d94-a482-16430b729541"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId=Guid.Parse("04ad3c68-6e35-4175-a8ff-564d4bf51e91"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId=Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("c18d4928-35d2-4085-aec9-379d00bcfd8f"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),FunctionId=Guid.Parse("b79d2f63-487c-44c8-b7d3-1e882994789b"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("c4991844-d3b4-4f9a-9c90-c13114515796"),FunctionId=Guid.Parse("fff9f1e7-7fd3-42f5-afe7-d40cca07f0ca"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("ca1d4b3a-336b-40a5-b683-0fe0bcbabaf8"),FunctionId=Guid.Parse("c1e7fa06-b759-4bb0-9545-7265e3798d28"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),FunctionId=Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d83c05a0-4d23-4b2b-ba87-284793bf3eba"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId=Guid.Parse("10fc92a8-30ed-4536-a995-c7af8e5548a1"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId=Guid.Parse("45dd0581-3394-4c0a-bb8e-c9e0074d5611"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("dec04485-3dab-4251-b7b8-1044e749a51e"),FunctionId=Guid.Parse("e38c1619-0f84-4e55-81c2-0f47992ee33d"),CreatedTime=DateTimeOffset.Parse("2021-11-19 08:19:47"),},
         };
        }
    }

}
