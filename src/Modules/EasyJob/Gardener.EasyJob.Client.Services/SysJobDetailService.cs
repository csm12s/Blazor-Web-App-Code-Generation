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
    /// 定时任务-任务服务
    /// </summary>
    [ScopedService]
    public class SysJobDetailService : ClientServiceBase<SysJobDetailDto, int>, ISysJobDetailService
    {
        public SysJobDetailService(IApiCaller apiCaller) : base(apiCaller, "sys-job-detail")
        {
        }

        public Task<bool> CancelSleep()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/crash");
        }
        public Task<IEnumerable<SysJobTriggerDto>> GetTriggers(int id)
        {
            return apiCaller.GetAsync<IEnumerable<SysJobTriggerDto>>($"{controller}/{id}/triggers");
        }

        public Task<bool> Pause(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/{id}/pause");
        }

        public Task<bool> PauseAll()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/pause-all");
        }

        public Task<bool> PersistAll()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/persist-all");
        }

        public Task<bool> Start(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/{id}/start");
        }

        public Task<bool> StartAll()
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/start-all");
        }
    }
}
