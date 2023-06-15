// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.Schedule;
using Gardener.Base;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Enums;
using Gardener.EasyJob.Impl.Core;
using Gardener.EasyJob.Impl.Domains;
using Gardener.EasyJob.Resources;
using Gardener.EntityFramwork;
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Gardener.EasyJob.Impl.Services
{

    /// <summary>
    /// 系统作业任务服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class SysJobService : ServiceBaseNoKey<SysJobDetail, SysJobDetailDto>, IDynamicApiController, ITransient
    {
        private readonly IRepository<SysJobDetail> _sysJobDetailRep;
        private readonly IRepository<SysJobTrigger> _sysJobTriggerRep;
        private readonly IRepository<SysJobCluster> _sysJobClusterRep;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly DynamicJobCompiler _dynamicJobCompiler;
        public SysJobService(IRepository<SysJobDetail> sysJobDetailRep,
            IRepository<SysJobTrigger> sysJobTriggerRep,
            IRepository<SysJobCluster> sysJobClusterRep,
            ISchedulerFactory schedulerFactory,
            DynamicJobCompiler dynamicJobCompiler) : base(sysJobDetailRep)
        {
            _sysJobDetailRep = sysJobDetailRep;
            _sysJobTriggerRep = sysJobTriggerRep;
            _sysJobClusterRep = sysJobClusterRep;
            _schedulerFactory = schedulerFactory;
            _dynamicJobCompiler = dynamicJobCompiler;
        }

        /// <summary>
        /// 获取作业分页列表
        /// </summary>
        /// <remarks>
        /// 获取作业分页列表
        /// </remarks>
        public override async Task<Base.PagedList<SysJobDetailDto>> Search(PageRequest request)
        {
            IQueryable<SysJobDetail> queryable = GetSearchQueryable(request);
            Base.PagedList<SysJobDetailDto> page = await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<SysJobDetailDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);

            if (page.Items.Any())
            {
                // 提取中括号里面的参数值
                var rgx = new Regex(@"(?i)(?<=\[)(.*)(?=\])");
                var jobIds = page.Items.Select(x => x.JobId);
                //获取所有job的触发器
                List<SysJobTrigger> triggers = _sysJobTriggerRep.AsQueryable(false).Where(x => jobIds.Contains(x.JobId)).ToList();
                foreach (SysJobDetailDto item in page.Items)
                {
                    item.JobTriggers = triggers.Where(x => x.JobId.Equals(item.JobId)).Select(x =>
                    {
                        var dto = x.Adapt<SysJobTriggerDto>();
                        dto.Args = rgx.Match(dto.Args ?? "").Value;
                        return dto;
                    });
                }
            }
            return page;
        }

        /// <summary>
        /// 添加作业
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        public override async Task<SysJobDetailDto> Insert(SysJobDetailDto input)
        {
            var isExist = await _sysJobDetailRep.AnyAsync(u => u.JobId == input.JobId && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetString<EasyJobLocalResource>(EasyJobLocalResource.JobId));

            // 动态创建作业
            Type? jobType;
            switch (input.CreateType)
            {
                case JobCreateType.Script when string.IsNullOrEmpty(input.ScriptCode):
                    throw Oops.Oh(ExceptionCode.Field_Required, nameof(input.ScriptCode));
                case JobCreateType.Script:
                    {
                        jobType = _dynamicJobCompiler.BuildJob(input.ScriptCode);
                        if (jobType == null)
                        {
                            throw Oops.Oh(EasyJobExceptionCode.Script_Code_Compile_Fail);
                        }
                        if (jobType.GetCustomAttributes(typeof(JobDetailAttribute), false).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
                            throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobDetail_Not_Find);
                        if (jobDetailAttribute.JobId != input.JobId)
                            throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobId_Inconsistency);
                        break;
                    }
                case JobCreateType.Http:
                    jobType = typeof(HttpJob);
                    break;

                default:
                    throw new NotSupportedException();
            }

            _schedulerFactory.AddJob(
                JobBuilder.Create(jobType)
                    .LoadFrom(input.Adapt<SysJobDetail>()).SetJobType(jobType));

            // 延迟一下等待持久化写入，再执行其他字段的更新
            await Task.Delay(500);
            SysJobDetail? jobDetail = await _sysJobDetailRep.Where(u => u.JobId == input.JobId).FirstOrDefaultAsync();
            if (jobDetail == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            jobDetail.CreateType = input.CreateType;
            jobDetail.ScriptCode = input.ScriptCode;
            await _sysJobDetailRep.UpdateAsync(jobDetail);

            return jobDetail.Adapt<SysJobDetailDto>();
        }

        /// <summary>
        /// 更新作业
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Update(SysJobDetailDto input)
        {
            var isExist = await _sysJobDetailRep.AnyAsync(u => u.JobId == input.JobId && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetString<EasyJobLocalResource>(nameof(SysJobDetailDto.JobId)));

            var sysJobDetail = await _sysJobDetailRep.Where(u => u.Id == input.Id).FirstAsync();
            if (sysJobDetail.JobId != input.JobId)
                throw Oops.Oh(ExceptionCode.Field_Cannot_Be_Modified, Lo.GetString<EasyJobLocalResource>(nameof(SysJobDetailDto.JobId)));

            var scheduler = _schedulerFactory.GetJob(sysJobDetail.JobId);
            var oldScriptCode = sysJobDetail.ScriptCode;//旧脚本代码
            input.Adapt(sysJobDetail);

            if (input.CreateType == JobCreateType.Script)
            {
                if (string.IsNullOrEmpty(input.ScriptCode))
                    throw Oops.Oh(ExceptionCode.Field_Required, Lo.GetString<EasyJobLocalResource>(nameof(input.ScriptCode)));

                if (input.ScriptCode != oldScriptCode)
                {
                    // 动态创建作业
                    var jobType = _dynamicJobCompiler.BuildJob(input.ScriptCode);
                    if (jobType == null)
                    {
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_Compile_Fail);
                    }
                    if (jobType.GetCustomAttributes(typeof(JobDetailAttribute),false).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobDetail_Not_Find);
                    if (jobDetailAttribute.JobId != input.JobId)
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_JobId_Inconsistency);

                    scheduler?.UpdateDetail(JobBuilder.Create(jobType).LoadFrom(sysJobDetail).SetJobType(jobType));
                }
            }
            else
            {
                scheduler?.UpdateDetail(scheduler.GetJobBuilder().LoadFrom(sysJobDetail));
            }

            // Tip: 假如这次更新有变更了 JobId，变更 JobId 后触发的持久化更新执行，会由于找不到 JobId 而更新不到数据
            // 延迟一下等待持久化写入，再执行其他字段的更新
            await Task.Delay(500);
            await _sysJobDetailRep.UpdateAsync(sysJobDetail);

            return true;
        }

        /// <summary>
        /// 删除作业
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteJobDetail(JobDetailInput input)
        {
            _schedulerFactory.RemoveJob(input.JobId);

            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下面的代码确保作业和触发器能被删除
            await _sysJobDetailRep.Where(u => u.JobId == input.JobId).ForEachAsync(x =>
            {
                x.DeleteNow();
            });
            await _sysJobTriggerRep.Where(u => u.JobId == input.JobId).ForEachAsync(x =>
            {
                x.DeleteNow();
            });
            return true;
        }

        /// <summary>
        /// 获取触发器列表
        /// </summary>
        public async Task<IEnumerable<SysJobTriggerDto>> GetJobTriggerList([FromQuery] JobDetailInput input)
        {
            return await _sysJobTriggerRep.AsQueryable(false)
                .Where(!string.IsNullOrWhiteSpace(input.JobId), u => u.JobId.Contains(input.JobId))
                .Select(x => x.Adapt<SysJobTriggerDto>())
                .ToListAsync();
        }

        /// <summary>
        /// 添加触发器
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddJobTrigger(SysJobTriggerDto input)
        {
            var isExist = await _sysJobTriggerRep.AnyAsync(u => u.TriggerId == input.TriggerId && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetString<EasyJobLocalResource>(nameof(SysJobTriggerDto.TriggerId)));

            var jobTrigger = input.Adapt<SysJobTrigger>();
            jobTrigger.Args = "[" + jobTrigger.Args + "]";

            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.AddTrigger(Triggers.Create(input.AssemblyName, input.TriggerType).LoadFrom(jobTrigger));
            return true;
        }

        /// <summary>
        /// 更新触发器
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateJobTrigger(SysJobTriggerDto input)
        {
            var isExist = await _sysJobTriggerRep.AnyAsync(u => u.TriggerId == input.TriggerId && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetString<EasyJobLocalResource>(nameof(SysJobTriggerDto.TriggerId)));

            var jobTrigger = input.Adapt<SysJobTrigger>();
            jobTrigger.Args = "[" + jobTrigger.Args + "]";

            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.UpdateTrigger(Triggers.Create(input.AssemblyName, input.TriggerType).LoadFrom(jobTrigger));

            return true;
        }

        /// <summary>
        /// 删除触发器
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteJobTrigger(JobTriggerInput input)
        {
            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.RemoveTrigger(input.TriggerId);

            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下行代码确保触发器能被删除
            List<SysJobTrigger> jobTriggers = await _sysJobTriggerRep.Where(u => u.JobId == input.JobId && u.TriggerId == input.TriggerId).ToListAsync();
            jobTriggers.ForEach(x =>
            {
                _sysJobTriggerRep.Delete(x);
            });

            return true;
        }

        /// <summary>
        /// 暂停所有作业
        /// </summary>
        /// <returns></returns>
        public Task<bool> PauseAllJob()
        {
            _schedulerFactory.PauseAll();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 启动所有作业
        /// </summary>
        /// <returns></returns>
        public Task<bool> StartAllJob()
        {
            _schedulerFactory.StartAll();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 暂停作业
        /// </summary>
        public Task<bool> PauseJob(JobDetailInput input)
        {
            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.Pause();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 启动作业
        /// </summary>
        public Task<bool> StartJob(JobDetailInput input)
        {
            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.Start();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 暂停触发器
        /// </summary>
        public Task<bool> PauseTrigger(JobTriggerInput input)
        {
            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.PauseTrigger(input.TriggerId);
            return Task.FromResult(true);
        }

        /// <summary>
        /// 启动触发器
        /// </summary>
        public Task<bool> StartTrigger(JobTriggerInput input)
        {
            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.StartTrigger(input.TriggerId);
            return Task.FromResult(true);
        }

        /// <summary>
        /// 强制唤醒作业调度器
        /// </summary>
        public Task<bool> CancelSleep()
        {
            _schedulerFactory.CancelSleep();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 强制触发所有作业持久化
        /// </summary>
        public Task<bool> PersistAll()
        {
            _schedulerFactory.PersistAll();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 获取集群列表
        /// </summary>
        public async Task<IEnumerable<SysJobClusterDto>> GetJobClusterList()
        {
            return await _sysJobClusterRep.Where(x => x.IsDeleted == false).Select(x => x.Adapt<SysJobClusterDto>()).ToListAsync();
        }
    }
}
