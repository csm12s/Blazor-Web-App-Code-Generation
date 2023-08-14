// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.EasyJob.Dtos;

namespace Gardener.EasyJob.Services
{
    /// <summary>
    /// 定时任务日志服务
    /// </summary>
    public interface ISysJobLogService : IServiceBase<SysJobLogDto, long>
    {
        /// <summary>
        /// 获取运行次数统计
        /// </summary>
        /// <remarks>
        /// 获取任务运行次数统计
        /// </remarks>
        /// <param name="jobId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        Task<SysJobLogRunsNumberCount> GetRunsNumberCount(string? jobId = null, int? days = null);
    }
}
