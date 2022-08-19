// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Furion.DatabaseAccessor;
using Gardener.Cache;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Furion;
using System.Reflection;
using Furion.TaskScheduler;
using Furion.FriendlyException;
using Gardener.SysTimer.Enums;
using Microsoft.EntityFrameworkCore;
using Furion.JsonSerialization;
using Furion.RemoteRequest.Extensions;
using Furion.DependencyInjection;
using Gardener.SysTimer.Dtos;
using Gardener.SysTimer.Domains;
using Gardener.Enums;
using ExceptionCode = Gardener.SysTimer.Enums.ExceptionCode;

namespace Gardener.SysTimer.Services
{
    /// <summary>
    /// 任务调度服务
    /// </summary>
    /// <remarks>
    /// 仅支持单点服务部署使用
    /// </remarks>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class SysTimerService : ServiceBase<SysTimerEntity, SysTimerDto>, ISysTimerService, IScoped
    {
        private readonly ICache _cache;

        /// <summary>
        /// 任务调度服务Init
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cache"></param>
        public SysTimerService(IRepository<SysTimerEntity> repository, ICache cache) : base(repository)
        {
            _cache = cache;
        }

        /// <summary>
        /// 分页获取任务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override async Task<Base.PagedList<SysTimerDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            var workers = SpareTime.GetWorkers().ToList();

            var timers = await base.GetPage(pageIndex, pageSize);

            var lst = new List<SysTimerDto>();
            timers.Items.ToList().ForEach(u =>
            {
                var timer = workers.FirstOrDefault(m => m.WorkerName == u.JobName);
                if (timer != null)
                {
                    u.TimerStatus = (TimerStatus)timer.Status;
                    u.RunNumber = timer.Tally;
                    u.Exception = timer.Exception.Values.LastOrDefault()?.Message;
                }
                lst.Add(u);
            });
            timers.Items = lst;
            return timers;
        }

        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public override async Task<SysTimerDto> Get(int id)
        {
            var data = await base.Get(id);

            if (data != null)
            {
                //只有当任务确认运行时才获取任务数据
                if (data.StartNow == true)
                {
                    var worker = SpareTime.GetWorker(data.JobName);
                    if (worker == null)
                        throw Oops.Oh(ExceptionCode.TASK_NOT_EXIST);
                    data.TimerStatus = (TimerStatus)worker.Status;
                    data.RunNumber = worker.Tally;
                    data.Exception = worker.Exception.Values.LastOrDefault()?.Message;
                }
            }
            return data;
        }

        ///<summary>
        /// 搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<Base.PagedList<SysTimerDto>> Search(Base.PageRequest request)
        {
            var workers = SpareTime.GetWorkers().ToList();
            var timers = await base.Search(request);

            var lst = new List<SysTimerDto>();
            timers.Items.ToList().ForEach(u =>
            {
                var timer = workers.FirstOrDefault(m => m.WorkerName == u.JobName);
                if (timer != null)
                {
                    u.TimerStatus = (TimerStatus)timer.Status;
                    u.Exception = timer.Exception.Values.LastOrDefault()?.Message;
                }
                lst.Add(u);
            });
            timers.Items = lst;
            return timers;
        }

        /// <summary>
        /// 获取所有本地任务
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TaskMethodInfo>> GetLocalJobs()
        {
            // 获取本地所有任务方法
            return await GetTaskMethods();
        }

        /// <summary>
        /// 增加任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost()]
        public override async Task<SysTimerDto> Insert(SysTimerDto input)
        {
            var exits = await _repository.Where(x => x.JobName == input.JobName).AnyAsync();
            if (exits)
            {
                throw Oops.Oh(ExceptionCode.TASK_ALLREADY_EXIST);
            }
            var data = await base.Insert(input);
            if (data.StartNow)
            {
                data.Started = true;
            }
            AddTimerJob(data);
            return data;
        }

        /// <summary>
        /// 假删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> FakeDelete(int id)
        {
            var timer = await _repository.FindAsync(id);
            if (timer == null)
            {
                return false;
                throw Oops.Oh(ExceptionCode.TASK_NOT_EXIST);
            }

            var result = await base.FakeDelete(id);

            if (result)
            {
                // 从调度器里取消
                SpareTime.Cancel(timer.JobName);
            }
            return result;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            var timer = await _repository.FindAsync(id);
            if (timer == null)
            {
                return false;
                throw Oops.Oh(ExceptionCode.TASK_NOT_EXIST);
            }

            var result = await base.Delete(id);

            if (result)
            {
                // 从调度器里取消
                SpareTime.Cancel(timer.JobName);
            }
            return result;
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(SysTimerDto input)
        {
            // 排除自己并且判断与其他是否相同
            var isExist = await _repository.AnyAsync(u => u.JobName == input.JobName && u.Id != input.Id, false);
            if (isExist) throw Oops.Oh(ExceptionCode.TASK_ALLREADY_EXIST);

            // 先从调度器里取消
            var oldTimer = await _repository.FirstOrDefaultAsync(u => u.Id == input.Id, false);
            SpareTime.Cancel(oldTimer.JobName);
            //启动状态继承
            input.Started = oldTimer.Started;
            var result = await base.Update(input);
            if (result)
            {
                // 再添加到任务调度里
                AddTimerJob(input);
            }
            return result;
        }

        /// <summary>
        /// 查看任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SysTimerDto> GetDetail([FromQuery] QueryJobInput input)
        {
            var data = await _repository.DetachedEntities.FirstOrDefaultAsync(u => u.Id == input.Id);
            return data.Adapt<SysTimerDto>();
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        [HttpPost()]
        public Task Stop([FromBody] string jobName)
        {
            SpareTime.Stop(jobName);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task Start([FromBody] string jobName)
        {
            var timer = SpareTime.GetWorkers().ToList().Find(u => u.WorkerName == jobName);
            if (timer == null)
            {
                var dbTimer = await _repository.FirstOrDefaultAsync(u => u.JobName == jobName, false);
                AddTimerJob(dbTimer.Adapt<SysTimerDto>());
            }
            // Start
            SpareTime.Start(jobName);
            return;
        }

        /// <summary>
        /// 新增定时任务
        /// </summary>
        /// <param name="myinput"></param>
        [NonAction]
        public async void AddTimerJob(SysTimerDto myinput)
        {
            var input = myinput.Adapt<SysTimerEntity>();
            Action<SpareTimer, long> action = null;

            switch (input.ExecuteType)
            {
                // 创建本地方法委托
                case ExecuteType.LOCAL:
                    {
                        // 查询符合条件的任务方法
                        var taskMethod = GetTaskMethods()?.Result.FirstOrDefault(m => m.LocalMethod == input.LocalMethod);
                        if (taskMethod == null) break;
                        Type t = Type.GetType(taskMethod.TypeName);
                        // 创建任务对象
                        var typeInstance = Activator.CreateInstance(t);

                        // 创建委托
                        action = (Action<SpareTimer, long>)Delegate.CreateDelegate(typeof(Action<SpareTimer, long>), typeInstance, taskMethod.MethodName);
                        break;
                    }
                // 创建网络任务委托
                case ExecuteType.HTTP:
                    {
                        action = async (_, _) =>
                        {
                            var requestUrl = input.RequestUrl.Trim();
                            requestUrl = requestUrl?.IndexOf("http") == 0 ? requestUrl : "http://" + requestUrl;
                            var requestParameters = input.RequestParameters;
                            var headersString = input.Headers;
                            var headers = string.IsNullOrEmpty(headersString)
                                ? null
                                : JSON.Deserialize<Dictionary<string, string>>(headersString);
                            try
                            {
                                switch (input.HttpMethod)
                                {
                                    case HttpMethod.GET:
                                        await requestUrl.SetHeaders(headers).GetAsync();
                                        break;

                                    case HttpMethod.POST:
                                        await requestUrl.SetHeaders(headers).SetQueries(requestParameters).PostAsync();
                                        break;

                                    case HttpMethod.PUT:
                                        await requestUrl.SetHeaders(headers).SetQueries(requestParameters).PutAsync();
                                        break;

                                    case HttpMethod.DELETE:
                                        await requestUrl.SetHeaders(headers).DeleteAsync();
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                return;
                            }
                        };
                        break;
                    }
            }

            if (action == null) return;

            // 缓存任务配置参数，以供任务运行时读取
            if (input.ExecuteType == ExecuteType.LOCAL)
            {
                var jobParametersName = $"{input.JobName}_Parameters";
                var jobParameters = await _cache.ExistsAsync(jobParametersName);
                var requestParametersIsNull = string.IsNullOrEmpty(input.RequestParameters);

                // 如果没有任务配置却又存在缓存，则删除缓存
                if (requestParametersIsNull && jobParameters)
                    await _cache.RemoveAsync(jobParametersName);
                else if (!requestParametersIsNull)
                    await _cache.SetAsync(jobParametersName, JSON.Deserialize<Dictionary<string, string>>(input.RequestParameters));
            }
            SpareTimeExecuteTypes mode = ExecutMode.Scceeding.Equals(input.ExecutMode) ? SpareTimeExecuteTypes.Serial : SpareTimeExecuteTypes.Parallel;

            // 创建定时任务
            switch (input.TimerType)
            {
                case SpareTimeTypes.Interval:
                    if (input.DoOnce)
                        SpareTime.DoOnce((int)input.Interval * 1000, action, input.JobName, input.Remark, input.Started, executeType: mode);
                    else
                        SpareTime.Do((int)input.Interval * 1000, action, input.JobName, input.Remark, input.Started, executeType: mode);
                    break;

                case SpareTimeTypes.Cron:
                    SpareTime.Do(input.Cron, action, input.JobName, input.Remark, input.Started, executeType: mode);
                    break;
            }
        }

        /// <summary>
        /// 启动自启动任务
        /// </summary>
        [NonAction]
        public async void StartTimerJob()
        {
            var sysTimerList = await _repository.DetachedEntities.Where(t => t.Started).ProjectToType<SysTimerDto>().ToListAsync();
            sysTimerList.ForEach(AddTimerJob);
        }


        /// <summary>
        /// 获取所有本地任务
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task<IEnumerable<TaskMethodInfo>> GetTaskMethods()
        {
            // 有缓存就返回缓存
            var taskMethods = await _cache.GetAsync<IEnumerable<TaskMethodInfo>>(CacheSchems.CACHE_KEY_TIMER_JOB);
            if (taskMethods != null) return taskMethods;

            // 获取所有本地任务方法，必须有spareTimeAttribute特性
            taskMethods = App.EffectiveTypes
                .Where(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(ISpareTimeWorker).IsAssignableFrom(u))
                .SelectMany(u => u.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.IsDefined(typeof(SpareTimeAttribute), false) &&
                       m.GetParameters().Length == 2 &&
                       m.GetParameters()[0].ParameterType == typeof(SpareTimer) &&
                       m.GetParameters()[1].ParameterType == typeof(long) && m.ReturnType == typeof(void))
                .Select(m =>
                {
                    // 默认获取第一条任务特性
                    var spareTimeAttribute = m.GetCustomAttribute<SpareTimeAttribute>();
                    return new TaskMethodInfo
                    {
                        JobName = spareTimeAttribute.WorkerName,
                        Cron = spareTimeAttribute.CronExpression,
                        DoOnce = spareTimeAttribute.DoOnce,
                        ExecuteMode = (ExecutMode)spareTimeAttribute.ExecuteType,
                        Interval = (int)spareTimeAttribute.Interval / 1000,
                        StartNow = spareTimeAttribute.StartNow,
                        ExecuteType = ExecuteType.LOCAL,
                        Remark = spareTimeAttribute.Description,
                        TimerType = string.IsNullOrEmpty(spareTimeAttribute.CronExpression) ? (TimerTypes)SpareTimeTypes.Interval : (TimerTypes)SpareTimeTypes.Cron,
                        MethodName = m.Name,
                        TypeName = m.DeclaringType.FullName,
                        DeclaringType = m.DeclaringType,
                        LocalMethod = $"{m.DeclaringType.FullName}|{m.Name}"
                    };
                }));

            await _cache.SetAsync(CacheSchems.CACHE_KEY_TIMER_JOB, taskMethods);
            return taskMethods;
        }
    }
}
