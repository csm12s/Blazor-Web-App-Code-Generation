//// -----------------------------------------------------------------------------
//// 园丁,是个很简单的管理系统
////  gitee:https://gitee.com/hgflydream/Gardener 
////  issues:https://gitee.com/hgflydream/Gardener/issues 
//// -----------------------------------------------------------------------------

//using Furion;
//using Furion.DatabaseAccessor;
//using Furion.DependencyInjection;
//using Furion.DynamicApiController;
//using Furion.FriendlyException;
//using Furion.Localization;
//using Furion.Schedule;
//using Gardener.Authentication.Dtos;
//using Gardener.Base;
//using Gardener.EasyJob.Dtos;
//using Gardener.EasyJob.Enums;
//using Gardener.EasyJob.Impl.Core;
//using Gardener.EasyJob.Impl.Domains;
//using Gardener.EasyJob.Resources;
//using Gardener.EntityFramwork;
//using Gardener.Enums;
//using Mapster;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Gardener.EasyJob.Impl.Services
//{

//    /// <summary>
//    /// 系统作业任务服务
//    /// </summary>
//    [ApiDescriptionSettings("SystemBaseServices")]
//    public class SysJobService : ServiceBaseNoKey<SysJobDetail, SysJobDetailDto>, IDynamicApiController, ITransient
//    {
//        private readonly IRepository<SysJobDetail> _sysJobDetailRep;
//        private readonly IRepository<SysJobTrigger> _sysJobTriggerRep;
//        private readonly IRepository<SysJobCluster> _sysJobClusterRep;
//        private readonly ISchedulerFactory _schedulerFactory;
//        private readonly DynamicJobCompiler _dynamicJobCompiler;

//        public SysJobService(IRepository<SysJobDetail> sysJobDetailRep,
//            IRepository<SysJobTrigger> sysJobTriggerRep,
//            IRepository<SysJobCluster> sysJobClusterRep,
//            ISchedulerFactory schedulerFactory,
//            DynamicJobCompiler dynamicJobCompiler) : base(sysJobDetailRep)
//        {
//            _sysJobDetailRep = sysJobDetailRep;
//            _sysJobTriggerRep = sysJobTriggerRep;
//            _sysJobClusterRep = sysJobClusterRep;
//            _schedulerFactory = schedulerFactory;
//            _dynamicJobCompiler = dynamicJobCompiler;
//        }

//        /// <summary>
//        /// 获取作业分页列表
//        /// </summary>
//        /// <remarks>
//        /// 获取作业分页列表
//        /// </remarks>
//        public override async Task<Base.PagedList<SysJobDetailDto>> Search(PageRequest request)
//        {
//            IQueryable<SysJobDetail> queryable = GetSearchQueryable(request);
//            Base.PagedList<SysJobDetailDto> page = await queryable
//                .OrderConditions(request.OrderConditions.ToArray())
//                .Select(x => x.Adapt<SysJobDetailDto>())
//                .ToPageAsync(request.PageIndex, request.PageSize);

//            if (page.Items.Any())
//            {
//                // 提取中括号里面的参数值
//                var rgx = new Regex(@"(?i)(?<=\[)(.*)(?=\])");
//                var jobIds = page.Items.Select(x => x.JobId);
//                //获取所有job的触发器
//                List<SysJobTrigger> triggers = _sysJobTriggerRep.AsQueryable(false).Where(x => jobIds.Contains(x.JobId)).ToList();
//                foreach (SysJobDetailDto item in page.Items)
//                {
//                    item.JobTriggers = triggers.Where(x => x.JobId.Equals(item.JobId)).Select(x =>
//                    {
//                        var dto = x.Adapt<SysJobTriggerDto>();
//                        dto.Args = rgx.Match(dto.Args ?? "").Value;
//                        return dto;
//                    });
//                }
//            }
//            return page;
//        }

//        /// <summary>
//        /// 添加作业
//        /// </summary>
//        /// <returns></returns>
//        /// <remarks>
//        /// 
//        /// </remarks>
//        public override async Task<SysJobDetailDto> Insert(SysJobDetailDto input)
//        {
//            var isExist = await _sysJobDetailRep.AnyAsync(u => u.JobId == input.JobId && u.Id != input.Id);
//            if (isExist)
//                throw Oops.Oh(ExceptionCode.Data_Key_Uniqueness_Conflict, Lo.GetString<EasyJobLocalResource>(EasyJobLocalResource.JobId));

//            // 动态创建作业
//            Type? jobType;
//            switch (input.CreateType)
//            {
//                case JobCreateType.Script when string.IsNullOrEmpty(input.ScriptCode):
//                    throw Oops.Oh(ExceptionCode.Field_Required,nameof(input.ScriptCode));
//                case JobCreateType.Script:
//                    {
//                        jobType = _dynamicJobCompiler.BuildJob(input.ScriptCode);
//                        if (jobType == null)
//                        {
//                            throw Oops.Oh(ExceptionCode.Script_Code_Compile_Fail);
//                        }
//                        if (jobType.GetCustomAttributes(typeof(JobDetailAttribute),false).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
//                            throw Oops.Oh(ErrorCodeEnum.D1702);
//                        if (jobDetailAttribute.JobId != input.JobId)
//                            throw Oops.Oh(ErrorCodeEnum.D1703);
//                        break;
//                    }
//                case JobCreateType.Http:
//                    jobType = typeof(HttpJob);
//                    break;

//                default:
//                    throw new NotSupportedException();
//            }

//            _schedulerFactory.AddJob(
//                JobBuilder.Create(jobType)
//                    .LoadFrom(input.Adapt<SysJobDetail>()).SetJobType(jobType));

//            // 延迟一下等待持久化写入，再执行其他字段的更新
//            await Task.Delay(500);
//            await _sysJobDetailRep.AsUpdateable()
//                .SetColumns(u => new SysJobDetail { CreateType = input.CreateType, ScriptCode = input.ScriptCode })
//                .Where(u => u.JobId == input.JobId).ExecuteCommandAsync();
//        }

//        /// <summary>
//        /// 更新作业
//        /// </summary>
//        /// <returns></returns>
//        [ApiDescriptionSettings(Name = "UpdateJobDetail"), HttpPost]
//        [DisplayName("更新作业")]
//        public async Task UpdateJobDetail(UpdateJobDetailInput input)
//        {
//            var isExist = await _sysJobDetailRep.IsAnyAsync(u => u.JobId == input.JobId && u.Id != input.Id);
//            if (isExist)
//                throw Oops.Oh(ErrorCodeEnum.D1006);

//            var sysJobDetail = await _sysJobDetailRep.GetFirstAsync(u => u.Id == input.Id);
//            if (sysJobDetail.JobId != input.JobId)
//                throw Oops.Oh(ErrorCodeEnum.D1704);

//            var scheduler = _schedulerFactory.GetJob(sysJobDetail.JobId);
//            var oldScriptCode = sysJobDetail.ScriptCode;//旧脚本代码
//            input.Adapt(sysJobDetail);

//            if (input.CreateType == JobCreateTypeEnum.Script)
//            {
//                if (string.IsNullOrEmpty(input.ScriptCode))
//                    throw Oops.Oh(ErrorCodeEnum.D1701);

//                if (input.ScriptCode != oldScriptCode)
//                {
//                    // 动态创建作业
//                    var jobType = _dynamicJobCompiler.BuildJob(input.ScriptCode);

//                    if (jobType.GetCustomAttributes(typeof(JobDetailAttribute)).FirstOrDefault() is not JobDetailAttribute jobDetailAttribute)
//                        throw Oops.Oh(ErrorCodeEnum.D1702);
//                    if (jobDetailAttribute.JobId != input.JobId)
//                        throw Oops.Oh(ErrorCodeEnum.D1703);

//                    scheduler?.UpdateDetail(JobBuilder.Create(jobType).LoadFrom(sysJobDetail).SetJobType(jobType));
//                }
//            }
//            else
//            {
//                scheduler?.UpdateDetail(scheduler.GetJobBuilder().LoadFrom(sysJobDetail));
//            }

//            // Tip: 假如这次更新有变更了 JobId，变更 JobId 后触发的持久化更新执行，会由于找不到 JobId 而更新不到数据
//            // 延迟一下等待持久化写入，再执行其他字段的更新
//            await Task.Delay(500);
//            await _sysJobDetailRep.UpdateAsync(sysJobDetail);
//        }

//        /// <summary>
//        /// 删除作业
//        /// </summary>
//        /// <returns></returns>
//        [ApiDescriptionSettings(Name = "DeleteJobDetail"), HttpPost]
//        [DisplayName("删除作业")]
//        public async Task DeleteJobDetail(DeleteJobDetailInput input)
//        {
//            _schedulerFactory.RemoveJob(input.JobId);

//            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下面的代码确保作业和触发器能被删除
//            await _sysJobDetailRep.DeleteAsync(u => u.JobId == input.JobId);
//            await _sysJobTriggerRep.DeleteAsync(u => u.JobId == input.JobId);
//        }

//        /// <summary>
//        /// 获取触发器列表
//        /// </summary>
//        [DisplayName("获取触发器列表")]
//        public async Task<List<SysJobTrigger>> GetJobTriggerList([FromQuery] JobDetailInput input)
//        {
//            return await _sysJobTriggerRep.AsQueryable()
//                .WhereIF(!string.IsNullOrWhiteSpace(input.JobId), u => u.JobId.Contains(input.JobId))
//                .ToListAsync();
//        }

//        /// <summary>
//        /// 添加触发器
//        /// </summary>
//        /// <returns></returns>
//        [ApiDescriptionSettings(Name = "AddJobTrigger"), HttpPost]
//        [DisplayName("添加触发器")]
//        public async Task AddJobTrigger(AddJobTriggerInput input)
//        {
//            var isExist = await _sysJobTriggerRep.IsAnyAsync(u => u.TriggerId == input.TriggerId && u.Id != input.Id);
//            if (isExist)
//                throw Oops.Oh(ErrorCodeEnum.D1006);

//            var jobTrigger = input.Adapt<SysJobTrigger>();
//            jobTrigger.Args = "[" + jobTrigger.Args + "]";

//            var scheduler = _schedulerFactory.GetJob(input.JobId);
//            scheduler?.AddTrigger(Triggers.Create(input.AssemblyName, input.TriggerType).LoadFrom(jobTrigger));
//        }

//        /// <summary>
//        /// 更新触发器
//        /// </summary>
//        /// <returns></returns>
//        [ApiDescriptionSettings(Name = "UpdateJobTrigger"), HttpPost]
//        [DisplayName("更新触发器")]
//        public async Task UpdateJobTrigger(UpdateJobTriggerInput input)
//        {
//            var isExist = await _sysJobTriggerRep.IsAnyAsync(u => u.TriggerId == input.TriggerId && u.Id != input.Id);
//            if (isExist)
//                throw Oops.Oh(ErrorCodeEnum.D1006);

//            var jobTrigger = input.Adapt<SysJobTrigger>();
//            jobTrigger.Args = "[" + jobTrigger.Args + "]";

//            var scheduler = _schedulerFactory.GetJob(input.JobId);
//            scheduler?.UpdateTrigger(Triggers.Create(input.AssemblyName, input.TriggerType).LoadFrom(jobTrigger));
//        }

//        /// <summary>
//        /// 删除触发器
//        /// </summary>
//        /// <returns></returns>
//        [ApiDescriptionSettings(Name = "DeleteJobTrigger"), HttpPost]
//        [DisplayName("删除触发器")]
//        public async Task DeleteJobTrigger(DeleteJobTriggerInput input)
//        {
//            var scheduler = _schedulerFactory.GetJob(input.JobId);
//            scheduler?.RemoveTrigger(input.TriggerId);

//            // 如果 _schedulerFactory 中不存在 JodId，则无法触发持久化，下行代码确保触发器能被删除
//            await _sysJobTriggerRep.DeleteAsync(u => u.JobId == input.JobId && u.TriggerId == input.TriggerId);
//        }

//        /// <summary>
//        /// 暂停所有作业
//        /// </summary>
//        /// <returns></returns>
//        [DisplayName("暂停所有作业")]
//        public void PauseAllJob()
//        {
//            _schedulerFactory.PauseAll();
//        }

//        /// <summary>
//        /// 启动所有作业
//        /// </summary>
//        /// <returns></returns>
//        [DisplayName("启动所有作业")]
//        public void StartAllJob()
//        {
//            _schedulerFactory.StartAll();
//        }

//        /// <summary>
//        /// 暂停作业
//        /// </summary>
//        [DisplayName("暂停作业")]
//        public void PauseJob(JobDetailInput input)
//        {
//            var scheduler = _schedulerFactory.GetJob(input.JobId);
//            scheduler?.Pause();
//        }

//        /// <summary>
//        /// 启动作业
//        /// </summary>
//        [DisplayName("启动作业")]
//        public void StartJob(JobDetailInput input)
//        {
//            var scheduler = _schedulerFactory.GetJob(input.JobId);
//            scheduler?.Start();
//        }

//        /// <summary>
//        /// 暂停触发器
//        /// </summary>
//        [DisplayName("暂停触发器")]
//        public void PauseTrigger(JobTriggerInput input)
//        {
//            var scheduler = _schedulerFactory.GetJob(input.JobId);
//            scheduler?.PauseTrigger(input.TriggerId);
//        }

//        /// <summary>
//        /// 启动触发器
//        /// </summary>
//        [DisplayName("启动触发器")]
//        public void StartTrigger(JobTriggerInput input)
//        {
//            var scheduler = _schedulerFactory.GetJob(input.JobId);
//            scheduler?.StartTrigger(input.TriggerId);
//        }

//        /// <summary>
//        /// 强制唤醒作业调度器
//        /// </summary>
//        [DisplayName("强制唤醒作业调度器")]
//        public void CancelSleep()
//        {
//            _schedulerFactory.CancelSleep();
//        }

//        /// <summary>
//        /// 强制触发所有作业持久化
//        /// </summary>
//        [DisplayName("强制触发所有作业持久化")]
//        public void PersistAll()
//        {
//            _schedulerFactory.PersistAll();
//        }

//        /// <summary>
//        /// 获取集群列表
//        /// </summary>
//        [DisplayName("获取集群列表")]
//        public async Task<List<SysJobCluster>> GetJobClusterList()
//        {
//            return await _sysJobClusterRep.GetListAsync();
//        }
//    }
//}
