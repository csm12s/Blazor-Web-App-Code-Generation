using Gardener.Client.Base;
using Gardener.ToolBox.Dtos;
using Gardener.ToolBox.Services;

namespace Gardener.ToolBox.Client.Services
{
    /// <summary>
    /// Cron示例服务
    /// </summary>
    [ScopedService]
    public class CronExampleService : ICronExampleService
    {

        private static readonly string controller = "cron-example";

        private readonly IApiCaller apiCaller;

        public CronExampleService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public Task<CronCheckResult> Check(CronCheckInput checkInput)
        {
            return apiCaller.PostAsync<CronCheckInput, CronCheckResult>($"{controller}/check", checkInput);
        }

        public Task<IEnumerable<CronExample>> GetCronExamples()
        {
            return apiCaller.GetAsync<IEnumerable<CronExample>>($"{controller}/cron-examples");
        }
    }
}
