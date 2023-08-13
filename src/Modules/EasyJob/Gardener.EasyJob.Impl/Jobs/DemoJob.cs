// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Schedule;
using Furion.TimeCrontab;

namespace Gardener.EasyJob.Impl.Jobs
{
    /// <summary>
    /// 测试job
    /// </summary>
    //[PeriodSeconds(5)]	//	秒周期（间隔）作业触发器特性
    //[PeriodMinutes(5)]	//	分钟周期（间隔）作业触发器特性
    //[PeriodHours(5)]	//	小时周期（间隔）作业触发器特性
    //[Secondly]	//	每秒开始作业触发器特性
    //[Hourly]    //	每小时开始作业触发器特性
    //[Daily] //	每天（午夜）开始作业触发器特性
    //[Monthly]   //	每月 1 号（午夜）开始作业触发器特性
    //[Weekly]    //	每周日（午夜）开始作业触发器特性
    //[Yearly]    //	每年 1 月 1 号（午夜）开始作业触发器特性
    //[Workday]   //	每周一至周五（午夜）开始触发器特性
    //[SecondlyAt]    //	特定秒开始作业触发器特性
    //[MinutelyAt]    //	每分钟特定秒开始作业触发器特性
    //[HourlyAt]  //	每小时特定分钟开始作业触发器特性
    //[DailyAt]   //	每天特定小时开始作业触发器特性
    //[MonthlyAt] //	每月特定天（午夜）开始作业触发器特性
    //[WeeklyAt]  //	每周特定星期几（午夜）开始作业触发器特性
    //[YearlyAt]	//	每年特定月 1 号（午夜）开始作业触发器特性
    [JobDetail("job_demo", Description = "测试任务", GroupName = "demo", Concurrent = false)]
    [Period(5000, TriggerId = "job_demo_Period", Description = "毫秒周期（间隔）作业触发器")]	//	毫秒周期（间隔）作业触发器特性
    [Cron("*/23 * * * * *", CronStringFormat.WithSeconds, TriggerId = "job_demo_Cron", Description = "Cron 表达式作业触发器特性")]
    [Minutely(TriggerId = "job_demo_Minutely", Description = "每分钟开始作业触发器特性")]  //	每分钟开始作业触发器特性
    public class DemoJob : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
            long num = context.Trigger.NumberOfRuns;
            Random random = new Random();
            Thread.Sleep(random.Next(100, 10000));

            if (num % 20 == 0)
            {
                throw new Exception("模拟失败");
            }
            context.Result = "执行完成";
            return Task.CompletedTask;
        }
    }
}
