﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.Schedule;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Dtos.Notification;
using Gardener.EasyJob.Impl.Domains;
using Gardener.NotificationSystem.Core;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// 作业持久化（数据库）
    /// </summary>
    public class DbJobPersistence : IJobPersistence
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<DbJobPersistence> logger;
        private readonly ISystemNotificationService _systemNotificationService;
        private readonly static string[] updateTriggerFields = new string[]{
                                    nameof(SysJobTrigger.UpdatedTime),
                                    nameof(SysJobTrigger.Status),
                                    nameof(SysJobTrigger.ElapsedTime),
                                    nameof(SysJobTrigger.NumberOfErrors),
                                    nameof(SysJobTrigger.NumberOfRuns),
                                    nameof(SysJobTrigger.LastRunTime),
                                    nameof(SysJobTrigger.NextRunTime),
                                    nameof(SysJobTrigger.Result)
        };
        /// <summary>
        /// 作业持久化（数据库）
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="logger"></param>
        /// <param name="systemNotificationService"></param>
        public DbJobPersistence(IServiceScopeFactory serviceScopeFactory, ILogger<DbJobPersistence> logger, ISystemNotificationService systemNotificationService)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
            this._systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 作业计划Scheduler的JobDetail变化时
        /// </summary>
        /// <param name="context"></param>
        public void OnChanged(PersistenceContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var jobDetailRepository = scope.ServiceProvider.GetRequiredService<IRepository<SysJobDetail>>();

            var jobDetail = context.JobDetail;
            switch (context.Behavior)
            {
                case PersistenceBehavior.Appended:
                    //保留，用于首次扫描内部程序自动保存db
                    if (!jobDetailRepository.Any(x => x.JobId.Equals(jobDetail.JobId)))
                    {
                        jobDetailRepository.InsertNow(jobDetail.Adapt<SysJobDetail>());
                    }
                    break;

                case PersistenceBehavior.Updated:
                    //运行时一直更新更新时间,且业务字段这里也更新不了，所以jobDetail不在这里更新了
                    //jobDetailRepository.Where(u => u.JobId == jobDetail.JobId).ToList().ForEach(x =>
                    //{
                    //    jobDetailRepository.UpdateNow(jobDetail.Adapt(x));
                    //});
                    break;

                case PersistenceBehavior.Removed:
                    //删除
                    jobDetailRepository.AsQueryable(false).Where(u => u.JobId == jobDetail.JobId).ToList().ForEach(x =>
                    {
                        jobDetailRepository.DeleteNow(x);
                    });
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 作业计划初始化通知
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public SchedulerBuilder OnLoading(SchedulerBuilder builder)
        {

            var sql = builder.GetJobBuilder().ConvertToSQL("test", PersistenceBehavior.Appended);

            //从数据库加载
            return builder;
        }
        /// <summary>
        /// 作业计划Scheduler的触发器Trigger变化时
        /// </summary>
        /// <param name="context"></param>
        public void OnTriggerChanged(PersistenceTriggerContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var jobTriggerRepository = scope.ServiceProvider.GetRequiredService<IRepository<SysJobTrigger>>();

            var jobTrigger = context.Trigger;
            switch (context.Behavior)
            {
                case PersistenceBehavior.Appended:
                    if (!jobTriggerRepository.Any(x => x.TriggerId.Equals(jobTrigger.TriggerId)))
                    {
                        jobTriggerRepository.InsertNow(jobTrigger.Adapt<SysJobTrigger>());
                    }
                    break;

                case PersistenceBehavior.Updated:
                    var dbTrigger = jobTriggerRepository.AsQueryable(false)
                        .Where(u => u.TriggerId == jobTrigger.TriggerId && u.JobId == jobTrigger.JobId)
                        .Select(x =>
                            new SysJobTrigger()
                            {
                                Id = x.Id,
                                Status = x.Status
                            }
                    ).FirstOrDefault();
                    if (dbTrigger == null)
                    {
                        break;
                    }

                    SysJobTrigger newTrigger = jobTrigger.Adapt(dbTrigger);
                    jobTriggerRepository.UpdateIncludeNow(newTrigger, updateTriggerFields);
                    _systemNotificationService.SendToGroup(EasyJobConstant.EasyJobNotificationGroupName, new EasyJobTriggerUpdateNotificationData(newTrigger.Adapt<SysJobTriggerDto>()));
                    break;
                case PersistenceBehavior.Removed:
                    jobTriggerRepository.AsQueryable(false).Where(u => u.TriggerId == jobTrigger.TriggerId && u.JobId == jobTrigger.JobId).ToList().ForEach(x =>
                    {
                        jobTriggerRepository.DeleteNow(x);
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 作业调度服务启动时
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SchedulerBuilder> Preload()
        {
            //映射关系 SysJobDetail.UpdatedTime=>JobDetail.UpdatedTime
            TypeAdapterConfig.GlobalSettings.ForType<SysJobDetail, JobDetail>()
                .Map(dest => dest.UpdatedTime,
                    src => src.UpdatedTime != null ? src.UpdatedTime.Value.DateTime : default(DateTime?));
            //映射关系 SysJobTrigger.UpdatedTime=>Trigger.UpdatedTime
            TypeAdapterConfig.GlobalSettings.ForType<SysJobTrigger, Trigger>()
                .Map(dest => dest.UpdatedTime,
                    src => src.UpdatedTime != null ? src.UpdatedTime.Value.DateTime : default(DateTime?))
                .Map(dest => dest.NextRunTime,
                    src => src.NextRunTime != null ? src.NextRunTime.Value.DateTime : default(DateTime?))
                .Map(dest => dest.LastRunTime,
                    src => src.LastRunTime != null ? src.LastRunTime.Value.DateTime : default(DateTime?))
                .Map(dest => dest.StartTime,
                    src => src.StartTime != null ? src.StartTime.Value.DateTime : default(DateTime?))
                .Map(dest => dest.EndTime,
                    src => src.EndTime != null ? src.EndTime.Value.DateTime : default(DateTime?));
            //映射关系 忽略id
            TypeAdapterConfig.GlobalSettings.ForType<JobDetail, SysJobDetail>()
                .Ignore(x => x.Id);
            //映射关系 忽略id
            TypeAdapterConfig.GlobalSettings.ForType<Trigger, SysJobTrigger>()
                .Ignore(x => x.Id)
                .Map(dest => dest.NextRunTime,
                    src => src.NextRunTime != null ? new DateTimeOffset(src.NextRunTime.Value) : default(DateTimeOffset?))
                .Map(dest => dest.LastRunTime,
                    src => src.LastRunTime != null ? new DateTimeOffset(src.LastRunTime.Value) : default(DateTimeOffset?))
                .Map(dest => dest.StartTime,
                    src => src.StartTime != null ? new DateTimeOffset(src.StartTime.Value) : default(DateTimeOffset?))
                .Map(dest => dest.EndTime,
                    src => src.EndTime != null ? new DateTimeOffset(src.EndTime.Value) : default(DateTimeOffset?));
            // 获取所有定义的作业
            var allJobs = App.EffectiveTypes.ScanToBuilders();
            using var scope = _serviceScopeFactory.CreateScope();
            var _jobRepository = scope.ServiceProvider.GetRequiredService<IRepository<SysJobDetail>>();
            // 若数据库不存在任何作业，则直接返回
            if (!_jobRepository.Any(u => true)) return allJobs;
            var _triggerRepository = scope.ServiceProvider.GetRequiredService<IRepository<SysJobTrigger>>();
            // 遍历所有程序中定义的作业
            foreach (var schedulerBuilder in allJobs)
            {
                // 获取作业信息构建器
                var jobBuilder = schedulerBuilder.GetJobBuilder();

                // 加载数据库数据
                var dbDetail = _jobRepository.AsQueryable(false).FirstOrDefault(u => u.JobId == jobBuilder.JobId);
                if (dbDetail == null) continue;
                //转换为furion model
                JobDetail detail = dbDetail.Adapt<JobDetail>();
                // 同步数据库数据
                jobBuilder.LoadFrom(detail);
                // 遍历所有作业触发器
                foreach (var (_, triggerBuilder) in schedulerBuilder.GetEnumerable())
                {
                    // 加载数据库数据
                    var dbTrigger = _triggerRepository.AsQueryable(false).FirstOrDefault(u => u.JobId == jobBuilder.JobId && u.TriggerId == triggerBuilder.TriggerId);
                    if (dbTrigger == null) continue;
                    Trigger trigger = dbTrigger.Adapt<Trigger>();
                    triggerBuilder.LoadFrom(trigger)
                                  .Updated();   // 标记更新
                }

                // 标记更新
                schedulerBuilder.Updated();
            }
            var dbJobs = _jobRepository.AsQueryable(false).Where(x => x.IsLocked == false && x.IsDeleted == false).ToList();
            SchedulerLoader schedulerLoader = scope.ServiceProvider.GetRequiredService<SchedulerLoader>();
            List<SchedulerBuilder> noFindSchedulerBuilders = new List<SchedulerBuilder>();
            foreach (var dbJob in dbJobs)
            {
                if (allJobs.Any(x => x.GetJobBuilder().JobId.Equals(dbJob.JobId))) { continue; }
                List<TriggerBuilder> triggers = new List<TriggerBuilder>();
                var dbTriggers = _triggerRepository.Where(u => u.JobId == dbJob.JobId).ToList();
                foreach (var trigger in dbTriggers)
                {
                    triggers.Add(TriggerBuilder.From(trigger.Adapt<Trigger>()).Appended());
                }
                //jobBuilder
                JobBuilder jobBuilder = schedulerLoader.CreateJobBuilder(dbJob);
                //重新构建后，运行类相关字段会变化
                _jobRepository.UpdateNow(dbJob);
                //schedulerBuilder
                var schedulerBuilder = SchedulerBuilder.Create(jobBuilder, triggers.ToArray());
                noFindSchedulerBuilders.Add(schedulerBuilder);

            }
            noFindSchedulerBuilders.AddRange(allJobs);
            return noFindSchedulerBuilders;
        }
    }
}
