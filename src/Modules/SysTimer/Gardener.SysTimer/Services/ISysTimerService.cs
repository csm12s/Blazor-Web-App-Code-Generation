// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.SysTimer.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Services
{
    public interface ISysTimerService:Base.IServiceBase<SysTimerDto, int>
    {
        void AddTimerJob(SysTimerDto input);

        Task Start(SysTimerDto input);

        Task Stop(StopJobInput input);

        void StartTimerJob();

        /// <remarks>
        /// 获取所有本地任务
        /// </remarks>
        /// <returns></returns>
        Task<IEnumerable<TaskMethodInfo>> GetLocalJobs();
    }
}
