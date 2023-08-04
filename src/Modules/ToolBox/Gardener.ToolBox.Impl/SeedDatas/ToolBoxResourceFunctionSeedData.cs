// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gardener.ToolBox.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ToolBoxResourceFunctionSeedData : IEntitySeedData<ResourceFunction>
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
                new ResourceFunction() {ResourceId=Guid.Parse("ceeb4c42-06a6-4635-b94f-8ed4ee026954"),FunctionId=Guid.Parse("a17564d2-df94-493f-9a6c-fd7c35ff7e0a"),CreatedTime=DateTimeOffset.Parse("2023-08-04 14:38:48"),},
                new ResourceFunction() {ResourceId=Guid.Parse("ceeb4c42-06a6-4635-b94f-8ed4ee026954"),FunctionId=Guid.Parse("c2895c9c-11ac-49c2-ace5-eb622299f8f8"),CreatedTime=DateTimeOffset.Parse("2023-08-04 14:38:48"),},
         };
        }
    }

}
