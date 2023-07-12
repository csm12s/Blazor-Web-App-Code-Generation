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
using Gardener.EasyJob.Impl.Domains;
using Gardener.EasyJob.Resources;
using Gardener.EntityFramwork;
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gardener.EasyJob.Impl.Services
{
    /// <summary>
    /// 定时任务-触发器服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class SysJobTriggerService : ServiceBase<SysJobTrigger, SysJobTriggerDto,int>, IDynamicApiController, ITransient
    {
        private readonly IRepository<SysJobTrigger> _sysJobTriggerRep;
        private readonly ISchedulerFactory _schedulerFactory;
        /// <summary>
        /// 系统作业任务触发器服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="schedulerFactory"></param>
        public SysJobTriggerService(IRepository<SysJobTrigger> repository, ISchedulerFactory schedulerFactory) : base(repository)
        {
            _sysJobTriggerRep = repository;
            _schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// 添加触发器
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<SysJobTriggerDto> Insert(SysJobTriggerDto input)
        {
            var isExist = await _sysJobTriggerRep.AnyAsync(u => u.TriggerId == input.TriggerId && u.Id != input.Id);
            if (isExist)
                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetString<EasyJobLocalResource>(nameof(SysJobTriggerDto.TriggerId)));

            var jobTrigger = input.Adapt<SysJobTrigger>();
            jobTrigger.Args = "[" + jobTrigger.Args + "]";

            var scheduler = _schedulerFactory.GetJob(input.JobId);
            scheduler?.AddTrigger(Triggers.Create(input.AssemblyName, input.TriggerType).LoadFrom(jobTrigger));
            return input;
        }

        /// <summary>
        /// 更新触发器
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(SysJobTriggerDto input)
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
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            SysJobTrigger jobTrigger = _sysJobTriggerRep.Find(id);
            if (jobTrigger == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(jobTrigger.JobId);
            scheduler?.RemoveTrigger(jobTrigger.TriggerId);

            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下行代码确保触发器能被删除
            List<SysJobTrigger> jobTriggers = await _sysJobTriggerRep.Where(u => u.JobId == jobTrigger.JobId && u.TriggerId == jobTrigger.TriggerId).ToListAsync();
            jobTriggers.ForEach(x =>
            {
                _sysJobTriggerRep.Delete(x);
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
        /// 暂停触发器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> Pause([ApiSeat(ApiSeats.ActionStart)]int id)
        {
            SysJobTrigger jobTrigger = _sysJobTriggerRep.Find(id);
            if (jobTrigger == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(jobTrigger.JobId);
            scheduler?.PauseTrigger(jobTrigger.TriggerId);
            return Task.FromResult(true);
        }

        /// <summary>
        /// 启动触发器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> Start([ApiSeat(ApiSeats.ActionStart)] int id)
        {
            SysJobTrigger jobTrigger = _sysJobTriggerRep.Find(id);
            if (jobTrigger == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            var scheduler = _schedulerFactory.GetJob(jobTrigger.JobId);
            scheduler?.StartTrigger(jobTrigger.TriggerId);
            return Task.FromResult(true);
        }
    }
}
