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
using System;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Impl
{
    /// <summary>
    /// 定时器监听
    /// </summary>
    public class TimeListener : ISpareTimeListener, ISingleton
    {
        /// <summary>
        /// 定时器监听
        /// </summary>
        /// <param name="executer"></param>
        /// <returns></returns>
        public async Task OnListener(SpareTimerExecuter executer)
        {
           var _repository= Db.GetRepository<SysTimerEntity>(); 
            var dbTimer = await _repository.FirstOrDefaultAsync(u => u.JobName == executer.Timer.WorkerName, false);
            if (dbTimer == null)
            {
                return;
            }
            switch (executer.Status)
            {
                // 执行开始通知
                case 0:
                    dbTimer.Started = true;
                    await _repository.UpdateIncludeAsync(dbTimer, new[] { nameof(SysTimerEntity.Started), nameof(SysTimerEntity.UpdatedTime) });
                    Console.WriteLine($"{executer.Timer.WorkerName} 任务开始通知");
                    break;
                // 任务执行之前通知
                case 1:
                    dbTimer.RunNumber++;
                    await _repository.UpdateIncludeAsync(dbTimer, new[] { nameof(SysTimerEntity.RunNumber), nameof(SysTimerEntity.UpdatedTime) });
                    Console.WriteLine($"{executer.Timer.WorkerName} 执行之前通知");
                    break;
                // 执行成功通知
                case 2:
                    Console.WriteLine($"{executer.Timer.WorkerName} 执行成功通知");
                    break;
                // 任务执行失败通知
                case 3:
                    dbTimer.RunErrorNumber++;
                    await _repository.UpdateIncludeAsync(dbTimer, new[] { nameof(SysTimerEntity.RunErrorNumber), nameof(SysTimerEntity.UpdatedTime) });
                    Console.WriteLine($"{executer.Timer.WorkerName} 执行失败通知");
                    break;
                // 任务执行停止通知
                case -1:
                    dbTimer.Started = false;
                    await _repository.UpdateIncludeAsync(dbTimer, new[] { nameof(SysTimerEntity.Started), nameof(SysTimerEntity.UpdatedTime) });
                    Console.WriteLine($"{executer.Timer.WorkerName} 执行停止通知");
                    break;
                // 任务执行取消通知
                case -2:
                    Console.WriteLine($"{executer.Timer.WorkerName} 执行取消通知");
                    break;
                default:
                    break;
            }
            return;
        }
    }
}
