// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Impl.Domains;
using Gardener.EasyJob.Services;
using Gardener.EntityFramwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gardener.EasyJob.Impl.Services
{
    /// <summary>
    /// 定时任务-执行日志
    /// </summary>
    [ApiDescriptionSettings("EasyJobServices")]
    public class SysJobLogService : ServiceBase<SysJobLog, SysJobLogDto, long>, ISysJobLogService
    {
        /// <summary>
        /// 定时任务-执行日志
        /// </summary>
        /// <param name="repository"></param>
        public SysJobLogService(IRepository<SysJobLog, MasterDbContextLocator> repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取运行次数统计
        /// </summary>
        /// <remarks>
        /// 获取任务运行次数统计
        /// </remarks>
        /// <param name="jobId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<SysJobLogRunsNumberCount> GetRunsNumberCount([FromQuery] string? jobId = null, [FromQuery] int? days = null)
        {
            IQueryable<SysJobLog> query = base._repository.AsQueryable(false);
            if (!string.IsNullOrEmpty(jobId))
            {
                query = query.Where(x => x.JobId.Equals(jobId));
            }
            if (days.HasValue)
            {
                DateTimeOffset beginTime = DateTimeOffset.Now.AddDays((double)(days * -1));
                query = query.Where(x => x.CreatedTime >= beginTime);
            }
            List<KeyValuePair<bool, int>> counts = await query.GroupBy(x => x.Succeeded, (k, g) => new KeyValuePair<bool, int>(k, g.Count())).ToListAsync();

            SysJobLogRunsNumberCount result = new SysJobLogRunsNumberCount()
            {
                Succeed = counts.Where(x => x.Key == true).Sum(x => x.Value),
                Fail = counts.Where(x => x.Key == false).Sum(x => x.Value),
            };
            result.Total = result.Succeed + result.Fail;
            return result;
        }

    }
}
