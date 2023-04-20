// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gardener.WoChat.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class WoChatImResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
               new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("0367ad11-0be0-48dd-a5a9-1d473b78c0bf"),CreatedTime=DateTimeOffset.Parse("2023-04-20 17:01:52"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("1adcacf3-33ae-4b36-b5c9-dcd95151ef3a"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("27d997d7-e691-4dbe-b5e0-74acbca53d98"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("333a3802-f8d2-4625-9476-dee8bf43fd0d"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("42636f5d-d5f6-4f64-bd2b-e77d80e51ff2"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("752b1623-8898-431f-ab3f-db9ebffae4e6"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("97b24035-cd96-4e28-bc42-103ac7e5fe3f"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("bdab8953-956d-4b1a-945b-b1806e9ac749"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:48:14"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("be639780-fbd0-4335-95e3-91e099d87850"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:22"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("d481028f-d67e-4ee0-b237-a4883c618486"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("ea2a64ac-bde9-45e5-a879-10b161b3f825"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
                new ResourceFunction() {ResourceId=Guid.Parse("19813cb6-00fc-478d-8fb4-36ac7e6fcf51"),FunctionId=Guid.Parse("f78ea06a-4c55-4445-9e16-bdc92c9b9fa6"),CreatedTime=DateTimeOffset.Parse("2023-04-20 16:21:03"),},
         };
        }
    }

}
