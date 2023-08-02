// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.Schedule;
using Gardener.EasyJob.Impl.Domains;
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
        /// <summary>
        /// 作业持久化（数据库）
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="logger"></param>
        public DbJobPersistence(IServiceScopeFactory serviceScopeFactory, ILogger<DbJobPersistence> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
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
                    jobDetailRepository.InsertNow(jobDetail.Adapt<SysJobDetail>());
                    break;

                case PersistenceBehavior.Updated:
                    jobDetailRepository.Where(u => u.JobId == jobDetail.JobId).ToList().ForEach(x =>
                    {
                        jobDetailRepository.UpdateNow(jobDetail.Adapt(x));
                    });
                    break;

                case PersistenceBehavior.Removed:
                    jobDetailRepository.Where(u => u.JobId == jobDetail.JobId).ToList().ForEach(x =>
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
                    jobTriggerRepository.InsertNow(jobTrigger.Adapt<SysJobTrigger>());
                    break;

                case PersistenceBehavior.Updated:
                    var list= jobTriggerRepository.Where(u => u.TriggerId == jobTrigger.TriggerId && u.JobId == jobTrigger.JobId).ToList();
                    foreach (var item in list)
                    {
                        SysJobTrigger newTrigger = jobTrigger.Adapt(item);
                        logger.LogInformation("----------------------------------------------");
                        logger.LogInformation(newTrigger.Status.ToString()+"___"+(uint)newTrigger.Status);
                        logger.LogInformation("----------------------------------------------");
                        jobTriggerRepository.UpdateNow(newTrigger);
                    }
                    break;

                case PersistenceBehavior.Removed:
                    jobTriggerRepository.Where(u => u.TriggerId == jobTrigger.TriggerId && u.JobId == jobTrigger.JobId).ToList().ForEach(x =>
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
            TypeAdapterConfig<SysJobDetail, JobDetail>
                .NewConfig()
                .Map(dest => dest.UpdatedTime,
                    src => src.UpdatedTime != null ? src.UpdatedTime.Value.DateTime : default(DateTime?));
            //映射关系 SysJobTrigger.UpdatedTime=>Trigger.UpdatedTime
            TypeAdapterConfig<SysJobTrigger, Trigger>
                .NewConfig()
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
            TypeAdapterConfig<JobDetail, SysJobDetail>
                .NewConfig()
                .Ignore(x => x.Id);
            //映射关系 忽略id
            TypeAdapterConfig<Trigger, SysJobTrigger>
                .NewConfig()
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

            // 遍历所有定义的作业
            foreach (var schedulerBuilder in allJobs)
            {
                // 获取作业信息构建器
                var jobBuilder = schedulerBuilder.GetJobBuilder();

                // 加载数据库数据
                var dbDetail = _jobRepository.FirstOrDefault(u => u.JobId == jobBuilder.JobId);
                if (dbDetail == null) continue;
                //转换为furion model
                JobDetail detail = dbDetail.Adapt<JobDetail>();
                // 同步数据库数据
                jobBuilder.LoadFrom(detail);
                var _triggerRepository = scope.ServiceProvider.GetRequiredService<IRepository<SysJobTrigger>>();
                // 遍历所有作业触发器
                foreach (var (_, triggerBuilder) in schedulerBuilder.GetEnumerable())
                {
                    // 加载数据库数据
                    var dbTrigger = _triggerRepository.FirstOrDefault(u => u.JobId == jobBuilder.JobId && u.TriggerId == triggerBuilder.TriggerId);
                    if (dbTrigger == null) continue;
                    Trigger trigger = dbTrigger.Adapt<Trigger>();
                    triggerBuilder.LoadFrom(trigger)
                                  .Updated();   // 标记更新
                }

                // 标记更新
                schedulerBuilder.Updated();
            }

            return allJobs;
        }
    }
}
