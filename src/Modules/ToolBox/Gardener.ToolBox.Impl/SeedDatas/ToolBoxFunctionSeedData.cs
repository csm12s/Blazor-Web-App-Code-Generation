// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using HttpMethod = Gardener.Enums.HttpMethod;
namespace Gardener
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ToolBoxFunctionSeedData : IEntitySeedData<Function>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Function> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Function() {Group="工具箱服务",Service="Cron示例服务",Summary="获取Cron示例列表",Key="AEC8E34F7F635293D50B7FA1E0CFD708",Description="cron-examples",Path="/api/cron-example/cron-examples",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("c2895c9c-11ac-49c2-ace5-eb622299f8f8"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-04 14:37:14"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
                new Function() {Group="工具箱服务",Service="Cron示例服务",Summary="检验",Key="9F97C43289E4D6001C4D080FA0D444D0",Description="check",Path="/api/cron-example/check",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=false,Id=Guid.Parse("a17564d2-df94-493f-9a6c-fd7c35ff7e0a"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-04 14:37:14"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),UpdatedTime=DateTimeOffset.Parse("2023-08-04 14:37:22"),UpdateBy="1",UpdateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }

}
