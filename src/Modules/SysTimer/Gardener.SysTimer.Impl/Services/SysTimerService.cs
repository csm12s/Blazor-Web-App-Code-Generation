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
using Microsoft.AspNetCore.Authorization;
using Gardener.SysTimer.Dtos;
using Gardener.SysTimer.Domains;

namespace Gardener.SysTimer.Services
{
    /// <summary>
    /// 任务调度服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class SysTimerService:ServiceBase<SysTimerEntity, SysTimerDto>, ISysTimerService, IScoped
    {
        private readonly ICache _cache;

        /// <summary>
        /// 任务调度服务Init
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cache"></param>
        public SysTimerService(IRepository<SysTimerEntity> repository, ICache cache):base(repository)
        {
            _cache = cache;
        }

        /// <summary>
        /// 分页获取任务列表
        /// </summary>
        /// <remarks>
        /// 分页获取任务列表
        /// </remarks>
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
                    u.Exception = JSON.Serialize(timer.Exception);
                }
                lst.Add(u);
            });
            timers.Items = lst;
            return timers;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        ///<remarks>
        /// 搜索
        /// </remarks>
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
                    u.RunNumber = timer.Tally;
                    u.Exception = JSON.Serialize(timer.Exception);
                }
                lst.Add(u);
            });
            timers.Items = lst;
            return timers;
        }
        /// <summary>
        /// 获取所有本地任务
        /// </summary>
        /// <remarks>
        /// 获取所有本地任务
        /// </remarks>
        /// <returns></returns>
        public async Task<IEnumerable<TaskMethodInfo>> GetLocalJobList()
        {
            // 获取本地所有任务方法
            return await GetTaskMethods();
        }

        /// <summary>
        /// 增加任务
        /// </summary>
        /// <remarks>
        /// 增加任务
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost()]
        public override async Task<SysTimerDto> Insert(SysTimerDto input)
        {
            var data = await base.Insert(input);
            AddTimerJob(data);
            return data;
        }

        /// <summary>
        /// 假删除任务
        /// </summary>
        /// <remarks>
        /// 假删除任务
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> FakeDelete(int id)
        {
            var timer = await _repository.FindAsync(id);
            if (timer == null)
            {
                return false;
                throw Oops.Oh(ExceptionCode.TASK_ALLREADY_EXIST);
            }
                
            var result = await base.FakeDelete(id);

            if(result)
            {
                // 从调度器里取消
                SpareTime.Cancel(timer.JobName);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <remarks>
        /// 删除任务
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            var timer = await _repository.FindAsync(id);
            if (timer == null)
            {
                return false;
                throw Oops.Oh(ExceptionCode.TASK_ALLREADY_EXIST);
            }

            var result = await base.Delete(id);

            if (result)
            {
                // 从调度器里取消
                SpareTime.Cancel(timer.JobName);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <remarks>
        /// 修改任务
        /// </remarks>
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

            var result = await base.Update(input);
            if(result)
            {
                // 再添加到任务调度里
                AddTimerJob(input);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查看任务
        /// </summary>
        /// <remarks>
        /// 查看任务
        /// </remarks>
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
        /// <remarks>
        /// 停止任务
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost()]
        public void Stop(StopJobInput input)
        {
            SpareTime.Stop(input.JobName);
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <remarks>
        /// 启动任务
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost()]
        public void Start(SysTimerDto input)
        {
            var timer = SpareTime.GetWorkers().ToList().Find(u => u.WorkerName == input.JobName);
            if (timer == null)
                AddTimerJob(input);

            // 如果 StartNow 为 flase , 执行 AddTimerJob 并不会启动任务
            SpareTime.Start(input.JobName);
        }

        /// <summary>
        /// 新增定时任务
        /// </summary>
        /// <param name="input"></param>
        [NonAction]
        public void AddTimerJob(SysTimerDto myinput)
        {
            var input = myinput.Adapt<SysTimerEntity>();
            Action<SpareTimer, long> action = null;

            switch (input.RequestType)
            {
                // 创建本地方法委托
                case RequestType.Run:
                    {
                        // 查询符合条件的任务方法
                        var taskMethod = GetTaskMethods()?.Result.FirstOrDefault(m => m.RequestUrl == input.RequestUrl);
                        if (taskMethod == null) break;

                        // 创建任务对象
                        var typeInstance = Activator.CreateInstance(taskMethod.DeclaringType);

                        // 创建委托
                        action = (Action<SpareTimer, long>)Delegate.CreateDelegate(typeof(Action<SpareTimer, long>), typeInstance, taskMethod.MethodName);
                        break;
                    }
                // 创建网络任务委托
                default:
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

                            switch (input.RequestType)
                            {
                                case RequestType.Get:
                                    await requestUrl.SetHeaders(headers).GetAsync();
                                    break;

                                case RequestType.Post:
                                    await requestUrl.SetHeaders(headers).SetQueries(requestParameters).PostAsync();
                                    break;

                                case RequestType.Put:
                                    await requestUrl.SetHeaders(headers).SetQueries(requestParameters).PutAsync();
                                    break;

                                case RequestType.Delete:
                                    await requestUrl.SetHeaders(headers).DeleteAsync();
                                    break;
                            }
                        };
                        break;
                    }
            }

            if (action == null) return;

            // 缓存任务配置参数，以供任务运行时读取
            if (input.RequestType == RequestType.Run)
            {
                var jobParametersName = $"{input.JobName}_Parameters";
                var jobParameters = _cache.Exists<object>(jobParametersName);
                var requestParametersIsNull = string.IsNullOrEmpty(input.RequestParameters);

                // 如果没有任务配置却又存在缓存，则删除缓存
                if (requestParametersIsNull && jobParameters)
                    _cache.RemoveAsync(jobParametersName);
                else if (!requestParametersIsNull)
                    _cache.SetAsync(jobParametersName, JSON.Deserialize<Dictionary<string, string>>(input.RequestParameters));
            }

            // 创建定时任务
            switch (input.TimerType)
            {
                case SpareTimeTypes.Interval:
                    if (input.DoOnce)
                        SpareTime.DoOnce((int)input.Interval * 1000, action, input.JobName, input.Remark, input.StartNow, executeType: input.ExecuteType);
                    else
                        SpareTime.Do((int)input.Interval * 1000, action, input.JobName, input.Remark, input.StartNow, executeType: input.ExecuteType);
                    break;

                case SpareTimeTypes.Cron:
                    SpareTime.Do(input.Cron, action, input.JobName, input.Remark, input.StartNow, executeType: input.ExecuteType);
                    break;
            }
        }

        /// <summary>
        /// 启动自启动任务
        /// </summary>
        [NonAction]
        public void StartTimerJob()
        {
            var sysTimerList = _repository.DetachedEntities.Where(t => t.StartNow).ProjectToType<SysTimerDto>().ToList();
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
                        RequestUrl = $"{m.DeclaringType.Name}/{m.Name}",
                        Cron = spareTimeAttribute.CronExpression,
                        DoOnce = spareTimeAttribute.DoOnce,
                        ExecuteType = (ExecutType)spareTimeAttribute.ExecuteType,
                        Interval = (int)spareTimeAttribute.Interval / 1000,
                        StartNow = spareTimeAttribute.StartNow,
                        RequestType = RequestType.Run,
                        Remark = spareTimeAttribute.Description,
                        TimerType = string.IsNullOrEmpty(spareTimeAttribute.CronExpression) ? (TimerTypes)SpareTimeTypes.Interval : (TimerTypes)SpareTimeTypes.Cron,
                        MethodName = m.Name,
                        DeclaringType = m.DeclaringType
                    };
                }));

            await _cache.SetAsync(CacheSchems.CACHE_KEY_TIMER_JOB, taskMethods);
            return taskMethods;
        }
    }
}
