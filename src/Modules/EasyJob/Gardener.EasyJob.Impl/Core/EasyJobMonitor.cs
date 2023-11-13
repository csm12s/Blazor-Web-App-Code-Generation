// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.Schedule;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Impl.Domains;
using Gardener.NotificationSystem.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mapster;
using System.Text.Json;

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// job执行监控
    /// </summary>
    public class EasyJobMonitor : IJobMonitor
    {
        private readonly ILogger<EasyJobMonitor> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// job执行监控
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="systemNotificationService"></param>
        public EasyJobMonitor(ILogger<EasyJobMonitor> logger, IServiceScopeFactory serviceScopeFactory, ISystemNotificationService systemNotificationService)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task OnExecutedAsync(JobExecutedContext context, CancellationToken stoppingToken)
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
                Succeeded = true,
                CreatedTime=DateTimeOffset.Now,
                JobDetailDescription = context.JobDetail.Description,
                JobTriggerDescription = context.Trigger.Description,
        };
            if (jobLog.TriggerStatus.Equals(Enums.TriggerStatus.ErrorToReady))
            {
                jobLog.Succeeded = false;
            }
            if (context.Exception != null)
            {
                jobLog.ExceptionMessage = context.Exception.Message;
                jobLog.Exception = context.Exception.InnerException?.ToString();
            }
            else 
            {
                jobLog.Result = trigger.Result;
            }
            SysJobLogDto logDto = jobLog.Adapt<SysJobLogDto>();
            #pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            systemNotificationService.SendToGroup(EasyJobConstant.EasyJobNotificationGroupName, new EasyJobRunLogNotificationData(logDto));
            #pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            //if (!string.IsNullOrWhiteSpace(context.JobDetail.Properties))
            //{
            //    JsonElement properties =System.Text.Json.JsonSerializer.SerializeToElement(context.JobDetail.Properties);
                //properties.c
                //JsonElement disableLogSaveEl= properties.GetProperty("disableLogSave")
                //if (properties.c)
                //bool disableLogSave = properties.TryGetProperty("disableLogSave").GetBoolean();
            //}
            using var factory = serviceScopeFactory.CreateScope();
            IRepository<SysJobLog> repository = factory.ServiceProvider.GetRequiredService<IRepository<SysJobLog>>();
            repository.InsertNow(jobLog);
            return Task.CompletedTask;

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
