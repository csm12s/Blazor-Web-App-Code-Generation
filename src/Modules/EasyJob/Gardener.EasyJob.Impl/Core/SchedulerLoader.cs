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
        public JobBuilder CreateJobBuilder(SysJobDetail jobDetail)
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
                        jobDetail.JobType = jobType.FullName;
                        jobDetail.AssemblyName = jobType.Assembly.GetName().Name;
                        break;
                    }
                case JobCreateType.Http:
                    jobType = typeof(HttpJob);
                    jobDetail.JobType = jobType.FullName;
                    jobDetail.AssemblyName = jobType.Assembly.GetName().Name;
                    break;

                default:
                    throw new NotSupportedException();
            }
            return JobBuilder.From(jobDetail.Adapt<JobDetail>()).SetJobType(jobType);
        }

        /// <summary>
        /// 更新job到调度
        /// </summary>
        /// <param name="newJooDetail"></param>
        /// <param name="oldJobDetail"></param>
        public JobBuilder CreateOrUpdateJobBuilder(SysJobDetail newJooDetail, SysJobDetail oldJobDetail)
        {
            //var scheduler = _schedulerFactory.GetJob(newJooDetail.JobId);
            //if (scheduler == null)
            //{
            //    throw Oops.Oh(ExceptionCode.Scheduler_Not_Find);
            //}
            var oldScriptCode = oldJobDetail.ScriptCode;//旧脚本代码

            if (newJooDetail.CreateType == JobCreateType.Script)
            {
                if (string.IsNullOrEmpty(newJooDetail.ScriptCode))
                    throw Oops.Oh(ExceptionCode.Field_Required, Lo.GetValue<EasyJobLocalResource>(nameof(newJooDetail.ScriptCode)));

                if (newJooDetail.ScriptCode != oldScriptCode)
                {
                    // 动态创建作业
                    var jobType = _dynamicJobCompiler.BuildJob(newJooDetail.ScriptCode);
                    if (jobType == null)
                    {
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_Compile_Fail);
                    }
                    if (jobType.GetCustomAttributes(typeof(JobDetailAttribute), false).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobDetail_Not_Find);
                    if (jobDetailAttribute.JobId != newJooDetail.JobId)
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobId_Inconsistency);

                    //内部生成的信息
                    newJooDetail.JobType = jobType.FullName;
                    newJooDetail.AssemblyName = jobType.Assembly.GetName().Name;
                    return JobBuilder.Create(jobType).LoadFrom(newJooDetail.Adapt<JobDetail>());
                }
            }
            return JobBuilder.From(newJooDetail.Adapt<JobDetail>());
        }
    }
}
