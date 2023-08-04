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
    public class EasyJobUserConfigService : IEasyJobUserConfigService
    {
        private static readonly string controller = "easy-job-user-config";

        private readonly IApiCaller apiCaller;

        public EasyJobUserConfigService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public Task<EasyJobUserConfigDto?> GetMyConfig()
        {
            return apiCaller.GetAsync<EasyJobUserConfigDto?>($"{controller}/my-config");
        }

        public Task<EasyJobUserConfigDto?> SaveMyConfig(EasyJobUserConfigDto config)
        {
            return apiCaller.PostAsync<EasyJobUserConfigDto, EasyJobUserConfigDto?>($"{controller}/save-my-config", config);
        }
    }
}
