// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.Schedule;
using Gardener.Base;
using Gardener.EasyJob.Dtos;
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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.RegularExpressions;

namespace Gardener.EasyJob.Impl.Services
{

    /// <summary>
    /// 定时任务-任务服务
    /// </summary>
    [ApiDescriptionSettings("EasyJobServices")]
    public class SysJobDetailService : ServiceBase<SysJobDetail, SysJobDetailDto, int>, IDynamicApiController, IScoped, ISysJobDetailService
    {
        private readonly IRepository<SysJobDetail> _sysJobDetailRep;
        private readonly IRepository<SysJobTrigger> _sysJobTriggerRep;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly SchedulerLoader schedulerLoader;
        /// <summary>
        /// 定时任务-任务服务
        /// </summary>
        /// <param name="sysJobDetailRep"></param>
        /// <param name="sysJobTriggerRep"></param>
        /// <param name="schedulerFactory"></param>
        /// <param name="schedulerLoader"></param>
        public SysJobDetailService(IRepository<SysJobDetail> sysJobDetailRep,
            IRepository<SysJobTrigger> sysJobTriggerRep,
            ISchedulerFactory schedulerFactory,
            SchedulerLoader schedulerLoader) : base(sysJobDetailRep)
        {
            _sysJobDetailRep = sysJobDetailRep;
            _sysJobTriggerRep = sysJobTriggerRep;
            _schedulerFactory = schedulerFactory;
            this.schedulerLoader = schedulerLoader;
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

            SysJobDetail jobDetail = input.Adapt<SysJobDetail>();

            //构建 builder 同时会赋值 执行类相关数据
            JobBuilder jobBuilder = schedulerLoader.CreateJobBuilder(jobDetail);
            //入库
            //扫描后下次不能扫了
            jobDetail.IncludeAnnotations = false;
            EntityEntry<SysJobDetail> entityEntry = await _sysJobDetailRep.InsertNowAsync(jobDetail);
            //入调度
            _schedulerFactory.AddJob(jobBuilder);
            //if (jobDetail.CreateType.Equals(JobCreateType.Script) && jobDetail.IncludeAnnotations)
            //{
            //    IScheduler scheduler = _schedulerFactory.GetJob(jobDetail.JobId);
            //    //运行中的detail
            //    JobDetail detail = scheduler.GetJobDetail();
            //    entityEntry.Entity.

            //    _sysJobDetailRep.UpdateIncludeNow(entityEntry.Entity);

            //}
            return entityEntry.Entity.Adapt<SysJobDetailDto>();
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

            var sysJobDetail = await _sysJobDetailRep.SingleOrDefaultAsync(x => x.Id == input.Id, false);
            if (sysJobDetail.JobId != input.JobId)
                throw Oops.Oh(ExceptionCode.Field_Cannot_Be_Modified, Lo.GetValue<EasyJobLocalResource>(nameof(SysJobDetailDto.JobId)));

            var scheduler = _schedulerFactory.GetJob(sysJobDetail.JobId);
            if (scheduler == null)
            {
                throw Oops.Oh(ExceptionCode.Scheduler_Not_Find);
            }
            SysJobDetail newJooDetail = input.Adapt<SysJobDetail>();
            //更新 任务
            var jobBuilder = schedulerLoader.CreateOrUpdateJobBuilder(newJooDetail, sysJobDetail);
            scheduler.UpdateDetail(jobBuilder);
            //更新 db
            //扫描后下次不能扫了
            newJooDetail.IncludeAnnotations = false;

            await _sysJobDetailRep.UpdateNowAsync(newJooDetail);
            return true;
        }

        /// <summary>
        /// 删除作业
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            SysJobDetail? sysJobDetail = await _sysJobDetailRep.SingleOrDefaultAsync(x => x.Id == id, false);
            if (sysJobDetail == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            _schedulerFactory.RemoveJob(sysJobDetail.JobId);

            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下面的代码确保作业和触发器能被删除
            var jobs = await _sysJobDetailRep.AsQueryable(false).Where(u => u.JobId == sysJobDetail.JobId).ToListAsync();
            foreach (var job in jobs)
            {
                _sysJobDetailRep.DeleteNow(job);
            }
            var triggers = await _sysJobTriggerRep.AsQueryable(false).Where(u => u.JobId == sysJobDetail.JobId).ToListAsync();
            foreach (var trigger in triggers)
            {
                _sysJobTriggerRep.DeleteNow(trigger);
            }
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
            SysJobDetail? sysJobDetail = await _sysJobDetailRep.SingleOrDefaultAsync(x => x.Id == id, false);
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
            SysJobDetail? sysJobDetail = await _sysJobDetailRep.SingleOrDefaultAsync(x => x.Id == id, false);
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
            SysJobDetail? sysJobDetail = await _sysJobDetailRep.SingleOrDefaultAsync(x => x.Id == id, false);
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
