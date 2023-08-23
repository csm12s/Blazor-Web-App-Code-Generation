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
    /// 定时任务-用户配置服务
    /// </summary>
    [ScopedService]
    public class EasyJobUserConfigService : ISysJobUserConfigService
    {
        private static readonly string controller = "sys-job-user-config";

        private readonly IApiCaller apiCaller;

        public EasyJobUserConfigService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public Task<SysJobUserConfigDto?> GetMyConfig()
        {
            return apiCaller.GetAsync<SysJobUserConfigDto?>($"{controller}/my-config");
        }

        public Task<SysJobUserConfigDto?> SaveMyConfig(SysJobUserConfigDto config)
        {
            return apiCaller.PostAsync<SysJobUserConfigDto, SysJobUserConfigDto?>($"{controller}/save-my-config", config);
        }
    }
}
