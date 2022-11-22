// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.NotificationSystem.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class FunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="示例服务",Service="聊天示例服务",Summary="获取聊天历史记录",Key="2A74937190C8E652BF107434EFFD1C17",Path="/api/chat-demo/history",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("8be6d20e-686c-4259-8eeb-3ec2b18739c3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }



}
