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
    /// 定时任务-集群服务
    /// </summary>
    [ScopedService]
    public class SysJobClusterService : ClientServiceBase<SysJobClusterDto,int>, ISysJobClusterService
    {
        public SysJobClusterService(IApiCaller apiCaller) : base(apiCaller, "sys-job-cluster")
        {
        }

        public Task<bool> Crash(JobClusterContext context)
        {
            return apiCaller.PostAsync<JobClusterContext, bool>($"{controller}/crash", context);
        }

        public Task<bool> Start(JobClusterContext context)
        {
            return apiCaller.PostAsync<JobClusterContext, bool>($"{controller}/start", context);
        }

        public Task<bool> Stop(JobClusterContext context)
        {
            return apiCaller.PostAsync<JobClusterContext, bool>($"{controller}/stop", context);
        }

        public Task<bool> Waiting(JobClusterContext context)
        {
            return apiCaller.PostAsync<JobClusterContext, bool>($"{controller}/waiting", context);
        }
    }
}
