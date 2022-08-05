using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Gardener.SystemManager.Enums;
using Gardener.Base.Domains;

namespace Gardener.SysTimer
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceSeedData : IEntitySeedData<Resource>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new Resource(){Id=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794300"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="任务调度",Icon="robot",Remark="配置任务调度模式",Key="system_manager_systimer_config",Path="/system_manager/systimer",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=ResourceType.Menu,Order=80}
            };
        }
    }
}