// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class CodeManagerResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("1f289163-7fb0-49d2-9165-cbb111b6f3ab"),FunctionId=Guid.Parse("0bc4b901-d9ee-4fac-91c7-e6ea6d6fb852"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:31:04"),},
                new ResourceFunction() {ResourceId=Guid.Parse("36a4434a-f702-42be-a211-862d0b3b5288"),FunctionId=Guid.Parse("483956b8-4651-4dea-8c34-501108220cc1"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:35:05"),},
                new ResourceFunction() {ResourceId=Guid.Parse("36a4434a-f702-42be-a211-862d0b3b5288"),FunctionId=Guid.Parse("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:33:21"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4b4f7b73-df18-4201-876e-b27e172f3b55"),FunctionId=Guid.Parse("06eff7a6-4291-43b0-9c34-58a97aa0fc01"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:31:41"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4b4f7b73-df18-4201-876e-b27e172f3b55"),FunctionId=Guid.Parse("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:34:00"),},
                new ResourceFunction() {ResourceId=Guid.Parse("4e582063-f524-4ce2-9417-ac2cd957332d"),FunctionId=Guid.Parse("810235c8-01f5-4255-a30c-fd77ffeb6eab"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:59:06"),},
                new ResourceFunction() {ResourceId=Guid.Parse("520f5cec-5f33-447c-a18b-59d8db31c5e9"),FunctionId=Guid.Parse("483956b8-4651-4dea-8c34-501108220cc1"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:34:21"),},
                new ResourceFunction() {ResourceId=Guid.Parse("520f5cec-5f33-447c-a18b-59d8db31c5e9"),FunctionId=Guid.Parse("ce5a43ff-7ee1-451b-ae23-8acd04f87f71"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:34:21"),},
                new ResourceFunction() {ResourceId=Guid.Parse("520f5cec-5f33-447c-a18b-59d8db31c5e9"),FunctionId=Guid.Parse("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:35:35"),},
                new ResourceFunction() {ResourceId=Guid.Parse("535e5f96-a036-4a40-96af-6c03cecadcd1"),FunctionId=Guid.Parse("595922c1-5099-4c6f-946f-ed3a7af7d7d3"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:35:47"),},
                new ResourceFunction() {ResourceId=Guid.Parse("5676c9b3-2d06-4817-9614-4a34230bb05e"),FunctionId=Guid.Parse("2fe5043f-fd43-45f7-a60b-e76865778c2a"),CreatedTime=DateTimeOffset.Parse("2023-04-09 14:00:17"),},
                new ResourceFunction() {ResourceId=Guid.Parse("68ebd579-e2c7-4f1c-8f9f-7a06df30bd5f"),FunctionId=Guid.Parse("7faa6bc4-385b-4cdd-ad8b-4e1673fc893b"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:59:46"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8c447e9b-1d39-48e5-b9b9-41ee2058b0c7"),FunctionId=Guid.Parse("f9480a7b-3408-44f0-aca9-78ba66a75799"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:32:27"),},
                new ResourceFunction() {ResourceId=Guid.Parse("8e913a11-dbbe-4aa4-ad58-f12737039d83"),FunctionId=Guid.Parse("56871df3-2984-4d02-8ff7-61101f0abc19"),CreatedTime=DateTimeOffset.Parse("2023-04-09 13:59:29"),},
                new ResourceFunction() {ResourceId=Guid.Parse("9800d45c-7ba8-4728-a6a6-a62dbc7b6f59"),FunctionId=Guid.Parse("1f164c01-cd48-4b41-8c87-0cd7e5dc1973"),CreatedTime=DateTimeOffset.Parse("2023-04-09 14:00:02"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a37b1cd8-98c4-4a93-a73e-436c138639eb"),FunctionId=Guid.Parse("b3f529a4-ad25-4d14-a86b-c1f4cd3f33e0"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:33:41"),},
                new ResourceFunction() {ResourceId=Guid.Parse("addfa7a9-7a8c-46ce-90f1-11424f385954"),FunctionId=Guid.Parse("e242137a-b4ea-4dd9-9692-63885fce93b8"),CreatedTime=DateTimeOffset.Parse("2023-04-09 14:01:48"),},
                new ResourceFunction() {ResourceId=Guid.Parse("bedacea2-80f1-4d4a-b401-c82940f80d4c"),FunctionId=Guid.Parse("595922c1-5099-4c6f-946f-ed3a7af7d7d3"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:30:26"),},
                new ResourceFunction() {ResourceId=Guid.Parse("cd23a5d8-6eab-4e46-a730-56b2808551c6"),FunctionId=Guid.Parse("a28ac095-5faa-4a34-bf4e-5c3d3ab7cac9"),CreatedTime=DateTimeOffset.Parse("2023-04-09 14:01:16"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d9fc6b89-25bb-458e-936f-d76eea2c680f"),FunctionId=Guid.Parse("213eb6ca-e125-4181-b5bd-3e5666d3e8c9"),CreatedTime=DateTimeOffset.Parse("2023-04-07 21:28:56"),},
                new ResourceFunction() {ResourceId=Guid.Parse("e1ab080c-b598-4c1c-9afa-45681f90f1e3"),FunctionId=Guid.Parse("4c206f6e-5e3a-4c96-b48c-81d6df44c9af"),CreatedTime=DateTimeOffset.Parse("2023-04-09 14:00:34"),},
                new ResourceFunction() {ResourceId=Guid.Parse("e1ab080c-b598-4c1c-9afa-45681f90f1e3"),FunctionId=Guid.Parse("a28ac095-5faa-4a34-bf4e-5c3d3ab7cac9"),CreatedTime=DateTimeOffset.Parse("2023-04-09 14:00:57"),},
                new ResourceFunction() {ResourceId=Guid.Parse("2a3f7c64-3ee9-473e-837d-5f443089c886"),FunctionId=Guid.Parse("4e85ec32-8a3c-46e9-ba60-4abd7bee6745"),CreatedTime=DateTimeOffset.Parse("2023-04-26 14:17:09"),},
                new ResourceFunction() {ResourceId=Guid.Parse("2a3f7c64-3ee9-473e-837d-5f443089c886"),FunctionId=Guid.Parse("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"),CreatedTime=DateTimeOffset.Parse("2023-04-26 14:17:09"),},
                new ResourceFunction() {ResourceId=Guid.Parse("874b5529-81d5-4338-9ba9-c084a2e833f1"),FunctionId=Guid.Parse("4e85ec32-8a3c-46e9-ba60-4abd7bee6745"),CreatedTime=DateTimeOffset.Parse("2023-04-26 14:15:40"),},
                new ResourceFunction() {ResourceId=Guid.Parse("874b5529-81d5-4338-9ba9-c084a2e833f1"),FunctionId=Guid.Parse("ed8c2fae-c63f-4aec-af4b-e915b6db38a2"),CreatedTime=DateTimeOffset.Parse("2023-04-26 14:15:40"),},
         };
        }
    }

}
