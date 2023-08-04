using Gardener.Client.Base;
using Gardener.ToolBox.Dtos;
using Gardener.ToolBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.ToolBox.Client.Services
{
    /// <summary>
    /// Cron示例服务
    /// </summary>
    [ScopedService]
    public class CronExampleService : ICronExampleService
    {

        private static readonly string crotroller = "cron-example";

        private readonly IApiCaller apiCaller;

        public CronExampleService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public Task<CronCheckResult> Check(CronCheckInput checkInput)
        {
            return apiCaller.PostAsync<CronCheckInput, CronCheckResult>($"{crotroller}/check", checkInput);
        }

        public Task<IEnumerable<CronExample>> GetCronExamples()
        {
            return apiCaller.GetAsync<IEnumerable<CronExample>>($"{crotroller}/cron-examples");
        }
    }
}
