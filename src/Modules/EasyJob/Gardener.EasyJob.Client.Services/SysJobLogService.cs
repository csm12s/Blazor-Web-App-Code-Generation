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

        public Task<SysJobLogRunsNumberCount> GetRunsNumberCount(string? jobId = null, int? days = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>()
            {
                {nameof(jobId), jobId},
                {nameof(days), days},
            };
            return apiCaller.GetAsync<SysJobLogRunsNumberCount>($"{controller}/runs-number-count", queryString);
        }
    }
}
