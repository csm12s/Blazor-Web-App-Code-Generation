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

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// 作业持久化（数据库）
    /// </summary>
    public class DbJobPersistence : IJobPersistence
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        /// <summary>
        /// 作业持久化（数据库）
        /// </summary>
        public DbJobPersistence(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 作业计划Scheduler的JobDetail变化时
        /// </summary>
        /// <param name="context"></param>
        public void OnChanged(PersistenceContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var jobDetailRepository = scope.ServiceProvider.GetRequiredService<IRepository<SysJobDetail>>();

            var jobDetail = context.JobDetail.Adapt<SysJobDetail>();
            switch (context.Behavior)
            {
                case PersistenceBehavior.Appended:
                    jobDetailRepository.Insert(jobDetail);
                    break;

                case PersistenceBehavior.Updated:
                    jobDetailRepository.Where(u => u.JobId == jobDetail.JobId).ToList().ForEach(x => {
                        jobDetailRepository.Update(x);
                    });
                    break;

                case PersistenceBehavior.Removed:
                    jobDetailRepository.Where(u => u.JobId == jobDetail.JobId).ToList().ForEach(x => {
                        jobDetailRepository.Delete(x);
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

            var jobTrigger = context.Trigger.Adapt<SysJobTrigger>();
            switch (context.Behavior)
            {
                case PersistenceBehavior.Appended:
                    jobTriggerRepository.Insert(jobTrigger);
                    break;

                case PersistenceBehavior.Updated:
                    jobTriggerRepository.Where(u => u.TriggerId == jobTrigger.TriggerId && u.JobId == jobTrigger.JobId).ToList().ForEach(x => {
                        jobTriggerRepository.Update(x);
                    });
                    break;

                case PersistenceBehavior.Removed:
                    jobTriggerRepository.Where(u => u.TriggerId == jobTrigger.TriggerId && u.JobId == jobTrigger.JobId).ToList().ForEach(x => {
                        jobTriggerRepository.Delete(x);
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

                // 同步数据库数据
                jobBuilder.LoadFrom(dbDetail);
                var _triggerRepository = scope.ServiceProvider.GetRequiredService<IRepository<SysJobTrigger>>();
                // 遍历所有作业触发器
                foreach (var (_, triggerBuilder) in schedulerBuilder.GetEnumerable())
                {
                    // 加载数据库数据
                    var dbTrigger = _triggerRepository.FirstOrDefault(u => u.JobId == jobBuilder.JobId && u.TriggerId == triggerBuilder.TriggerId);
                    if (dbTrigger == null) continue;

                    triggerBuilder.LoadFrom(dbTrigger)
                                  .Updated();   // 标记更新
                }

                // 标记更新
                schedulerBuilder.Updated();
            }

            return allJobs;
        }
    }
}
