// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.Schedule;
using Gardener.EasyJob.Enums;
using Gardener.EasyJob.Impl.Domains;
using Gardener.EasyJob.Resources;
using Gardener.Enums;
using Gardener.LocalizationLocalizer;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class SchedulerLoader : IScoped
    {

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly DynamicJobCompiler _dynamicJobCompiler;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerFactory"></param>
        /// <param name="dynamicJobCompiler"></param>
        public SchedulerLoader(ISchedulerFactory schedulerFactory, DynamicJobCompiler dynamicJobCompiler)
        {
            _schedulerFactory = schedulerFactory;
            _dynamicJobCompiler = dynamicJobCompiler;
        }
        /// <summary>
        /// 加载job到调度
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="triggerBuilders"></param>
        public void AddJob(SysJobDetail jobDetail, params TriggerBuilder[] triggerBuilders)
        {
            // 动态创建作业
            Type? jobType;
            switch (jobDetail.CreateType)
            {
                case JobCreateType.Script when string.IsNullOrEmpty(jobDetail.ScriptCode):
                    throw Oops.Oh(ExceptionCode.Field_Required, nameof(jobDetail.ScriptCode));
                case JobCreateType.Script:
                    {
                        jobType = _dynamicJobCompiler.BuildJob(jobDetail.ScriptCode);
                        if (jobType == null)
                        {
                            throw Oops.Oh(EasyJobExceptionCode.Script_Code_Compile_Fail);
                        }
                        if (jobType.GetCustomAttributes(typeof(JobDetailAttribute), false).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
                            throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobDetail_Not_Find);
                        if (jobDetailAttribute.JobId != jobDetail.JobId)
                            throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobId_Inconsistency);
                        jobDetail.AssemblyName = jobType.Assembly.GetName().Name;
                        break;
                    }
                case JobCreateType.Http:
                    jobType = typeof(HttpJob);
                    break;

                default:
                    throw new NotSupportedException();
            }
            var job = JobBuilder
                    .Create(jobType)
                    .LoadFrom(jobDetail.Adapt<JobDetail>())
                    .SetJobType(jobType);
            //加载 job
            _schedulerFactory.AddJob(job);
            if(triggerBuilders.Length > 0)
            {
                //获取job调度
                var scheduler = _schedulerFactory.GetJob(job.JobId);
                //获取job现有触发器
                var runTriggers = scheduler.GetTriggers();
                foreach (var trigger in triggerBuilders)
                {
                    if (runTriggers.Any(x => x.TriggerId.Equals(trigger.TriggerId))){ continue; }
                    //添加不存在的触发器
                    scheduler.AddTrigger(trigger);
                }
            }    
        }

        /// <summary>
        /// 更新job到调度
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="oldJobDetail"></param>
        public void UpdateJob(SysJobDetail jobDetail, SysJobDetail oldJobDetail)
        {
            var scheduler = _schedulerFactory.GetJob(jobDetail.JobId);
            if (scheduler == null)
            {
                throw Oops.Oh(ExceptionCode.Scheduler_Not_Find);
            }
            var oldScriptCode = oldJobDetail.ScriptCode;//旧脚本代码

            if (jobDetail.CreateType == JobCreateType.Script)
            {
                if (string.IsNullOrEmpty(jobDetail.ScriptCode))
                    throw Oops.Oh(ExceptionCode.Field_Required, Lo.GetValue<EasyJobLocalResource>(nameof(jobDetail.ScriptCode)));

                if (jobDetail.ScriptCode != oldScriptCode)
                {
                    // 动态创建作业
                    var jobType = _dynamicJobCompiler.BuildJob(jobDetail.ScriptCode);
                    if (jobType == null)
                    {
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_Compile_Fail);
                    }
                    if (jobType.GetCustomAttributes(typeof(JobDetailAttribute), false).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobDetail_Not_Find);
                    if (jobDetailAttribute.JobId != jobDetail.JobId)
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobId_Inconsistency);

                    scheduler?.UpdateDetail(JobBuilder.Create(jobType).LoadFrom(jobDetail.Adapt<JobDetail>()).SetJobType(jobType));
                }
            }
            else
            {
                scheduler?.UpdateDetail(scheduler.GetJobBuilder().LoadFrom(jobDetail.Adapt<JobDetail>()));
            }
        }
    }
}
