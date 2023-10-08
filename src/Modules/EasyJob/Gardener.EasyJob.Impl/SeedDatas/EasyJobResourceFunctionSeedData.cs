// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gardener.EasyJob.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EasyJobResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("01061a49-b2d6-4c14-887b-e23ae4539031"),FunctionId=Guid.Parse("7d53bd2b-b5c2-4a79-91e7-591bd8bb4d22"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:34:58"),},
                new ResourceFunction() {ResourceId=Guid.Parse("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),FunctionId=Guid.Parse("5c83a810-f270-4602-9cc5-4cae64cc032c"),CreatedTime=DateTimeOffset.Parse("2023-08-21 10:50:16"),},
                new ResourceFunction() {ResourceId=Guid.Parse("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),FunctionId=Guid.Parse("78fc70a3-d21b-4c7f-8e21-9833ece89eef"),CreatedTime=DateTimeOffset.Parse("2023-08-21 10:50:16"),},
                new ResourceFunction() {ResourceId=Guid.Parse("164abf88-cbe6-4002-aeb1-6a84ebd644d0"),FunctionId=Guid.Parse("eea858a2-155b-4934-b4b6-219beaea44f8"),CreatedTime=DateTimeOffset.Parse("2023-08-21 10:50:16"),},
                new ResourceFunction() {ResourceId=Guid.Parse("21b2aec3-0c17-4c3f-82f7-dfa0ab76877a"),FunctionId=Guid.Parse("0ed7905f-7825-4f7f-8a00-18cd2da1e89b"),CreatedTime=DateTimeOffset.Parse("2023-08-21 10:49:04"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3bd11c81-982f-400a-b6e8-d9a27b8baee1"),FunctionId=Guid.Parse("0ed7905f-7825-4f7f-8a00-18cd2da1e89b"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:44:54"),},
                new ResourceFunction() {ResourceId=Guid.Parse("3e23e69d-8e27-466b-bfc6-a8f1f191549d"),FunctionId=Guid.Parse("26afb233-f6cc-47bb-b367-0b0b7ce3fe42"),CreatedTime=DateTimeOffset.Parse("2023-08-21 10:49:56"),},
                new ResourceFunction() {ResourceId=Guid.Parse("54dc1159-93cd-4690-9ec1-f45e9a5dca7a"),FunctionId=Guid.Parse("193923dc-e968-4e75-9531-b090ffae7eb7"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:34:22"),},
                new ResourceFunction() {ResourceId=Guid.Parse("5e858248-f765-4412-9753-92621f20f611"),FunctionId=Guid.Parse("29e9de55-7f46-4ffe-8b16-4b928046a679"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:27:25"),},
                new ResourceFunction() {ResourceId=Guid.Parse("61f0721c-a1d2-4b11-99e0-2e56533a433c"),FunctionId=Guid.Parse("779b4330-2c14-4733-8161-e1230a9db78c"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:32:30"),},
                new ResourceFunction() {ResourceId=Guid.Parse("693cd650-3b03-4bdf-8080-14112547329c"),FunctionId=Guid.Parse("29e9de55-7f46-4ffe-8b16-4b928046a679"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:29:43"),},
                new ResourceFunction() {ResourceId=Guid.Parse("696c06fd-a230-4472-adca-d378747091a4"),FunctionId=Guid.Parse("a95c3584-f7cd-4641-a4ad-397e5a3ea5f4"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:29:27"),},
                new ResourceFunction() {ResourceId=Guid.Parse("696c06fd-a230-4472-adca-d378747091a4"),FunctionId=Guid.Parse("f71b16b5-22d1-4968-aeeb-bbfc3c53aebf"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:29:27"),},
                new ResourceFunction() {ResourceId=Guid.Parse("9299ac14-8d67-45a0-846e-ab35d15c0fbc"),FunctionId=Guid.Parse("b63017f6-d5a9-469f-a682-b0a2ba622f0b"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:33:34"),},
                new ResourceFunction() {ResourceId=Guid.Parse("9299ac14-8d67-45a0-846e-ab35d15c0fbc"),FunctionId=Guid.Parse("e8e5ebed-ed84-485a-8f51-3ea62ed13af8"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:31:16"),},
                new ResourceFunction() {ResourceId=Guid.Parse("95d5c35c-fdb3-4fec-bc6c-92aa5f61680f"),FunctionId=Guid.Parse("2f2ac0d9-9544-4d5d-95f7-adbb4171e4ad"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:31:53"),},
                new ResourceFunction() {ResourceId=Guid.Parse("95d5c35c-fdb3-4fec-bc6c-92aa5f61680f"),FunctionId=Guid.Parse("e8e5ebed-ed84-485a-8f51-3ea62ed13af8"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:30:40"),},
                new ResourceFunction() {ResourceId=Guid.Parse("9c9c7330-1bd8-4582-87e6-cad9e7b6d755"),FunctionId=Guid.Parse("0ed7905f-7825-4f7f-8a00-18cd2da1e89b"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:32:36"),},
                new ResourceFunction() {ResourceId=Guid.Parse("9c9c7330-1bd8-4582-87e6-cad9e7b6d755"),FunctionId=Guid.Parse("249577f0-a0e4-42ea-beed-2a035533933f"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:32:36"),},
                new ResourceFunction() {ResourceId=Guid.Parse("9c9c7330-1bd8-4582-87e6-cad9e7b6d755"),FunctionId=Guid.Parse("e8e5ebed-ed84-485a-8f51-3ea62ed13af8"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:32:36"),},
                new ResourceFunction() {ResourceId=Guid.Parse("a0a21d0c-b733-40e7-833f-73c97baf913a"),FunctionId=Guid.Parse("0ed7905f-7825-4f7f-8a00-18cd2da1e89b"),CreatedTime=DateTimeOffset.Parse("2023-08-21 10:49:34"),},
                new ResourceFunction() {ResourceId=Guid.Parse("ae604973-bb28-4deb-87a5-c3da8b88d6d3"),FunctionId=Guid.Parse("a95c3584-f7cd-4641-a4ad-397e5a3ea5f4"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:29:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("bfa94510-818b-4058-b20f-e4c95ca23a5b"),FunctionId=Guid.Parse("249577f0-a0e4-42ea-beed-2a035533933f"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:29:58"),},
                new ResourceFunction() {ResourceId=Guid.Parse("bfa94510-818b-4058-b20f-e4c95ca23a5b"),FunctionId=Guid.Parse("e8e5ebed-ed84-485a-8f51-3ea62ed13af8"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:29:58"),},
                new ResourceFunction() {ResourceId=Guid.Parse("cb7772c4-6dda-4c0a-aa7b-c506b303da02"),FunctionId=Guid.Parse("6f9c2287-753e-42d4-a465-c61dd1820fcf"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:31:10"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d137d256-a643-4e1d-bec2-2489f4f3630c"),FunctionId=Guid.Parse("ed2911d8-c745-4140-90d6-7d82b5252b34"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:29:59"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d1c1f5c6-907b-47db-9d1f-6c6d87a64494"),FunctionId=Guid.Parse("2f2ac0d9-9544-4d5d-95f7-adbb4171e4ad"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:30:51"),},
                new ResourceFunction() {ResourceId=Guid.Parse("d25b3920-8833-4168-bff1-1065cd72c8c7"),FunctionId=Guid.Parse("f1be2be4-b212-4725-9d5b-d99f0d545a64"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:33:58"),},
                new ResourceFunction() {ResourceId=Guid.Parse("df23010b-e960-4c50-b114-e84df2edda4f"),FunctionId=Guid.Parse("0ed7905f-7825-4f7f-8a00-18cd2da1e89b"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:45:23"),},
                new ResourceFunction() {ResourceId=Guid.Parse("e787f680-8ad9-4154-a036-4978162c8b56"),FunctionId=Guid.Parse("2032eead-2715-4f84-9cb1-2e7bcadad2ab"),CreatedTime=DateTimeOffset.Parse("2023-08-03 15:28:43"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fbcde10a-a6d4-4ee6-a2fe-bd541bb91adf"),FunctionId=Guid.Parse("0ed7905f-7825-4f7f-8a00-18cd2da1e89b"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:32:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fbcde10a-a6d4-4ee6-a2fe-bd541bb91adf"),FunctionId=Guid.Parse("249577f0-a0e4-42ea-beed-2a035533933f"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:32:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("fbcde10a-a6d4-4ee6-a2fe-bd541bb91adf"),FunctionId=Guid.Parse("e8e5ebed-ed84-485a-8f51-3ea62ed13af8"),CreatedTime=DateTimeOffset.Parse("2023-08-13 11:32:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("54e062f1-d353-4a67-905e-f2cc5f14d689"),FunctionId=Guid.Parse("3942a4b5-0545-49af-b8b4-195249960b6b"),CreatedTime=DateTimeOffset.Parse("2023-10-08 15:09:35"),},
         };
        }
    }
}
