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
using Gardener.EasyJob.Services;
using Gardener.EntityFramwork;
using Gardener.Enums;
using Gardener.LocalizationLocalizer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Gardener.EasyJob.Impl.Services
{

    /// <summary>
    /// 定时任务-任务服务
    /// </summary>
    [ApiDescriptionSettings("EasyJobServices")]
    public class SysJobDetailService : ServiceBase<SysJobDetail, SysJobDetailDto,int>, IDynamicApiController, ITransient, ISysJobDetailService
    {
        private readonly IRepository<SysJobDetail> _sysJobDetailRep;
        private readonly IRepository<SysJobTrigger> _sysJobTriggerRep;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly DynamicJobCompiler _dynamicJobCompiler;
        /// <summary>
        /// 定时任务-任务服务
        /// </summary>
        /// <param name="sysJobDetailRep"></param>
        /// <param name="sysJobTriggerRep"></param>
        /// <param name="schedulerFactory"></param>
        /// <param name="dynamicJobCompiler"></param>
        public SysJobDetailService(IRepository<SysJobDetail> sysJobDetailRep,
            IRepository<SysJobTrigger> sysJobTriggerRep,
            ISchedulerFactory schedulerFactory,
            DynamicJobCompiler dynamicJobCompiler) : base(sysJobDetailRep)
        {
            _sysJobDetailRep = sysJobDetailRep;
            _sysJobTriggerRep = sysJobTriggerRep;
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
                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetValue<EasyJobLocalResource>(EasyJobLocalResource.JobId));

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
                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetValue<EasyJobLocalResource>(nameof(SysJobDetailDto.JobId)));

            var sysJobDetail = await _sysJobDetailRep.Where(u => u.Id == input.Id).FirstAsync();
            if (sysJobDetail.JobId != input.JobId)
                throw Oops.Oh(ExceptionCode.Field_Cannot_Be_Modified, Lo.GetValue<EasyJobLocalResource>(nameof(SysJobDetailDto.JobId)));

            var scheduler = _schedulerFactory.GetJob(sysJobDetail.JobId);
            if (scheduler == null)
            {
                throw Oops.Oh(ExceptionCode.Scheduler_Not_Find);
            }
            var oldScriptCode = sysJobDetail.ScriptCode;//旧脚本代码
            input.Adapt(sysJobDetail);

            if (input.CreateType == JobCreateType.Script)
            {
                if (string.IsNullOrEmpty(input.ScriptCode))
                    throw Oops.Oh(ExceptionCode.Field_Required, Lo.GetValue<EasyJobLocalResource>(nameof(input.ScriptCode)));

                if (input.ScriptCode != oldScriptCode)
                {
                    // 动态创建作业
                    var jobType = _dynamicJobCompiler.BuildJob(input.ScriptCode);
                    if (jobType == null)
                    {
                        throw Oops.Oh(EasyJobExceptionCode.Script_Code_Compile_Fail);
                    }
                    if (jobType.GetCustomAttributes(typeof(JobDetailAttribute), false).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
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
        public override async Task<bool> Delete(int id)
        {
            SysJobDetail? sysJobDetail=await _sysJobDetailRep.FindOrDefaultAsync(id);
            if (sysJobDetail == null) 
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            _schedulerFactory.RemoveJob(sysJobDetail.JobId);

            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下面的代码确保作业和触发器能被删除
            await _sysJobDetailRep.Where(u => u.JobId == sysJobDetail.JobId).ForEachAsync(x =>
            {
                x.DeleteNow();
            });
            await _sysJobTriggerRep.Where(u => u.JobId == sysJobDetail.JobId).ForEachAsync(x =>
            {
                x.DeleteNow();
            });
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public override Task<bool> Deletes([FromBody] int[] ids)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public override Task<bool> FakeDelete(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public override Task<bool> FakeDeletes([FromBody] int[] ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取触发器列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SysJobTriggerDto>> GetTriggers([ApiSeat(ApiSeats.ActionStart)] int id)
        {
            SysJobDetail? sysJobDetail = await _sysJobDetailRep.FindOrDefaultAsync(id);
            if (sysJobDetail == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            return await _sysJobTriggerRep.AsQueryable(false)
                .Where(!string.IsNullOrWhiteSpace(sysJobDetail.JobId), u => u.JobId.Contains(sysJobDetail.JobId))
                .Select(x => x.Adapt<SysJobTriggerDto>())
                .ToListAsync();
        }

        /// <summary>
        /// 暂停作业
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public async Task<bool> Pause([ApiSeat(ApiSeats.ActionStart)] int id)
        {
            SysJobDetail? sysJobDetail = await _sysJobDetailRep.FindOrDefaultAsync(id);
            if (sysJobDetail == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(sysJobDetail.JobId);
            if (scheduler == null)
            {
                throw Oops.Oh(ExceptionCode.Scheduler_Not_Find);
            }
            scheduler.Pause();
            return true;
        }

        /// <summary>
        /// 启动作业
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public async Task<bool> Start([ApiSeat(ApiSeats.ActionStart)] int id)
        {
            SysJobDetail? sysJobDetail = await _sysJobDetailRep.FindOrDefaultAsync(id);
            if (sysJobDetail == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(sysJobDetail.JobId);
            if (scheduler == null)
            {
                throw Oops.Oh(ExceptionCode.Scheduler_Not_Find);
            }
            scheduler.Start();
            return true;
        }

        /// <summary>
        /// 暂停所有作业
        /// </summary>
        /// <returns></returns>
        public Task<bool> PauseAll()
        {
            _schedulerFactory.PauseAll();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 启动所有作业
        /// </summary>
        /// <returns></returns>
        public Task<bool> StartAll()
        {
            _schedulerFactory.StartAll();
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

    }
}
