// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gardener.EasyJob.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class CodeTypeSeedData : IEntitySeedData<CodeType>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<CodeType> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new CodeType() {CodeTypeName="定时任务统计查询日期",CodeTypeValue="easy_job_count_query_date",Id=4,IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2023-08-13 13:04:27"),CreateBy="1",CreateIdentityType=Enum.Parse<IdentityType>("User"),},
         };
        }
    }

}
