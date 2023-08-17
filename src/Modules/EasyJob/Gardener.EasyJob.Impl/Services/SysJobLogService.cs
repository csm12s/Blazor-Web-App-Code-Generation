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
        /// 获取总运行次数统计
        /// </summary>
        /// <remarks>
        /// 获取总运行次数统计
        /// </remarks>
        /// <param name="jobId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<SysJobLogCountAll> GetAllCount([FromQuery] string? jobId = null, [FromQuery] int? days = null)
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

            SysJobLogCountAll result = new SysJobLogCountAll()
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
        public Task<IEnumerable<SysJobLogElapsedTime>> GetAvgElapsedTime([FromQuery] string? jobId = null, [FromQuery] int? days = null)
        {
            IQueryable<SysJobLog> query = base._repository.AsQueryable(false);
            if (!string.IsNullOrEmpty(jobId))
            {
                query = query.Where(x => x.JobId.Equals(jobId));
            }
            Func<SysJobLog, object> groupFunc = x => new { x.JobId, x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day };
            if (days.HasValue)
            {
                DateTimeOffset beginTime = DateTimeOffset.Now.AddDays((double)(days * -1));
                query = query.Where(x => x.CreatedTime >= beginTime);
                if (days == 1)
                {
                    groupFunc = x => new { x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day, x.CreatedTime.Hour, x.JobId};
                }
            }

            List<KeyValuePair<dynamic, double>> counts = query.GroupBy(groupFunc, (k, g) => new KeyValuePair<dynamic, double>(k, g.Average(c => c.ElapsedTime))).ToList();

            if (!counts.Any())
            {
                return Task.FromResult<IEnumerable<SysJobLogElapsedTime>>(new List<SysJobLogElapsedTime>());
            }
            List<SysJobLogElapsedTime> result = new List<SysJobLogElapsedTime>();

            foreach (var item in counts)
            {
                string group = $"{item.Key.Year}/{item.Key.Month}/{item.Key.Day}";
                if (days.HasValue && days == 1)
                {
                    group = $"{item.Key.Year}/{item.Key.Month}/{item.Key.Day} {item.Key.Hour}";
                }
                SysJobLogElapsedTime time = new SysJobLogElapsedTime(group, item.Key.JobId, item.Value);
                result.Add(time);
            }

            return Task.FromResult<IEnumerable<SysJobLogElapsedTime>>(result.OrderBy(x=>x.Group));
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
        public Task<IEnumerable<SysJobLogCount>> GeCount([FromQuery] string? jobId = null, [FromQuery] int? days = null)
        {
            IQueryable<SysJobLog> query = base._repository.AsQueryable(false);
            if (!string.IsNullOrEmpty(jobId))
            {
                query = query.Where(x => x.JobId.Equals(jobId));
            }
            Func<SysJobLog, object> groupFunc = x => new { x.Succeeded, x.JobId, x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day };
            if (days.HasValue)
            {
                DateTimeOffset beginTime = DateTimeOffset.Now.AddDays((double)(days * -1));
                query = query.Where(x => x.CreatedTime >= beginTime);
                if (days == 1)
                {
                    groupFunc = x => new { x.Succeeded, x.JobId, x.CreatedTime.Year, x.CreatedTime.Month, x.CreatedTime.Day, x.CreatedTime.Hour };
                }
            }

            List<KeyValuePair<dynamic, long>> counts = query.GroupBy(groupFunc, (k, g) => new KeyValuePair<dynamic, long>(k, g.Count())).ToList();

            if (!counts.Any())
            {
                return Task.FromResult<IEnumerable<SysJobLogCount>>(new List<SysJobLogCount>());
            }

            Dictionary<string, SysJobLogCount> result = new Dictionary<string, SysJobLogCount>();
            foreach (var item in counts)
            {
                string group = $"{item.Key.Year}/{item.Key.Month}/{item.Key.Day}";
                if (days.HasValue && days == 1)
                {
                    group = $"{item.Key.Year}/{item.Key.Month}/{item.Key.Day} {item.Key.Hour}";
                }
                SysJobLogCount time;
                if (result.ContainsKey(group))
                {
                    time = result[group];
                }
                else 
                {
                    time = new SysJobLogCount(group, item.Key.JobId);
                    result.Add(group, time);
                }
                if (item.Key.Succeeded)
                {
                    time.Succeed+=item.Value;
                }
                else
                { 
                    time.Fail += item.Value;
                }
                time.Total += item.Value;
            }

            return Task.FromResult<IEnumerable<SysJobLogCount>>(result.Values.OrderBy(x => x.Group));
        }

    }
}
