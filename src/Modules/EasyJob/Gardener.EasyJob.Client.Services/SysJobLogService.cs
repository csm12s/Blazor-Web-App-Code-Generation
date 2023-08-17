// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Services;

namespace Gardener.EasyJob.Client.Services
{
    /// <summary>
    /// 定时任务-运行日志
    /// </summary>
    [ScopedService]
    public class SysJobLogService : ClientServiceBase<SysJobLogDto, long>, ISysJobLogService
    {
        /// <summary>
        /// 定时任务-运行日志
        /// </summary>
        public SysJobLogService(IApiCaller apiCaller) : base(apiCaller, "sys-job-log")
        {
        }

        public Task<IEnumerable<SysJobLogElapsedTime>> GetAvgElapsedTime(string? jobId = null, int? days = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>()
            {
                {nameof(jobId), jobId},
                {nameof(days), days},
            };
            return apiCaller.GetAsync<IEnumerable<SysJobLogElapsedTime>>($"{controller}/avg-elapsed-time", queryString);
        }

        public Task<SysJobLogCountAll> GetAllCount(string? jobId = null, int? days = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>()
            {
                {nameof(jobId), jobId},
                {nameof(days), days},
            };
            return apiCaller.GetAsync<SysJobLogCountAll>($"{controller}/all-count", queryString);
        }

        public Task<IEnumerable<SysJobLogCount>> GeCount(string? jobId = null, int? days = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>()
            {
                {nameof(jobId), jobId},
                {nameof(days), days},
            };
            return apiCaller.GetAsync<IEnumerable<SysJobLogCount>>($"{controller}/count", queryString);
        }
    }
}
