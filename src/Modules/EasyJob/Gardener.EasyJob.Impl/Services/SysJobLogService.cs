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

        private readonly ISysJobDetailService jobDetailService;

        /// <summary>
        /// 定时任务-执行日志
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="jobDetailService"></param>
        public SysJobLogService(IRepository<SysJobLog, MasterDbContextLocator> repository, ISysJobDetailService jobDetailService) : base(repository)
        {
            this.jobDetailService = jobDetailService;
        }


        /// <summary>
        /// 获取总运行次数统计
        /// </summary>
        /// <remarks>
        /// 获取总运行次数统计
        /// </remarks>
        /// <param name="jobId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<SysJobLogAllCount> GetAllCount([FromQuery] string? jobId = null, [FromQuery] int? days = null)
        {
            IQueryable<SysJobLog> query = base._repository.AsQueryable(false);
            if (!string.IsNullOrEmpty(jobId))
            {
                query = query.Where(x => x.JobId.Equals(jobId));
            }
            if (days.HasValue)
            {
                DateTimeOffset beginTime = DateTimeOffset.Now.Date.AddDays((double)(days * -1));
                query = query.Where(x => x.CreatedTime >= beginTime);
            }
            List<KeyValuePair<bool, int>> counts = await query.GroupBy(x => x.Succeeded, (k, g) => new KeyValuePair<bool, int>(k, g.Count())).ToListAsync();

            SysJobLogAllCount result = new SysJobLogAllCount()
            {
                Succeed = counts.Where(x => x.Key == true).Sum(x => x.Value),
                Fail = counts.Where(x => x.Key == false).Sum(x => x.Value),
            };
            result.Total = result.Succeed + result.Fail;
            return result;
        }

        /// <summary>
        /// 获取评价耗时
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        /// <remarks>
        /// 获取评价耗时
        /// </remarks>
        public async Task<IEnumerable<SysJobLogElapsedTime>> GetAvgElapsedTime([FromQuery] string? jobId = null, [FromQuery] int? days = null)
        {
            List<SysJobDetailDto> jobs = await jobDetailService.GetAllUsable();

            IQueryable<SysJobLog> query = base._repository.AsQueryable(false);
            if (!string.IsNullOrEmpty(jobId))
            {
                query = query.Where(x => x.JobId.Equals(jobId));
            }
            Func<SysJobLog, object> groupFunc = x => new { x.JobId, x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day };
            if (days.HasValue)
            {
                DateTimeOffset beginTime = DateTimeOffset.Now.Date.AddDays((double)(days * -1));
                query = query.Where(x => x.CreatedTime >= beginTime);
                if (days == 0)
                {
                    groupFunc = x => new { x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day, x.CreatedTime.Hour, x.JobId };
                }
            }

            List<KeyValuePair<dynamic, double>> counts = query.GroupBy(groupFunc, (k, g) => new KeyValuePair<dynamic, double>(k, g.Average(c => c.ElapsedTime))).ToList();

            if (!counts.Any())
            {
                return new List<SysJobLogElapsedTime>();
            }
            List<SysJobLogElapsedTime> result = new List<SysJobLogElapsedTime>();

            foreach (var item in counts)
            {
                string time = new DateTime(item.Key.Year, item.Key.Month, item.Key.Day).ToString("yyyy-MM-dd");
                if (days.HasValue && days == 0)
                {
                    time = new DateTime(item.Key.Year, item.Key.Month, item.Key.Day, item.Key.Hour, 0, 0).ToString("HH:mm");
                }
                SysJobLogElapsedTime elapsedTime = new SysJobLogElapsedTime(time, item.Key.JobId, (long)Math.Round(item.Value));
                var job = jobs?.FirstOrDefault(x => x.JobId.Equals(elapsedTime.JobName));
                if (job != null && !string.IsNullOrWhiteSpace(job.Description))
                {
                    elapsedTime.JobName = $"{job.Description}({job.JobId})";
                }
                result.Add(elapsedTime);
            }

            return result.OrderBy(x => x.Time);
        }

        /// <summary>
        /// 获取运行次数统计
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        /// <remarks>
        /// 获取运行次数统计
        /// </remarks>
        public async Task<IEnumerable<SysJobLogCount>> GetCount([FromQuery] string? jobId = null, [FromQuery] int? days = null)
        {
            List<SysJobDetailDto> jobs = await jobDetailService.GetAllUsable();
            IQueryable<SysJobLog> query = base._repository.AsQueryable(false);
            if (!string.IsNullOrEmpty(jobId))
            {
                query = query.Where(x => x.JobId.Equals(jobId));
            }
            Func<SysJobLog, object> groupFunc = x => new { x.Succeeded, x.JobId, x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day };
            if (days.HasValue)
            {
                DateTimeOffset beginTime = DateTimeOffset.Now.Date.AddDays((double)(days * -1));
                query = query.Where(x => x.CreatedTime >= beginTime);
                if (days == 0)
                {
                    groupFunc = x => new { x.Succeeded, x.JobId, x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day, x.CreatedTime.Hour };
                }
            }

            List<KeyValuePair<dynamic, long>> counts = query.GroupBy(groupFunc, (k, g) => new KeyValuePair<dynamic, long>(k, g.Count())).ToList();

            if (!counts.Any())
            {
                return new List<SysJobLogCount>();
            }

            Dictionary<string, SysJobLogCount> result = new Dictionary<string, SysJobLogCount>();
            foreach (var item in counts)
            {
                

                string time = new DateTime(item.Key.Year, item.Key.Month, item.Key.Day).ToString("yyyy-MM-dd");
                if (days.HasValue && days == 0)
                {
                    time = new DateTime(item.Key.Year, item.Key.Month, item.Key.Day, item.Key.Hour, 0, 0).ToString("HH:mm");
                }
                SysJobLogCount count;
                string itemKey=time+"_" + item.Key.JobId;
                if (result.ContainsKey(itemKey))
                {
                    count = result[itemKey];
                }
                else
                {
                    count = new SysJobLogCount(item.Key.JobId, time);
                    result.Add(itemKey, count);
                }
                if (item.Key.Succeeded)
                {
                    count.Succeed += item.Value;
                }
                else
                {
                    count.Fail += item.Value;
                }
                count.Total += item.Value;

                var job = jobs?.FirstOrDefault(x => x.JobId.Equals(count.JobName));
                if (job != null && !string.IsNullOrWhiteSpace(job.Description))
                {
                    count.JobName = $"{job.Description}({job.JobId})";
                }
            }

            return result.Values.OrderBy(x => x.Time);
        }

    }
}
