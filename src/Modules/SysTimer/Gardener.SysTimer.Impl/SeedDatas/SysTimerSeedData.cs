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
                    Remark = "接口API"
                },
                new SysTimerEntity
                {
                    Id = 1,
                    JobName = "本地DEMO",
                    DoOnce = false,
                    StartNow = false,
                    Interval = 5,
                    TimerType = SpareTimeTypes.Interval,
                    ExecuteType = ExecuteType.LOCAL,
                    HttpMethod = HttpMethod.GET,
                    ExecutMode=ExecutMode.Scceeding,
                    IsDeleted = false,
                    Remark = "定时执行财经消息抓取推送"
                }
            };
        }
    }
}