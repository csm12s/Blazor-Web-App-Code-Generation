// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Resources;
using Gardener.EasyJob.Services;
using Gardener.Enums;
using Mapster;

namespace Gardener.EasyJob.Client.Services
{
    /// <summary>
    /// 定时任务-触发器服务
    /// </summary>
    [ScopedService]
    public class SysJobTriggerService : ClientServiceBase<SysJobTriggerDto, int>, ISysJobTriggerService
    {
        public SysJobTriggerService(IApiCaller apiCaller) : base(apiCaller, "sys-job-trigger")
        {
        }

        public Task<bool> Pause(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/{id}/pause");
        }

        public Task<bool> Start(int id)
        {
            return apiCaller.PostWithoutBodyAsync<bool>($"{controller}/{id}/start");
        }
    }
}
