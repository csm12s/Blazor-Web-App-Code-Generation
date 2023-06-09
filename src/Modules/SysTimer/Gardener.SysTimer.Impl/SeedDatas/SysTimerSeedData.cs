using Furion.DatabaseAccessor;
using Furion.TaskScheduler;
using Gardener.Enums;
using Gardener.SysTimer.Domains;
using Gardener.SysTimer.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Gardener.SysTimer
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class SysTimerSeedData : IEntitySeedData<SysTimerEntity>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<SysTimerEntity> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new SysTimerEntity
                {
                    Id = 1,
                    JobName = "百度api",
                    DoOnce = false,
                    StartNow = false,
                    Interval = 5,
                    TimerType = SpareTimeTypes.Interval,
                    ExecuteType = ExecuteType.HTTP,
                    RequestUrl = "https://www.baidu.com",
                    HttpMethod = HttpMethod.GET,
                    ExecutMode=ExecutMode.Scceeding,
                    IsDeleted = false,
                    Remark = "接口API",
                    Started = true
                },
                new SysTimerEntity
                {
                    Id = 2,
                    JobName = "测试本地定时任务DEMO",
                    DoOnce = false,
                    StartNow = true,
                    Interval = 5,
                    TimerType = SpareTimeTypes.Interval,
                    ExecuteType = ExecuteType.LOCAL_METHOD,
                    ExecutMode=ExecutMode.Scceeding,
                    IsDeleted = false,
                    Remark = "定时抓取财经新闻，作为聊天数据推送到客户端",
                    LocalMethod="Gardener.SysTimer.Impl.Demo.DomeWorker|DoSomething",
                    Started = true
                }
            };
        }
    }
}