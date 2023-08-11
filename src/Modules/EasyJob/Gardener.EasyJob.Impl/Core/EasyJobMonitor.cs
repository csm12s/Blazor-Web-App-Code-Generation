// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.Schedule;
using Gardener.EasyJob.Impl.Domains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// job执行监控
    /// </summary>
    public class EasyJobMonitor : IJobMonitor
    {
        private readonly ILogger<EasyJobMonitor> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        /// <summary>
        /// <summary>
        /// job执行监控
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        public EasyJobMonitor(ILogger<EasyJobMonitor> logger, IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnExecutedAsync(JobExecutedContext context, CancellationToken stoppingToken)
        {
            Trigger trigger = context.Trigger;
            SysJobLog jobLog = new SysJobLog()
            {
                JobId = context.JobId,
                TriggerId = context.TriggerId,
                TriggerStatus = (Enums.TriggerStatus)((uint)trigger.Status),
                LastRunTime = trigger.LastRunTime,
                NextRunTime = trigger.NextRunTime,
                ElapsedTime = trigger.ElapsedTime,
                NumberOfErrors = trigger.NumberOfErrors,
                NumberOfRuns = trigger.NumberOfRuns,
                Result = trigger.Result,
                Succeeded=true
            };
            if (jobLog.TriggerStatus.Equals(Enums.TriggerStatus.ErrorToReady))
            {
                jobLog.Succeeded = false;
            }
            if(context.Exception != null)
            {
                jobLog.ExceptionMessage = context.Exception.Message;
                jobLog.Exception = context.Exception.InnerException?.ToString();
            }
            using var factory = serviceScopeFactory.CreateScope();
            IRepository<SysJobLog> repository = factory.ServiceProvider.GetRequiredService<IRepository<SysJobLog>>();
            await repository.InsertNowAsync(jobLog);
        }

        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task OnExecutingAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {

            return Task.CompletedTask;
        }
    }
}
