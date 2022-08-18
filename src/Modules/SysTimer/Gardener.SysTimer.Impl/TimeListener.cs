// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.TaskScheduler;
using Gardener.Base.Domains;
using Gardener.SysTimer.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Impl
{
    /// <summary>
    /// 定时器监听
    /// </summary>
    public class TimeListener : ISpareTimeListener, ISingleton
    {
        // 日志对象
        private readonly ILogger<TimeListener> _logger;

        // 服务工厂
        private readonly IServiceScopeFactory _scopeFactory;
        /// <summary>
        /// 定时器监听
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="scopeFactory"></param>
        public TimeListener(ILogger<TimeListener> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        /// <summary>
        /// 定时器监听
        /// </summary>
        /// <param name="executer"></param>
        /// <returns></returns>
        public async Task OnListener(SpareTimerExecuter executer)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _repository = Db.GetRepository<SysTimerEntity>(services);
                var dbTimer = await _repository.AsQueryable(false).Where(u => u.JobName == executer.Timer.WorkerName).FirstOrDefaultAsync();
                if (dbTimer == null)
                {
                    return;
                }
                switch (executer.Status)
                {
                    // 执行开始通知
                    case 0:
                        dbTimer.Started = true;
                        await _repository.UpdateIncludeNowAsync(dbTimer, new[] { nameof(SysTimerEntity.Started), nameof(SysTimerEntity.UpdatedTime) });
                        Console.WriteLine($"{executer.Timer.WorkerName} 任务开始通知");
                        break;
                    // 任务执行之前通知
                    case 1:
                        if (dbTimer.RunNumber.HasValue) { dbTimer.RunNumber++; } else { dbTimer.RunNumber = 1; }
                        await _repository.UpdateIncludeNowAsync(dbTimer, new[] { nameof(SysTimerEntity.RunNumber), nameof(SysTimerEntity.UpdatedTime) });
                        Console.WriteLine($"{executer.Timer.WorkerName} 执行之前通知");
                        break;
                    // 执行成功通知
                    case 2:
                        Console.WriteLine($"{executer.Timer.WorkerName} 执行成功通知");
                        break;
                    // 任务执行失败通知
                    case 3:
                        if (dbTimer.RunErrorNumber.HasValue) { dbTimer.RunErrorNumber++; } else { dbTimer.RunErrorNumber = 1; }
                        await _repository.UpdateIncludeNowAsync(dbTimer, new[] { nameof(SysTimerEntity.RunErrorNumber), nameof(SysTimerEntity.UpdatedTime) });
                        Console.WriteLine($"{executer.Timer.WorkerName} 执行失败通知");
                        _logger.LogError($"{executer.Timer.WorkerName} 执行失败", executer.Timer.Exception.LastOrDefault());
                        break;
                    // 任务执行停止通知
                    case -1:
                        dbTimer.Started = false;
                        await _repository.UpdateIncludeNowAsync(dbTimer, new[] { nameof(SysTimerEntity.Started), nameof(SysTimerEntity.UpdatedTime) });
                        Console.WriteLine($"{executer.Timer.WorkerName} 执行停止通知");
                        break;
                    // 任务执行取消通知
                    case -2:
                        Console.WriteLine($"{executer.Timer.WorkerName} 执行取消通知");
                        break;
                    default:
                        break;
                }
            }

            return;
        }
    }
}
