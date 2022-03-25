using Furion.DatabaseAccessor;
using Furion.TaskScheduler;
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
                    ExecuteType = SpareTimeExecuteTypes.Serial,
                    RequestUrl = "https://www.baidu.com",
                    RequestType = RequestType.Post,
                    IsDeleted = false,
                    Remark = "接口API"
                }
            };
        }
    }
}